using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string Orderno { get; set; }
        public DateTime OrderDate  { get; set; }
        public string OrderTotal { get; set; }
        public string Customer { get; set; }
        public DateTime ShippingDate { get; set; }
        public bool IsDelivered { get; set; }=false;
    }
}