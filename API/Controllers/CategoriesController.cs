using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Utils;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        public ICategoryRepository _repository;


        public CategoriesController(ICategoryRepository repository)
        {

            _repository = repository;

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                var categories = await _repository.GetCategoryAsync();
                var response = new Response(true, categories, null);
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
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                var category = await _repository.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "category not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);
                }
                else
                {
                    var response = new Response(true, category, null);
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
        public async Task<ActionResult> AddCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                if (category.Name == "string")
                {
                    var responseError = new ResponseError(StatusCodes.Status400BadRequest, "category name cannot be null");
                    var response = new Response(false, ModelState, responseError);
                    return BadRequest(response);

                }
                else
                {
                    _repository.Add(category);
                    await _repository.SaveChangesAsync();
                    var response = new Response(true, category, null);
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
        public async Task<ActionResult> UpdateCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                if (id != category.Id)
                {
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "category not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);
                }
                else
                {
                    _repository.Update(category);
                    await _repository.SaveChangesAsync();
                    var response = new Response(true, category, null);
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
        public async Task<ActionResult> DeleteCategory(int id)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                var category = await _repository.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "Product not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);
                }else{
                    _repository.Delete(category);
                    await _repository.SaveChangesAsync();
                    var response = new Response(true, category, null);
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