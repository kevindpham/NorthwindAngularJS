using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using NorthwindCustomerService.DataAccess;
using NorthwindCustomerService.Model;
using NorthwindCustomerService.Web.ExtensionMethods;

namespace NorthwindCustomerService.Web.Controllers
{
    [RoutePrefix("api/v1/customers")]
    public class CustomersController : ApiController
    {
        private UnitOfWork uow;

        //Dependency injection using Unity
        public CustomersController(UnitOfWork uow)
        {
            this.uow = uow;
        }

        // GET: api/Customers/count
        [Route("count")]
        public async Task<int> GetCustomersCount()
        {
            return await uow.CustomerRepository.GetCountAsync();
        }

        // GET: api/Customers
        [Route("")]
        public async Task<IHttpActionResult> GetCustomers()
        {
            var customers = await uow.CustomerRepository.GetAsync(orderBy: c => c.OrderBy(s => s.CustomerID));
            return Ok(customers.Select(Mapper.Map<CustomerDTO>));
        }

        // GET: api/Customers?page=5&take=10
        [Route("")]
        public async Task<IHttpActionResult> GetCustomers(int page, int take)
        {
            int skip = page == 0 ? 0 : (page - 1) * take ;
            var customers = await uow.CustomerRepository.GetAsync(orderBy: c => c.OrderBy(s => s.CustomerID), skip: skip, take: take);
            return Ok(customers.Select(Mapper.Map<CustomerDTO>));
        }

        // GET: api/Customers/5
        [Route("{id}")]
        [ResponseType(typeof(CustomerDTO))]
        public async Task<IHttpActionResult> GetCustomer(string id)
        {
            Customer customer = await uow.CustomerRepository.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CustomerDetailDTO>(customer));
        }

        // GET: api/Customers/5/Orders
        [Route("{id}/orders")]
        [ResponseType(typeof(OrderDTO))]
        public async Task<IHttpActionResult> GetOrdersByCustomerId(string id)
        {
            IEnumerable<Customer> customers = await uow.CustomerRepository.GetAsync(filter: c=>c.CustomerID==id , includeProperties: "orders");
            Customer customer = customers.FirstOrDefault();
            
            if (customer == null)
            {
                return NotFound();
            }
            IEnumerable<Order> orders = customer.Orders;

            return Ok(orders.Select(o=>Mapper.Map<OrderDTO>(o).Map(customer)));
        }

        // GET: api/Customers/5/Orders/1
        [Route("{id}/orders/{orderId:int}")]
        [ResponseType(typeof(OrderDTO))]
        public async Task<IHttpActionResult> GetOrderByCustomer(string id, int orderId)
        {

            IEnumerable<Customer> customers = await uow.CustomerRepository.GetAsync(filter: c => c.CustomerID == id, includeProperties: "orders");
            Customer customer = customers.FirstOrDefault();

            if (customer == null)
            {
                return NotFound();
            }
            Order order = customer.Orders.FirstOrDefault(o => o.OrderID == orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<OrderDTO>(order));
        }

        // PUT: api/Customers/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomer(string id, Customer customer)
        {
            if (string.IsNullOrEmpty(customer.CompanyName))
            {
                ModelState.AddModelError("CompanyName", new Exception("You must specify a company name"));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerID)
            {
                return BadRequest();
            }

            uow.CustomerRepository.Update(customer);

            try
            {
               await uow.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customers
        [Route("{id?}")]
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            uow.CustomerRepository.Add(customer);

            try
            {
               await uow.SaveAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerID))
                {
                    return Conflict();
                }
                throw;
            }
            return Created(Request.RequestUri + customer.CustomerID, customer);
          //  return CreatedAtRoute("DefaultApi", new { id = customer.CustomerID }, customer);
        }

        // DELETE: api/Customers/5
        [Route("{id}")]
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> DeleteCustomer(string id)
        {
            Customer customer = await uow.CustomerRepository.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            uow.CustomerRepository.Delete(customer);
            await uow.SaveAsync();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                uow.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(string id)
        {
            return uow.CustomerRepository.Find(id) != null;
        }

    }
}