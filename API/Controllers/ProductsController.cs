using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Utils;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class ProductsController : BaseApiController
    {
        public IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository repository, IMapper mapper,ILogger<ProductsController> logger)
        {
           
            _mapper = mapper;
            this._logger = logger;
            _repository = repository;

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                var products = await _repository.GetProductsAsync();
                var mappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
                _logger.LogInformation("getproductokey");
                var response = new Response(true, mappedProducts, null);
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
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                var product = await _repository.GetProductByIdAsync(id);
                var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(product);
                if (MappedProduct == null)
                {
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "Product not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);

                }
                else
                {
                    var response = new Response(true, MappedProduct, null);
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
        public async Task<ActionResult> AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                if (product.CategoryId == 0)
                {
                    var responseError = new ResponseError(StatusCodes.Status400BadRequest, "cannot be null");
                    var response = new Response(false, ModelState, responseError);
                    return BadRequest(response);
                }
                else
                {
                    _repository.Add(product);
                    await _repository.SaveChangesAsync();
                    var response = new Response(true, product, null);
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
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                if (id != product.Id)
                {
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "Product not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);

                }
                else
                {
                    _repository.Update(product);
                    await _repository.SaveChangesAsync();
                    var response = new Response(true, product, null);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                var product = await _repository.GetProductByIdAsync(id);
                if (product == null)
                {
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "Product not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);

                }
                else
                {
                    _repository.Delete(product);
                    await _repository.SaveChangesAsync();
                    var response = new Response(true, product, null);
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

    }
}