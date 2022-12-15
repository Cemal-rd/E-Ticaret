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
    public class Order_DetailssController:BaseApiController
    {
        public IOrder_DetailRepository _repository;
        private readonly IMapper _mapper;

        public Order_DetailssController(IOrder_DetailRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;

        }

        [HttpGet]
        public async  Task<ActionResult<IReadOnlyList<OrderDetailToReturnDto>>> GetOrder_Details()
        {

            var order_Details = await _repository.GetOrder_DetailsAsync();
            var mappedOrderDetails = _mapper.Map<IReadOnlyList<Order_Details>, IReadOnlyList<OrderDetailToReturnDto>>(order_Details);
            return Ok(mappedOrderDetails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailToReturnDto>> GetOrder_Detail(int id)
        {
            var order_Details= await _repository.GetOrder_DetailsByIdAsync(id);
            return _mapper.Map<Order_Details, OrderDetailToReturnDto>(order_Details);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder_Detail(Order_Details order_Details)
        {
            _repository.Add(order_Details);
            return Ok(await _repository.SaveChangesAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder_Detail(string id, Order_Details order_Details)
        {
            _repository.Update(order_Details);
            return Ok(await _repository.SaveChangesAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder_Details(int id)
        {
            var order_Details = await _repository.GetOrder_DetailsByIdAsync(id);
            _repository.Delete(order_Details);
            return Ok(await _repository.SaveChangesAsync());
        }

        
    }
}