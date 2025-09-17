using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet101.DAL;
using Projet101.Models;

namespace Projet101.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IUnitOfWork _unitOfWork) : ControllerBase
    {

        [Authorize]
        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _unitOfWork.GetRepository<Order>().Get();
            return Ok(orders);
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _unitOfWork.GetRepository<Order>().GetByID(id);
            if (order == null) return NotFound();
            return Ok(order);
        }
        [Authorize]
        [HttpPost]
        public IActionResult PostOrder(Order order)
        {
            _unitOfWork.GetRepository<Order>().Insert(order);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutOrder(int id, Order order)
        {
            if (id != order.Id) return BadRequest();
            _unitOfWork.GetRepository<Order>().Update(order);
            _unitOfWork.Save();
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _unitOfWork.GetRepository<Order>().GetByID(id);
            if (order == null) return NotFound();
            _unitOfWork.GetRepository<Order>().Delete(order);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
