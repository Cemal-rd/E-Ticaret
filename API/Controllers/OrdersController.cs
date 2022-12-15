using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.Dtos;
using Core.Entities;

namespace API.Controllers
{
    public class OrdersController:BaseApiController
    {
        public IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;

        }

        [HttpGet]
        public async  Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrders()
        {

            var orders = await _repository.GetOrderAsync();
            var mappedOrderDetails = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders);
            return Ok(mappedOrderDetails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrder(int id)
        {
            var orders= await _repository.GetOrderByIdAsync(id);
            return _mapper.Map<Order, OrderToReturnDto>(orders);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder(Order order)
        {
            _repository.Add(order);
            return Ok(await _repository.SaveChangesAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(string id, Order order)
        {
            _repository.Update(order);
            return Ok(await _repository.SaveChangesAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = await _repository.GetOrderByIdAsync(id);
            _repository.Delete(order);
            return Ok(await _repository.SaveChangesAsync());
        }

    }
}