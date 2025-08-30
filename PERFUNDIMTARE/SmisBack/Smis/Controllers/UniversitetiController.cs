using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smis.Dtos.UniversitetiDto;
using Smis.Models;
using Microsoft.AspNetCore.Authorization;



namespace Smis.Controllers

{
    // [Authorize]
    [ApiController]
    [Route("api/universiteti")]

    public class UniversitetiController : ControllerBase
    {
        private readonly SmisContext _context;
        private readonly IMapper _mapper;

        public UniversitetiController(SmisContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
           [Authorize(Roles = "Admin,SuperAdmin")]

        public async Task<ActionResult<List<GetUniversitetiDto>>> GetAll()
        {
            var universitetet = await _context.Universiteti
                .Include(u => u.Lokacionet)
                .ToListAsync();
            var getUniversitetiDto = _mapper.Map<List<GetUniversitetiDto>>(universitetet);

            return Ok(getUniversitetiDto);
        }
        [HttpGet("{id}")]
           [Authorize(Roles = "Admin,Student,StafAkademik,SuperAdmin")]

        public async Task<ActionResult<GetUniversitetiDto>> GetUniversiteti(int id)
        {
            var universiteti = await _context.Universiteti
          .Include(u => u.Lokacionet)
          .FirstOrDefaultAsync(u => u.UniId == id);

            if (universiteti == null) return NotFound();

            var uniDto = _mapper.Map<GetUniversitetiDto>(universiteti);
            return Ok(uniDto);
        }
        [HttpPost]
           [Authorize(Roles = "SuperAdmin")]

        public async Task<ActionResult<CreateEditUniversitetiDto>> CreateUniversiteti([FromBody] CreateEditUniversitetiDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var universiteti = _mapper.Map<Universiteti>(dto);
            _context.Universiteti.Add(universiteti);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateUniversiteti), new { id = universiteti.UniId }, new
            {
                message = "Universiteti u shtua me sukses",
                universitetiId = universiteti.UniId
            });
        }
        [HttpPut("{id}")]
           [Authorize(Roles = "SuperAdmin")]


        public async Task<IActionResult> UpdateUniversiteti(int id, [FromBody] CreateEditUniversitetiDto dto)
        {
            var universiteti = await _context.Universiteti
           .Include(u => u.Lokacionet)
           .FirstOrDefaultAsync(u => u.UniId == id);
            if (universiteti == null) return NotFound();

            _mapper.Map(dto, universiteti);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Universiteti u perditesua me sukses" });

        }

        [HttpDelete("{id}")]
           [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> DeleteUniversiteti(int id)
        {
            var universiteti = await _context.Universiteti
                .Include(u => u.Lokacionet)
                .FirstOrDefaultAsync(u => u.UniId == id);

            if (universiteti == null) return NotFound();

            _context.Universiteti.Remove(universiteti);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Universiteti u fshi me sukses" });
        }


    }
}