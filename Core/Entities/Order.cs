using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Order:BaseEntity
    {
        
        public string Orderno { get; set; }
        public DateTime OrderDate  { get; set; }
        public string OrderTotal { get; set; }
        // public int Custormer { get; set; }
        public DateTime ShippingDate { get; set; }
        public bool IsDelivered { get; set; }=false;
        //
        public Customer Customer{ get; set; }
        public int CustomerId{ get; set; }


    
     
    }
}