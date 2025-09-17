
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet101.DAL;
using Projet101.Models;

namespace Projet101.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController(IUnitOfWork _unitOfWork) : ControllerBase
    {
        
        [Authorize]
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _unitOfWork.GetRepository<Category>().Get();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _unitOfWork.GetRepository<Category>().GetByID(id);
            if (category == null) return NotFound();
            return Ok(category);
        }
        [Authorize]
        [HttpPost]
        public IActionResult PostCategory(Category category)
        {
            _unitOfWork.GetRepository<Category>().Insert(category);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutCategory(int id, Category category)
        {
            if (id != category.Id) return BadRequest();
            _unitOfWork.GetRepository<Category>().Update(category);
            _unitOfWork.Save();
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _unitOfWork.GetRepository<Category>().GetByID(id);
            if (category == null) return NotFound();
            _unitOfWork.GetRepository<Category>().Delete(category);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
