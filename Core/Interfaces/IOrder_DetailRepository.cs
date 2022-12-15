using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IOrder_DetailRepository
    {
        Task<Order_Details> GetOrder_DetailsByIdAsync(int id);
        Task<IReadOnlyList<Order_Details>> GetOrder_DetailsAsync();
        Task<bool> SaveChangesAsync();
        void Add(Order_Details order_Details);
        void Update(Order_Details order_Details);
        void Delete(Order_Details order_Details);
    }
}