using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindCustomerService.Model
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
      
    }
}