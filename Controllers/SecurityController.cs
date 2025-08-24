using Microsoft.AspNetCore.Http;
using TaskManagementSystem.Task.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.SqlLite;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagementSystem.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        public IConfiguration _config;
        public AppDbContext _context;
        public SecurityController(IConfiguration configuration, AppDbContext context)
        {
            _config = configuration;
            _context = context;
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="request">The user registration details (username, email, password).</param>
        /// <returns>
        /// Returns a success response if the registration is successful.  
        /// If the email is already taken or validation fails, returns a BadRequest.
        /// </returns>
        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserRequest request)
        {
            // Hash password before saving
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new UserEF
            {
                Username = request.Username,
                Email = request.Email,
                Password = hashedPassword
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully");
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>Returns a list of users.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var usersList = _context.Users.ToList();
            return Ok(usersList);
        }

        /// <summary>
        ///  Deletes a Users by ID.
        /// </summary>
        /// <param name="id">ID of the Users</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("GetUserDeleteById")]
        public IActionResult GetUserDeleteById([Required]int id)
        {
           var deleteUsers = _context.Users.Find(id);
            if (deleteUsers == null) return NotFound();
            _context.Users.Remove(deleteUsers);
            _context.SaveChanges();
            return Ok();

        }

        /// <summary>
        /// Authenticates a user with the given credentials.
        /// </summary>
        /// <param name="Email">The email or username of the user.</param>
        /// <param name="Password">The password of the user.</param>
        /// <returns>
        /// Returns a JWT token if the login is successful, otherwise returns Unauthorized (401).
        /// </returns>
        [HttpPost("login")]
        public IActionResult Login([FromQuery] string Email, string Password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == Email);
            if (user != null && BCrypt.Net.BCrypt.Verify(Password, user.Password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["JWT:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, Email),
                new Claim(ClaimTypes.Role, "Customer")
            }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new { Token = tokenHandler.WriteToken(token) });
            }

            return Unauthorized();
        }

    }
}
