using ChatApp.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public RegisterController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new IdentityUser
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new { message = "User registered successfully" });
        }
    }
}
