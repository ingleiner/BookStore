using BookStore.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountsController (UserManager<IdentityUser> userManager,
            IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }
        
        [HttpPost("register")] //api/accounts/register
        public async Task<ActionResult<AuthenticationResponse>> Register(UserCredential userCredential)
        {
            var user = new IdentityUser
            {
                UserName = userCredential.Email,
                Email = userCredential.Email
            };
            var result = await _userManager.CreateAsync(user, userCredential.Password );
            
            if(result.Succeeded)
            {
                return BuildToken(userCredential);
            }
            else
            {
                return BadRequest(result.Errors);
            }

        }
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(UserCredential userCredential)
        {
            var result = await _signInManager.PasswordSignInAsync(userCredential.Email,
                userCredential.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return BuildToken(userCredential);
            }
            else
            {
                return BadRequest("Login Incorrecto");
            }

        }

        //Respuesta a autenticación de usuario
        private AuthenticationResponse BuildToken(UserCredential userCredential)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", userCredential.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            return new AuthenticationResponse
            {
                Token =new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration,
            };

        }
    }
}
