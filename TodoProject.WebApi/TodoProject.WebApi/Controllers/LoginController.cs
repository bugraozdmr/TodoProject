using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;

using TodoProject.DtoLayer.LoginDto;
using TodoProject.EntityLayer.Concrete;
using TodoProject.WebApi.Models.Jwt;
using TodoProject.DataAccessLayer.Concrete;



namespace TodoProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<AppUser> _userManager;



        public LoginController(SignInManager<AppUser> signInManager, IOptions<JwtSettings> settings, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = settings.Value;
        }

        [HttpPost]  //LoginUserDto dto
        public async Task<IActionResult> login([FromBody] ApiUser user)
        {


            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
            if (result.Succeeded)
            {
                return Ok(new { message = "Signed in", data = CreateToken(user) });
            }
            return Unauthorized(new { message = "something went wrong" });
        }

        private async Task<string> CreateToken(ApiUser user)
        {
            if (_jwtSettings.Key == null) throw new Exception("key required");

            if(user.UserName!=null) user.Role = await _userManager.FindByNameAsync(user.UserName.ToString());

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claim = new[]
            {   // user.UserName!  sondaki unlem null mı değilmi kontrol etme demek
                new Claim(ClaimTypes.NameIdentifier, user.UserName!),
                new Claim(ClaimTypes.Role,user.Role!)
            };
            var token = new JwtSecurityToken(_jwtSettings.Issuer,
                _jwtSettings.Audience,
                claim,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
