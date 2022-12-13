using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Customer:BaseEntity
    {
        public string Customer_Email { get; set; }
        public string First_Name { get; set; }
        public string Last_name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

    
    }
}