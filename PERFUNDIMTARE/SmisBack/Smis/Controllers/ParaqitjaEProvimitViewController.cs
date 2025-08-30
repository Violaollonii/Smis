using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smis.Dtos.ParaqitjaEProvimitDto;
using Smis.Models;
using Microsoft.AspNetCore.Authorization;

namespace Smis.Controllers
{
    //  [Authorize(Roles = "StafAkademik")]
    [Route("api/[controller]")]
    [ApiController]
    public class ParaqitjaEProvimitViewController : ControllerBase
    {
        private readonly SmisContext _context;

        public ParaqitjaEProvimitViewController(SmisContext context)
        {
            _context = context;
        }
        [HttpGet("view-all/{id}")]
        public async Task<ActionResult<IEnumerable<ParaqitjaEProvimitViewDto>>> GetParaqitjetMeTeDhenat(int id)
        {
            var paraqitjet = await _context.ParaqitjaEprovimit
              .Include(p => p.Lenda)
              .Include(p => p.StafiAkademik)
                  .ThenInclude(s => s.Useri)
                  .Include(p => p.Studenti)
                  .ThenInclude(s => s.Useri) // ✅ Ky ishte mungues!
              .Include(p => p.Nota)
              .Where(p => p.StafiAkademikId == id)
              .ToListAsync(); // <-- e kthen në objekt normal

            var rezultati = paraqitjet.Select(p => new ParaqitjaEProvimitViewDto
            {
                ParaqitjaId = p.ParaqitjaId,
                KodiLendes = p.Lenda != null ? p.Lenda.LendaId.ToString() : "",
                EmriLendes = p.Lenda?.Emri ?? "",
                Kategoria = p.Lenda?.Kategoria ?? "",
                Studenti = p.Studenti?.Useri?.Emri + " " + p.Studenti?.Useri?.Mbiemri ?? "",
                Nota = p.Nota?.NotaNr,
                StatusiNotes = p.Nota != null ? "Vendosur" : "Pa vendosur",
                DataVendosjes = p.Nota?.DataVendosjes.HasValue == true
                    ? p.Nota.DataVendosjes.Value.ToDateTime(TimeOnly.MinValue)
                    : null,
                NotaId = p.Nota?.NotaId
            }).ToList();

            return Ok(rezultati);



        }
    }
}