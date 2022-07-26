using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Persistence.IdentityModels;
using Application.Common.Interface;
using Domain.Main.DTO;
using Domain.Main;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ILogger<UserController> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterUserRequest userDetails)
        {
            if (ModelState.IsValid)
            {
                var userDetailToBeCreated = new User
                {
                    UserName = userDetails.Email,
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    Email = userDetails.Email,
                    Gender = userDetails.Gender,
                };

                var response = await _userManager.CreateAsync(userDetailToBeCreated, userDetails.Password);

                if (response.Succeeded)
                {
                    return Ok("User Created Successfully.");
                }
                else
                {
                    return BadRequest(response.Errors);
                }
            }
            return BadRequest();

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("AssignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleRequest userDetails)
        {
            if (ModelState.IsValid)
            {

                var response = await _userManager.FindByNameAsync(userDetails.Email);

                if (response != null)
                {

                    if (await _userManager.IsInRoleAsync(response, userDetails.RoleName))
                    {
                        return BadRequest("This Role Already Assigned To User");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(response, userDetails.RoleName);
                    }
                    return Ok("Role Assigned to User.");
                }
                else
                {
                    return BadRequest("Something went wrong");
                }
            }
            return BadRequest();

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleRequest rolerRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = rolerRequest.Role,
                });

                if (response.Succeeded)
                {

                    return Ok("Role Created Successfully.");
                }
                else
                {
                    return BadRequest(response.Errors);
                }
            }
            return BadRequest();

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(LoginModel loginDetail)
        {

            if (loginDetail is null) return Unauthorized();
            if (!ModelState.IsValid) return Unauthorized();

            var user = await _userManager.FindByNameAsync(loginDetail.UserName);
            if (user == null) return Unauthorized();

            bool isValidPassword = await _userManager.CheckPasswordAsync(user, loginDetail.Password);
            if (isValidPassword)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

                var role = await _userManager.GetRolesAsync(user);

                int? roleID = null;
                switch (role?.FirstOrDefault()?.ToLower())
                {
                    case "individualcontributor":
                        roleID = (int)RoleDetails.IndividualContributor;
                        break;
                    case "manager":
                        roleID = (int)RoleDetails.Manager;
                        break;
                    case "admin":
                        roleID = (int)RoleDetails.Admin;
                        break;
                }

                var useDetail = new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = role?.FirstOrDefault(),
                    RoleID = roleID
                };

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _configuration["JWT:Issuer"],
                    Audience = _configuration["JWT:Audience"],
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new AuthenticateResponse { Token = tokenHandler.WriteToken(token), UserDetail = useDetail });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.Select(f => new UserListResponse
            {
                Email = f.Email,
                FirstName = f.FirstName,
                LastName = f.LastName,
                Gender = f.Gender
            }).ToList();
            return Ok(users);
        }
    }
}