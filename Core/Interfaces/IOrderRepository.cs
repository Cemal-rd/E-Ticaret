using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int id);
        Task<IReadOnlyList<Order>> GetOrderAsync();
        Task<bool> SaveChangesAsync();
        void Add(Order order);
        void Update(Order order);
        void Delete(Order order);
    }
}