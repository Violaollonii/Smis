using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Smis.Models;

using Smis.Dtos.SallaDto;
using Smis.Mappers;
using Microsoft.AspNetCore.Authorization;

namespace Smis.Controllers

{
    [Route("api/salla")]
    [ApiController]
    public class SallaController : ControllerBase
    {
        private readonly SmisContext _context;

        public SallaController(SmisContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,StafAkademik,SuperAdmin")]

        public IActionResult GetAll()
        {
            var sallaList = _context.Salla
                .Select(s => s.ToSallaDto())
                .ToList();

            return Ok(sallaList);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Student,StafAkademik,SuperAdmin")]

        public IActionResult GetById(int id)
        {
            var salla = _context.Salla.Find(id);
            if (salla == null)
            {
                return NotFound();
            }
            return Ok(salla.ToSallaDto());
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]

        public IActionResult Create([FromBody] CreateSallaDto sallaDto)
        {
            var salla = sallaDto.ToSallaFromCreateDto();
            _context.Salla.Add(salla);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = salla.SallaId }, salla.ToSallaDto());
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]

        public async Task<IActionResult> UpdateSalla(int id, [FromBody] CreateSallaDto updateDto)
        {
            var salla = await _context.Salla.FindAsync(id);
            if (salla == null)
            {
                return NotFound();
            }

            salla.UpdateEntity(updateDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]

        public async Task<IActionResult> DeleteSalla(int id)
        {
            var salla = await _context.Salla.FindAsync(id);
            if (salla == null)
            {
                return NotFound();
            }

            _context.Salla.Remove(salla);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
