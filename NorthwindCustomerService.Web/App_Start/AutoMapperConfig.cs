using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using NorthwindCustomerService.Model;

namespace NorthwindCustomerService.Web
{
    public class AutoMapperConfig
    {
        public static void CreateMapping()
        {
            //map to CustomerDTO and CustomerDetailDTO
            Mapper.CreateMap<Customer, CustomerDTO>();
            Mapper.CreateMap<Customer, CustomerDetailDTO>();

            //map to OrderDTO
            Mapper.CreateMap<Order, OrderDTO>();
            Mapper.CreateMap<Customer, OrderDTO>()
                .ForMember(o => o.CustomerName, opt => opt.MapFrom(c => c.ContactName));

            //map to OrderDetailDTO
            Mapper.CreateMap<Order, OrderDetailDTO>();
            Mapper.CreateMap<Order_Detail, OrderDetailDTO>();
            Mapper.CreateMap<Product, OrderDetailDTO>()
                 .ForMember(o => o.Productname, opt => opt.MapFrom(p => p.ProductName));

            //map to ProductDTO and ProductDetailDTO
            Mapper.CreateMap<Product, ProductDTO>();
            Mapper.CreateMap<Product, ProductDetailDTO>();
        }
    }
}