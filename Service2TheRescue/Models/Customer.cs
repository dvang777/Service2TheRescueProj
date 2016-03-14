using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service2TheRescue.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string CustomerID { get; set; }
    }
}