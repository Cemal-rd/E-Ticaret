using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.Dtos;
using Core.Entities;
using API.Utils;

namespace API.Controllers
{
    public class OrdersController : BaseApiController
    {
        public IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrders()
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                var orders = await _repository.GetOrderAsync();
                var mappedOrder = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders);
                var response = new Response(true, mappedOrder, null);
                return Ok(response);
            }
            catch (Exception e)
            {
                var responseError = new ResponseError(StatusCodes.Status500InternalServerError, e.Message);
                var response = new Response(false, null, responseError);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrder(int id)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                var orders = await _repository.GetOrderByIdAsync(id);
                var MappedOrder = _mapper.Map<Order, OrderToReturnDto>(orders);
                if (MappedOrder == null)
                {
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "Product not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);
                }
                else
                {
                    var response = new Response(true, MappedOrder, null);
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                var responseError = new ResponseError(StatusCodes.Status500InternalServerError, e.Message);
                var response = new Response(false, null, responseError);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }


        }

        [HttpPost]
        public async Task<ActionResult> AddOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                if (order.CustomerId == 0)
                {
                    var responseError = new ResponseError(StatusCodes.Status400BadRequest, "cannot be null");
                    var response = new Response(false, ModelState, responseError);
                    return BadRequest(response);

                }
                else
                {
                    _repository.Add(order);
                    await _repository.SaveChangesAsync();
                    var response = new Response(true, order, null);
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                var responseError = new ResponseError(StatusCodes.Status500InternalServerError, e.Message);
                var response = new Response(false, null, responseError);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }


        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                if (id != order.Id)
                {
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "Product not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);
                }
                else
                {
                    _repository.Update(order);
                   await _repository.SaveChangesAsync();
                   var response = new Response(true, order, null);
                    return Ok(response);
                }
            } catch (Exception e)
            {
                var responseError = new ResponseError(StatusCodes.Status500InternalServerError, e.Message);
                var response = new Response(false, null, responseError);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
           
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }try{
                var order = await _repository.GetOrderByIdAsync(id);
                if(order==null){
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "Product not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);
                }else{
                    _repository.Delete(order);
                    await _repository.SaveChangesAsync();
                    var response = new Response(true, order, null);
                    return Ok(response);

                }
                
            } catch (Exception e)
            {
                var responseError = new ResponseError(StatusCodes.Status500InternalServerError, e.Message);
                var response = new Response(false, null, responseError);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
           
           
        }

    }
}