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
            if(!ModelState.IsValid){
                var responseError=new ResponseError(StatusCodes.Status400BadRequest,"invalid model");
                var response=new Response(false,ModelState,responseError);
                return BadRequest(response);
            }try{
                var order_Details = await _repository.GetOrder_DetailsAsync();
                var mappedOrderDetails = _mapper.Map<IReadOnlyList<Order_Details>, IReadOnlyList<OrderDetailToReturnDto>>(order_Details);
                var response=new Response(true,mappedOrderDetails,null);
                return Ok(response);

            }catch(Exception e){
                var responseError=new ResponseError(StatusCodes.Status500InternalServerError,e.Message);
                var response=new Response(false,null,responseError);
                return StatusCode(StatusCodes.Status500InternalServerError,response);
            }

           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailToReturnDto>> GetOrder_Detail(int id)
        {
             if(!ModelState.IsValid){
                var responseError=new ResponseError(StatusCodes.Status400BadRequest,"invalid model");
                var response=new Response(false,ModelState,responseError);
                return BadRequest(response);
            }try{
                var order_Details= await _repository.GetOrder_DetailsByIdAsync(id);
                var mappedorderdetail=_mapper.Map<Order_Details, OrderDetailToReturnDto>(order_Details);
                if(mappedorderdetail==null){
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "orderdetail not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);
                }else{
                    var response=new Response(true,mappedorderdetail,null);
                    return Ok(response);

                }
            
            }catch(Exception e){
                var responseError=new ResponseError(StatusCodes.Status500InternalServerError,e.Message);
                var response=new Response(false,null,responseError);
                return StatusCode(StatusCodes.Status500InternalServerError,response);
                }
           
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder_Detail(Order_Details order_Details)
        {
             if(!ModelState.IsValid){
                var responseError=new ResponseError(StatusCodes.Status400BadRequest,"invalid model");
                var response=new Response(false,ModelState,responseError);
                return BadRequest(response);
            }try{
                if(order_Details.OrderId==0){
                    var responseError=new ResponseError(StatusCodes.Status400BadRequest,"cannot be null");
                    var response=new Response(false,ModelState,   responseError);
                    return BadRequest(response);
                }else{
                    _repository.Add(order_Details);
                    await _repository.SaveChangesAsync();
                    var response =new Response(true,order_Details,null);
                    return Ok(response);
                }

            } catch(Exception e){
                var responseError=new ResponseError(StatusCodes.Status500InternalServerError,e.Message);
                var response=new Response(false,null,responseError);
                return StatusCode(StatusCodes.Status500InternalServerError,response);
            }
           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder_Detail(int id, Order_Details order_Details)
        {
            if(!ModelState.IsValid){
                var responseError=new ResponseError(StatusCodes.Status400BadRequest,"invalid model");
                var response=new Response(false,ModelState,responseError);
                return BadRequest(response);
            } try{
                if(id!=order_Details.Id){
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "Orderdetail not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);

                }else{
                    _repository.Update(order_Details);
                    await _repository.SaveChangesAsync();
                    var response =new Response(true,order_Details,null);
                    return Ok(response);
                }
            }catch(Exception e){
                var responseError=new ResponseError(StatusCodes.Status500InternalServerError,e.Message);
                var response=new Response(false,null,responseError);
                return StatusCode(StatusCodes.Status500InternalServerError,response);
            }
           
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