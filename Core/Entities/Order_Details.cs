using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Order_Details:BaseEntity
    {
        // public int propid { get; set; }
        public int Productquantity { get; set; }
        public int ProductPrice { get; set; }
        public int SubTotal { get; set; }
        //
        public Product Product{ get; set; }
        public int ProductId { get; set; }
        public Order Order{ get; set; }
        public int OrderId { get; set; }
    }
}