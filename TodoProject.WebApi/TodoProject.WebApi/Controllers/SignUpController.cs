using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoProject.DtoLayer.RegisterDto;
using TodoProject.EntityLayer.Concrete;

namespace TodoProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public SignUpController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // 123456aA*    -> password için
        [HttpPost]
        public async Task<IActionResult> register(CreateNewUserDto dto)
        {
            if (dto.Role == null) dto.Role = "user";

            var appUser = new AppUser()
            {
                UserName = dto.UserName,
                Role = dto.Role
            };

            var result = await _userManager.CreateAsync(appUser,
                dto.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "user created" });
            }

            return BadRequest(new { message = "user couldn't create" });
        }
    }
}
