using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet101.DAL;
using Projet101.Models;

namespace Projet101.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUnitOfWork _unitOfWork) : ControllerBase
    {

        [Authorize]
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _unitOfWork.UserRepository.Get();
            return Ok(users);
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _unitOfWork.UserRepository.GetByID(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
        [Authorize]
        [HttpPost]
        public IActionResult PostUser(User user)
        {
            _unitOfWork.UserRepository.Insert(user);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (id != user.Id) return BadRequest();
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Save();
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _unitOfWork.UserRepository.GetByID(id);
            if (user == null) return NotFound();
            _unitOfWork.UserRepository.Delete(user);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
