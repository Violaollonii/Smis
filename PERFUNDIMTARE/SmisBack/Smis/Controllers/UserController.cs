using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smis.Dtos;
using Smis.Dtos.Useri;
using Smis.Mappers;
using Smis.Mappers.UserMappers;
using Smis.Models;

namespace Smis.Controllers
{
    [Route("api/useri")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly SmisContext _context;

        public UserController(SmisContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfileDto>>> GetAllUsers()
        {
            var users = await _context.Useri.ToListAsync();
            var usersDtos = users.Select(user => user.ToUserProfileDto()).ToList();
            return Ok(usersDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileDto>> GetUser(string id)
        {
            var user = await _context.Useri.FindAsync(id);
            if (user == null) return NotFound();
            return Ok(user.ToUserProfileDto());
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = createUserDto.ToUserEntity();
            _context.Useri.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, new
            {
                message = "Useri u shtua me sukses",
                userId = newUser.Id
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, CreateUserDto updateUserDto)
        {
            var user = await _context.Useri.FindAsync(id);
            if (user == null) return NotFound();
            user.UpdateEntity(updateUserDto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _context.Useri.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Useri.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

