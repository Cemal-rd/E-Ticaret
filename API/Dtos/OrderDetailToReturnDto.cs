using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class OrderDetailToReturnDto
    {
        public int Id { get; set; }
        public int Productquantity { get; set; }
        public int ProductPrice { get; set; }
        public int SubTotal { get; set; }
        public string Product { get; set; }
        public string Order { get; set; }
    }
}