using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet101.DAL;
using Projet101.Models;

namespace Projet101.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingAddressesController(IUnitOfWork _unitOfWork) : ControllerBase
    {

        [Authorize]
        [HttpGet]
        public IActionResult GetAddresses()
        {
            var addresses = _unitOfWork.GetRepository<ShippingAddress>().Get();
            return Ok(addresses);
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetAddress(int id)
        {
            var address = _unitOfWork.GetRepository<ShippingAddress>().GetByID(id);
            if (address == null) return NotFound();
            return Ok(address);
        }
        [Authorize]
        [HttpPost]
        public IActionResult PostAddress(ShippingAddress address)
        {
            _unitOfWork.GetRepository<ShippingAddress>().Insert(address);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetAddress), new { id = address.Id }, address);
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutAddress(int id, ShippingAddress address)
        {
            if (id != address.Id) return BadRequest();
            _unitOfWork.GetRepository<ShippingAddress>().Update(address);
            _unitOfWork.Save();
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            var address = _unitOfWork.GetRepository<ShippingAddress>().GetByID(id);
            if (address == null) return NotFound();
            _unitOfWork.GetRepository<ShippingAddress>().Delete(address);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
