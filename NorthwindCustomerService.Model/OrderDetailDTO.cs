﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindCustomerService.Model
{
    public class OrderDetailDTO
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public string Productname { get; set; }
    }
}