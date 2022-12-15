using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoriesController:BaseApiController
    {
        public ICategoryRepository _repository;
        

        public CategoriesController(ICategoryRepository repository)
        {
          
            _repository = repository;

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {

            var categories = await _repository.GetCategoryAsync();
            
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _repository.GetCategoryByIdAsync(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(Category category)
        {
            _repository.Add(category);
            return Ok(await _repository.SaveChangesAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(string id, Category category)
        {
            _repository.Update(category);
            return Ok(await _repository.SaveChangesAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _repository.GetCategoryByIdAsync(id);
            _repository.Delete(category);
            return Ok(await _repository.SaveChangesAsync());
        }
    }
}