using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet101.DAL;
using Projet101.Models;

namespace Projet101.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IUnitOfWork _unitOfWork) : ControllerBase
    {

        [Authorize]
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _unitOfWork.GetRepository<Product>().Get();
            return Ok(products);
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _unitOfWork.GetRepository<Product>().GetByID(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
        [Authorize]
        [HttpPost]
        public IActionResult PostProduct(Product product)
        {
            _unitOfWork.GetRepository<Product>().Insert(product);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.Id) return BadRequest();
            _unitOfWork.GetRepository<Product>().Update(product);
            _unitOfWork.Save();
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _unitOfWork.GetRepository<Product>().GetByID(id);
            if (product == null) return NotFound();
            _unitOfWork.GetRepository<Product>().Delete(product);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
