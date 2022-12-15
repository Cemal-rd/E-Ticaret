using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class Order_DetailRepository : IOrder_DetailRepository
    {
        private readonly StoreContext _context;

        public Order_DetailRepository(StoreContext context)
        {
            _context = context;
        }
        public void Add(Order_Details order_Details)
        {
            _context.Add(order_Details);
        }

        public void Delete(Order_Details order_Details)
        {
            _context.Add(order_Details);
        }

        public async Task<IReadOnlyList<Order_Details>> GetOrder_DetailsAsync()
        {
           return await _context.Order_Details.Include(p => p.Product).Include(p=>p.Order).ToListAsync();
        }

        public async Task<Order_Details> GetOrder_DetailsByIdAsync(int id)
        {
            return await _context.Order_Details.Include(p => p.Product).Include(p=>p.Order)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public  async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public void Update(Order_Details order_Details)
        {
             _context.Attach(order_Details);
            _context.Entry(order_Details).State = EntityState.Modified;
        }
    }
}