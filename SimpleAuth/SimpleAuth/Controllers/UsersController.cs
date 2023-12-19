using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Data;
using SimpleAuth.Enums;
using SimpleAuth.Models;
using SimpleAuth.Services;

namespace SimpleAuth.Controllers
{
    /// <summary>
    /// Manages user registration and authentication.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthContext _context;
        private readonly ITokenService _tokenService;

        public UsersController(UserManager<ApplicationUser> userManager, AuthContext context, ITokenService tokenService, ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _context = context;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/users/register
        ///     {
        ///        "username": "john_doe",
        ///        "password": "password123",
        ///        "email": "john.doe@example.com",
        ///        "role": "admin"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">The registration model.</param>
        /// <returns>Returns 201 Created on successful registration.</returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.CreateAsync(
                new ApplicationUser { UserName = request.Username, Email = request.Email, Role = Role.User },
                request.Password
            );
            if (result.Succeeded)
            {
                request.Password = "";
                return CreatedAtAction(nameof(Register), new { email = request.Email, role = request.Role }, request);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/account/login
        ///     {
        ///        "email": "john.doe@example.com",
        ///        "password": "password123"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">The login model.</param>
        /// <returns>Returns 200 OK on successful login.</returns>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var managedUser = await _userManager.FindByEmailAsync(request.Email);
            if (managedUser == null)
            {
                return BadRequest("Username Incorrect");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);
            if (!isPasswordValid)
            {
                return BadRequest("Password Incorrect");
            }

            var userInDb = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (userInDb is null)
            {
                return Unauthorized();
            }

            var accessToken = _tokenService.CreateToken(userInDb);
            await _context.SaveChangesAsync();

            return Ok(new AuthResponse
            {
                Username = userInDb.UserName,
                Email = userInDb.Email,
                Token = accessToken,
            });
        }
    }
}
