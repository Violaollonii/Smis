using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smis.Dtos.AccountDto;
using Smis.Interfaces;
using Smis.Models;

namespace Smis.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Useri> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<Useri> _signInManager;
        private readonly SmisContext _context;

        public AccountController(
            UserManager<Useri> userManager,
            ITokenService tokenService,
            SignInManager<Useri> signInManager,
            SmisContext context)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _context = context;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);

            string primaryRole =
                roles.Contains("SuperAdmin") ? "SuperAdmin" :
                roles.Contains("Admin") ? "Admin" :
                roles.Contains("StafAkademik") ? "StafAkademik" :
                roles.Contains("Student") ? "Student" :
                roles.FirstOrDefault() ?? "Unknown";

            var student = await _context.Studenti.FirstOrDefaultAsync(s => s.Id == userId);
            var staf = await _context.StafiAkademik.FirstOrDefaultAsync(s => s.Id == userId);
            var admin = await _context.Admin.FirstOrDefaultAsync(a => a.Id == userId);

            return Ok(new
            {
                user.Id,
                user.Email,
                Role = primaryRole,
                StudentId = student?.StudentiId,
                UniversitetiId = student?.UniId ?? staf?.UniId ?? admin?.UniId,
                StafId = staf?.StafiId,
                AdminId = admin?.AdminId,
                DepartamentiId = student?.DepartamentiId ?? staf?.DepartamentiId ?? admin?.DepartamentiId
            });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(loginDto.Email)
                       ?? await _userManager.FindByNameAsync(loginDto.Email);

            if (user == null) return Unauthorized("User not found.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized("Invalid credentials.");

            var jwt = _tokenService.CreateToken(user);
            var refresh = _tokenService.GenerateRefreshToken();
            refresh.UserId = user.Id;

            _context.RefreshTokens.Add(refresh);
            await _context.SaveChangesAsync();

            var roles = await _userManager.GetRolesAsync(user);
            string primaryRole =
                roles.Contains("SuperAdmin") ? "SuperAdmin" :
                roles.Contains("Admin") ? "Admin" :
                roles.Contains("StafAkademik") ? "StafAkademik" :
                roles.Contains("Student") ? "Student" :
                roles.FirstOrDefault() ?? "Unknown";

            return Ok(new
            {
                userName = user.UserName,
                email = user.Email,
                role = primaryRole,
                token = jwt,
                refreshToken = refresh.Token
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.RefreshToken))
                return BadRequest("Refresh token required.");

            var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == req.RefreshToken);
            if (storedToken == null) return NotFound("Refresh token not found.");

            storedToken.Revoked = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok("U çkyç me sukses.");
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.RefreshToken))
                return BadRequest("Refresh token required.");

            var tokenFromDb = await _context.RefreshTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(x => x.Token == req.RefreshToken);

            if (tokenFromDb == null || !tokenFromDb.IsActive)
                return Unauthorized("Refresh token invalid or expired");

            tokenFromDb.Revoked = DateTime.UtcNow;

            var newJwt = _tokenService.CreateToken(tokenFromDb.User);
            var newRefresh = _tokenService.GenerateRefreshToken();
            newRefresh.UserId = tokenFromDb.User.Id;

            _context.RefreshTokens.Add(newRefresh);
            await _context.SaveChangesAsync();

            return Ok(new { token = newJwt, refreshToken = newRefresh.Token });
        }

        // =================== REGISTER ===================
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (string.IsNullOrWhiteSpace(registerDto.Roli))
                return BadRequest("Roli është i detyrueshëm.");

            var appUser = new Useri
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Emri = registerDto.Emri,
                Mbiemri = registerDto.Mbiemri,
                Datelindja = registerDto.Datelindja,
                NrLeternjoftimit = registerDto.NrLeternjoftimit,
                Gjinia = registerDto.Gjinia,
                VendLindja = registerDto.VendLindja,
                Vendbanimi = registerDto.Vendbanimi,
                Shteti = registerDto.Shteti,
                Adresa = registerDto.Adresa,
                Telefoni = registerDto.Telefoni,
                Zipkodi = registerDto.Zipkodi,
                Nenshtetesia = registerDto.Nenshtetesia,
                Foto = registerDto.Foto
            };

            var createUser = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (!createUser.Succeeded) return StatusCode(500, createUser.Errors);

            var roli = registerDto.Roli.Trim();

            if (roli == "Admin")
            {
                if (!registerDto.DepartamentiId.HasValue || !registerDto.UniId.HasValue)
                    return BadRequest("DepartamentiId dhe UniId janë të detyrueshme për Admin.");

                _context.Admin.Add(new Admin
                {
                    Id = appUser.Id,
                    DepartamentiId = registerDto.DepartamentiId.Value,
                    UniId = registerDto.UniId.Value
                });
            }
            else if (roli == "Student")
            {
                if (!registerDto.VitiRegjistrimit.HasValue || !registerDto.DepartamentiId.HasValue
                    || !registerDto.UniId.HasValue || !registerDto.GrupiId.HasValue || !registerDto.SemestriID.HasValue)
                    return BadRequest("Fushat e studentit janë të detyrueshme.");

                _context.Studenti.Add(new Studenti
                {
                    Id = appUser.Id,
                    VitiRegjistrimit = registerDto.VitiRegjistrimit.Value,
                    DepartamentiId = registerDto.DepartamentiId.Value,
                    UniId = registerDto.UniId.Value,
                    GrupiId = registerDto.GrupiId.Value,
                    Statusi = registerDto.Statusi!,
                    SemestriID = registerDto.SemestriID.Value
                });
            }
            else if (roli == "SuperAdmin")
            {
                _context.SuperAdminet.Add(new SuperAdmin { Id = appUser.Id });
            }
            else if (roli == "StafAkademik")
            {
                if (!registerDto.DepartamentiId.HasValue || !registerDto.UniId.HasValue)
                    return BadRequest("DepartamentiId dhe UniId janë të detyrueshme për StafAkademik.");

                _context.StafiAkademik.Add(new StafiAkademik
                {
                    Id = appUser.Id,
                    Titulli = registerDto.Titulli!,
                    RoliStafit = registerDto.RoliStafit!,
                    DepartamentiId = registerDto.DepartamentiId.Value,
                    VitiRegjistrimit = registerDto.VitiRegjistrimit,
                    UniId = registerDto.UniId.Value
                });
            }
            else return BadRequest("Roli i pasaktë.");

            await _context.SaveChangesAsync();

            var roleResult = await _userManager.AddToRoleAsync(appUser, roli);
            if (!roleResult.Succeeded) return StatusCode(500, roleResult.Errors);

            return Ok(new { UserName = appUser.UserName, Email = appUser.Email, Role = roli });
        }

        // =================== EDIT ===================
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut("edit-user/{id}")]
        public async Task<IActionResult> EditUser(string id, [FromBody] RegisterDto dto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound("User not found.");

            user.Email = dto.Email ?? user.Email;
            user.UserName = dto.Email ?? user.UserName;
            user.Emri = dto.Emri ?? user.Emri;
            user.Mbiemri = dto.Mbiemri ?? user.Mbiemri;
            user.Datelindja = dto.Datelindja ?? user.Datelindja;
            user.NrLeternjoftimit = dto.NrLeternjoftimit ?? user.NrLeternjoftimit;
            user.Gjinia = dto.Gjinia ?? user.Gjinia;
            user.VendLindja = dto.VendLindja ?? user.VendLindja;
            user.Vendbanimi = dto.Vendbanimi ?? user.Vendbanimi;
            user.Shteti = dto.Shteti ?? user.Shteti;
            user.Adresa = dto.Adresa ?? user.Adresa;
            user.Telefoni = dto.Telefoni ?? user.Telefoni;
            user.Zipkodi = dto.Zipkodi ?? user.Zipkodi;
            user.Nenshtetesia = dto.Nenshtetesia ?? user.Nenshtetesia;
            user.Foto = dto.Foto ?? user.Foto;

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin"))
            {
                var admin = await _context.Admin.FindAsync(id);
                if (admin != null)
                {
                    if (dto.DepartamentiId.HasValue) admin.DepartamentiId = dto.DepartamentiId.Value;
                    if (dto.UniId.HasValue) admin.UniId = dto.UniId.Value;
                }
            }
            else if (roles.Contains("Student"))
            {
                var studenti = await _context.Studenti.FindAsync(id);
                if (studenti != null)
                {
                    if (dto.VitiRegjistrimit.HasValue) studenti.VitiRegjistrimit = dto.VitiRegjistrimit.Value;
                    if (dto.UniId.HasValue) studenti.UniId = dto.UniId.Value;
                    if (dto.GrupiId.HasValue) studenti.GrupiId = dto.GrupiId.Value;
                    if (dto.DepartamentiId.HasValue) studenti.DepartamentiId = dto.DepartamentiId.Value;
                    if (!string.IsNullOrWhiteSpace(dto.Statusi)) studenti.Statusi = dto.Statusi!;
                    if (dto.SemestriID.HasValue) studenti.SemestriID = dto.SemestriID.Value;
                }
            }
            else if (roles.Contains("StafAkademik"))
            {
                var staf = await _context.StafiAkademik.FindAsync(id);
                if (staf != null)
                {
                    if (!string.IsNullOrWhiteSpace(dto.Titulli)) staf.Titulli = dto.Titulli!;
                    if (!string.IsNullOrWhiteSpace(dto.RoliStafit)) staf.RoliStafit = dto.RoliStafit!;
                    if (dto.DepartamentiId.HasValue) staf.DepartamentiId = dto.DepartamentiId.Value;
                    if (dto.VitiRegjistrimit.HasValue) staf.VitiRegjistrimit = dto.VitiRegjistrimit;
                    if (dto.UniId.HasValue) staf.UniId = dto.UniId.Value;
                }
            }

            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();

            return Ok("Useri u përditësua me sukses.");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound("User not found.");

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin"))
            {
                var admin = await _context.Admin.FindAsync(id);
                if (admin != null) _context.Admin.Remove(admin);
            }
            else if (roles.Contains("Student"))
            {
                var student = await _context.Studenti.FindAsync(id);
                if (student != null) _context.Studenti.Remove(student);
            }
            else if (roles.Contains("StafAkademik"))
            {
                var staf = await _context.StafiAkademik.FindAsync(id);
                if (staf != null) _context.StafiAkademik.Remove(staf);
            }

            await _userManager.DeleteAsync(user);
            await _context.SaveChangesAsync();

            return Ok("User deleted successfully.");
        }

        [AllowAnonymous]
        [HttpGet("email-exists")]
        public async Task<IActionResult> EmailExists([FromQuery] string email)
        {
            var exists = await _userManager.FindByEmailAsync(email) != null;
            return Ok(exists);
        }
    }
}
