using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet101.DAL;
using Projet101.Models;

namespace Projet101.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController(IUnitOfWork _unitOfWork) : ControllerBase
    {

        [Authorize]
        [HttpGet]
        public IActionResult GetPayments()
        {
            var payments = _unitOfWork.GetRepository<Payment>().Get();
            return Ok(payments);
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetPayment(int id)
        {
            var payment = _unitOfWork.GetRepository<Payment>().GetByID(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }
        [Authorize]
        [HttpPost]
        public IActionResult PostPayment(Payment payment)
        {
            _unitOfWork.GetRepository<Payment>().Insert(payment);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutPayment(int id, Payment payment)
        {
            if (id != payment.Id) return BadRequest();
            _unitOfWork.GetRepository<Payment>().Update(payment);
            _unitOfWork.Save();
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            var payment = _unitOfWork.GetRepository<Payment>().GetByID(id);
            if (payment == null) return NotFound();
            _unitOfWork.GetRepository<Payment>().Delete(payment);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
