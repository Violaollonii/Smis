using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Smis.Models;
using Smis.Mappers;
using Smis.Dtos.OrariDto;
using Smis.Dtos.UpdateOrariDto;

using Microsoft.AspNetCore.Authorization;

namespace Smis.Controllers
{
    // [Authorize]
    [Route("api/orari")]
    [ApiController]
    public class OrariController : ControllerBase
    {
        private readonly SmisContext _context;

        public OrariController(SmisContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var oraret = _context.Orari
                .ToList()
                .Select(o => o.ToOrariDto());

            return Ok(oraret);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var orari = _context.Orari.Find(id);
            if (orari == null)
            {
                return NotFound();
            }

            return Ok(orari.ToOrariDto());
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateOrariDto orariDto)
        {
            var orari = orariDto.ToOrariFromCreateDTO();
            _context.Orari.Add(orari);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = orari.OrariId }, orari.ToOrariDto());
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrari(int id, [FromBody] CreateOrariDto updateDto)
        {
            var orari = await _context.Orari.FindAsync(id);
            if (orari == null)
            {
                return NotFound();
            }

            orari.UpdateEntity(updateDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrari(int id)
        {
            var orari = await _context.Orari.FindAsync(id);
            if (orari == null)
            {
                return NotFound();
            }

            _context.Orari.Remove(orari);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
