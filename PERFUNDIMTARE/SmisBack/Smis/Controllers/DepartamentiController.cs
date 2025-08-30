using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smis.Dtos.Departamenti;
using Smis.Mappers.DepartamentiMappers;
using Smis.Models;
using Microsoft.AspNetCore.Authorization;

namespace Smis.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class DepartamentiController : ControllerBase
    {
        private readonly SmisContext _context;

        public DepartamentiController(SmisContext context)
        {
            _context = context;
        }

        // GET: api/Departamenti
        [HttpGet]
        [Authorize(Roles = "Admin,Student,StafAkademik,SuperAdmin")]

        public async Task<ActionResult<IEnumerable<DepartamentiDto>>> GetDepartamentet()
        {
            var departamentet = await _context.Departamenti
                .Include(d => d.Universiteti)
                .ToListAsync();

            return Ok(departamentet.Select(d => d.ToDepartamentiDto()));
        }





        // GET: api/Departamenti/5
        [HttpGet("{id}")]
           [Authorize(Roles = "Admin,Student,StafAkademik,SuperAdmin")]

        public async Task<ActionResult<DepartamentiDto>> GetDepartamenti(int id)
        {
            var departamenti = await _context.Departamenti
                .Include(d => d.Universiteti) // gjithashtu këtu
                .FirstOrDefaultAsync(d => d.DepartamentiId == id);

            if (departamenti == null)
                return NotFound();

            return new DepartamentiDto
            {
                DepartamentiId = departamenti.DepartamentiId,
                Emri = departamenti.Emri,
                UniId = departamenti.UniId,
                UniversitetiEmri = departamenti.Universiteti != null ? departamenti.Universiteti.Emri : null
            };
        }
        // POST: api/Departamenti
        [HttpPost]
           [Authorize(Roles = "SuperAdmin")]

        public async Task<ActionResult<Departamenti>> CreateDepartamenti(CreateDepartamentiDto dto)
        {
            var departamenti = dto.ToDepartamenti();
            _context.Departamenti.Add(departamenti);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDepartamenti), new { id = departamenti.DepartamentiId }, departamenti);
        }

        // PUT: api/Departamenti/5
        [HttpPut("{id}")]
           [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> UpdateDepartamenti(int id, CreateDepartamentiDto dto)
        {
            var departamenti = await _context.Departamenti.FindAsync(id);
            if (departamenti == null)
                return NotFound();

            departamenti.Emri = dto.Emri;
            departamenti.UniId = dto.UniId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Departamenti/5
        [HttpDelete("{id}")]
           [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> DeleteDepartamenti(int id)
        {
            var departamenti = await _context.Departamenti.FindAsync(id);
            if (departamenti == null)
                return NotFound($"Departamenti me ID {id} nuk u gjet.");

            // Kontrollo nëse ka varësi nga tabela të tjera
            bool kaLende = await _context.Lenda.AnyAsync(l => l.DepartamentiId == id);
            if (kaLende)
                return BadRequest("Nuk mund të fshihet departamenti sepse ka lëndë të lidhura.");

            bool kaGrup = await _context.Grupi.AnyAsync(g => g.DepartamentiId == id);
            if (kaGrup)
                return BadRequest("Nuk mund të fshihet departamenti sepse ka grupe të lidhura.");

            bool kaStaf = await _context.StafiAkademik.AnyAsync(s => s.DepartamentiId == id);
            if (kaStaf)
                return BadRequest("Nuk mund të fshihet departamenti sepse ka staf akademik të lidhur.");

            _context.Departamenti.Remove(departamenti);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
