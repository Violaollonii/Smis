using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smis.Dtos.Ligjerata;
using Smis.Mappers.LigjerataMappers;
using Smis.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Smis.Controllers
{
    //  [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class LigjerataController : ControllerBase
    {
        private readonly SmisContext _context;

        public LigjerataController(SmisContext context)
        {
            _context = context;
        }

        // GET: api/ligjerata
        [HttpGet]
        [Authorize(Roles = "Admin,Student,StafAkademik,SuperAdmin")]

        public async Task<ActionResult<IEnumerable<LigjerataDto>>> GetAll()
        {
            var ligjeratat = await _context.Ligjerata
                .Include(l => l.Lenda)
                .Include(l => l.Stafi)
                .ThenInclude(s => s.Useri)
                .Include(l => l.Orari)
                .ToListAsync();

            var dtos = ligjeratat.Select(l => l.ToLigjerataDto()).ToList();

            return Ok(dtos);
        }

        // GET: api/ligjerata/5/3
        [HttpGet("{stafiId}/{lendaId}")]
        [Authorize(Roles = "Admin,Student,StafAkademik,SuperAdmin")]

        public async Task<ActionResult<LigjerataDto>> GetById(int stafiId, int lendaId)
        {
            var ligjerata = await _context.Ligjerata
                .Include(l => l.Lenda)
                .Include(l => l.Stafi)
                .ThenInclude(s => s.Useri)
                .Include(l => l.Orari)
                .FirstOrDefaultAsync(l => l.StafiId == stafiId && l.LendaId == lendaId);

            if (ligjerata == null)
                return NotFound();

            return Ok(ligjerata.ToLigjerataDto());
        }
        [HttpGet("universiteti/{uniId}/departamenti/{departamentiId}")]
        [Authorize(Roles = "Admin,Student,StafAkademik,SuperAdmin")]
        public async Task<ActionResult<IEnumerable<LigjerataDto>>> GetLigjerataByUniAndDepartament(int uniId, int departamentiId)
        {
            var ligjeratat = await _context.Ligjerata
                .Include(l => l.Lenda)
                .ThenInclude(ld => ld.Departamenti)
                .Include(l => l.Stafi)
                .ThenInclude(s => s.Useri)
                .Include(l => l.Orari)
                // filtrim sipas departamentit + universitetit (nga Departamenti)
                .Where(l => l.Lenda.DepartamentiId == departamentiId &&
                            l.Lenda.Departamenti.UniId == uniId)
                .ToListAsync();

            var dtos = ligjeratat.Select(l => new LigjerataDto
            {
                StafiId = l.StafiId,
                LendaId = l.LendaId,
                ProfesoriEmri = l.Stafi?.Useri != null
                    ? $"{l.Stafi.Useri.Emri} {l.Stafi.Useri.Mbiemri}"
                    : null,
                LendaEmri = l.Lenda?.Emri,

            }).ToList();

            return Ok(dtos);
        }

        // POST: api/ligjerata
        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]

        public async Task<ActionResult<LigjerataDto>> Create(CreateLigjerataDto dto)
        {
            var ligjerata = dto.ToLigjerata();

            _context.Ligjerata.Add(ligjerata);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { stafiId = ligjerata.StafiId, lendaId = ligjerata.LendaId }, ligjerata.ToLigjerataDto());
        }

        // PUT: api/ligjerata/5/3
        [HttpPut("{stafiId}/{lendaId}")]
        [Authorize(Roles = "Admin,SuperAdmin")]

        public async Task<IActionResult> Update(int stafiId, int lendaId, CreateLigjerataDto updateDto)
        {
            var ligjerata = await _context.Ligjerata
                .FirstOrDefaultAsync(l => l.StafiId == stafiId && l.LendaId == lendaId);

            if (ligjerata == null)
                return NotFound();

            ligjerata.UpdateEntity(updateDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/ligjerata/5/3
        [HttpDelete("{stafiId}/{lendaId}")]
        [Authorize(Roles = "Admin,SuperAdmin")]

        public async Task<IActionResult> Delete(int stafiId, int lendaId)
        {
            var ligjerata = await _context.Ligjerata.FindAsync(stafiId, lendaId);
            if (ligjerata == null)
                return NotFound();

            _context.Ligjerata.Remove(ligjerata);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }

}
