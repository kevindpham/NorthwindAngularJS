using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindCustomerService.Model;

namespace NorthwindCustomerService.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private NorthwindEntities ctx;
        private GenericRepository<Customer> customerRepository;
        private GenericRepository<Order> orderRepository;
        private GenericRepository<Order_Detail> orderDetailRepository;
        private GenericRepository<Product> productRepository;

        private bool disposed = false;

        public UnitOfWork(NorthwindEntities ctx)
        {
            this.ctx = ctx;
            ctx.Configuration.ProxyCreationEnabled = false;
        }

        public GenericRepository<Customer> CustomerRepository
        {
            get
            {
                return customerRepository = customerRepository ?? new GenericRepository<Customer>(ctx);
            }
        }

        public GenericRepository<Order> OrderRepository
        {
            get
            {
                return orderRepository = orderRepository ?? new GenericRepository<Order>(ctx);
            }
        }

        public GenericRepository<Order_Detail> OrderDetailRepository
        {
            get
            {
                return orderDetailRepository = orderDetailRepository ?? new GenericRepository<Order_Detail>(ctx);
            }
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {
                return productRepository = productRepository ?? new GenericRepository<Product>(ctx);
            }
        }

        public async Task SaveAsync()
        {
           await ctx.SaveChangesAsync();
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
