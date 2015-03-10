using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorthwindCustomerService.Model;

namespace NorthwindCustomerService.Model
{
    public class CustomerDTO
    {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
       
    }
}