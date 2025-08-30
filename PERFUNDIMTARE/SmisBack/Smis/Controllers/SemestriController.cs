using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smis.Dtos.Semestri;
using Smis.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Smis.Controllers
{
    [Route("api/semestri")]
    [ApiController]
    [Authorize] // login i domosdoshëm për të gjitha endpoint-et (përveç atyre që e anulojnë me atribute)
    public class SemestriController : ControllerBase
    {
        private readonly SmisContext _context;
        private readonly IMapper _mapper;

        public SemestriController(SmisContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ===========================
        // LISTIM: Student + Admin
        // ===========================
        // - Student: sheh vetëm semestrat e universitetit të tij (UniId)
        // - Admin/SuperAdmin: sheh të gjithë semestrat
        [HttpGet]
        [Authorize(Roles = "Student,StafAkademik,Admin,SuperAdmin")]
        public async Task<ActionResult<IEnumerable<SemestriDto>>> GetSemestrat()
        {
            var isStudent = User.IsInRole("Student");
            IQueryable<Semestri> query = _context.Semestri.AsNoTracking();

            if (isStudent)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId)) return Unauthorized();

                var studenti = await _context.Studenti
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == userId);

                if (studenti == null) return Forbid();

                // ⚙️ filtro sipas universitetit të studentit
                query = query.Where(s => s.UniId == studenti.UniId);
            }
            // nëse është Admin/SuperAdmin → pa filtër (sheh të gjithë)

            var semestrat = await query.ToListAsync();
            var semestriDto = _mapper.Map<List<SemestriDto>>(semestrat);
            return Ok(semestriDto);
        }

        // ===========================
        // LISTIM sipas Departamentit
        // ===========================
        // Studentit i lejohet vetem nqs kërkon departamentin e vet;
        // Admin/SuperAdmin mund të kërkojnë cilindo departament
        [HttpGet("departamentiId/{departamentiId:int}")]
        [Authorize(Roles = "StafAkademik,Student,Admin,SuperAdmin")]
        public async Task<ActionResult<IEnumerable<SemestriDto>>> GetSemestratByDep(int departamentiId)
        {
            var isStudent = User.IsInRole("Student");

            int? uniId;
            if (isStudent)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId)) return Unauthorized();

                var studenti = await _context.Studenti
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == userId);
                if (studenti == null) return Forbid();

                // Student: mund të kërkojë vetëm departamentin e vet
                if (studenti.DepartamentiId != departamentiId) return Forbid();

                // nxirr UniId nga departamenti i kërkuar (i njëjtë me të studentit)
                uniId = await _context.Departamenti
                    .Where(d => d.DepartamentiId == departamentiId)
                    .Select(d => d.UniId)
                    .FirstOrDefaultAsync();
            }
            else
            {
                // Admin/SuperAdmin: mund të kërkojë për çdo departament
                uniId = await _context.Departamenti
                    .Where(d => d.DepartamentiId == departamentiId)
                    .Select(d => d.UniId)
                    .FirstOrDefaultAsync();
            }

            var semestrat = await _context.Semestri
                .AsNoTracking()
                .Where(s => s.UniId == uniId)
                .ToListAsync();

            var semestriDto = _mapper.Map<List<SemestriDto>>(semestrat);
            return Ok(semestriDto);
        }

        // ===========================
        // ADMIN/SUPERADMIN: GET ALL (DTO i zgjeruar)
        // ===========================
        [HttpGet("get-all-semestrat")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult<IEnumerable<GetSemestriDto>>> MerrTeGjitheSemestrat()
        {
            var semestrat = await _context.Semestri
                .AsNoTracking()
                .Include(s => s.Universiteti)
                .ToListAsync();

            var dto = semestrat.Select(s => new GetSemestriDto
            {
                SemestriId = s.SemestriId,
                Semestri1 = s.Semestri1,
                UniversitetiEmri = s.Universiteti != null ? s.Universiteti.Emri : "N/A",
                UniId = s.UniId ?? 0
            }).ToList();

            return Ok(dto);
        }

        // ===========================
        // ADMIN/SUPERADMIN: GET BY ID
        // ===========================
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult<SemestriDto>> GetSemestri(int id)
        {
            var semestri = await _context.Semestri.FindAsync(id);
            if (semestri == null) return NotFound();

            var semestriDto = _mapper.Map<SemestriDto>(semestri);
            return Ok(semestriDto);
        }

        // ===========================
        // ADMIN/SUPERADMIN: CREATE
        // ===========================
        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> CreateSemestri([FromBody] CreateSemestriDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var semestri = _mapper.Map<Semestri>(dto);
            _context.Semestri.Add(semestri);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSemestri), new { id = semestri.SemestriId }, new
            {
                message = "Semestri u shtua me sukses",
                semestriId = semestri.SemestriId
            });
        }

        // ===========================
        // ADMIN/SUPERADMIN: UPDATE
        // ===========================
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> UpdateSemestri(int id, [FromBody] CreateSemestriDto dto)
        {
            var semestri = await _context.Semestri.FindAsync(id);
            if (semestri == null) return NotFound();

            _mapper.Map(dto, semestri);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Semestri u përditësua me sukses" });
        }

        // ===========================
        // ADMIN/SUPERADMIN: DELETE
        // ===========================
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> DeleteSemestri(int id)
        {
            var semestri = await _context.Semestri.FindAsync(id);
            if (semestri == null) return NotFound();

            _context.Semestri.Remove(semestri);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Semestri u fshi me sukses" });
        }
    }
}
