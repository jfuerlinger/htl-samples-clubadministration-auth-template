using ClubAdministration.Core.Entities;
using ClubAdministration.Web.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Web.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<Member> _userManager;

        public AuthController(
            IConfiguration configuration,
            UserManager<Member> userManager)
        {
            _config = configuration;
            _userManager = userManager;
        }

        /// <summary>
        /// Benutzer im System anmelden
        /// </summary>
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] CredentialDto credentials)
        {
            return Ok();
        }


        /// <summary>
        /// JWT erzeugen. Minimale Claim-Infos: Email und Rolle
        /// </summary>
        /// <returns>Token mit Claims</returns>
        private string GenerateJwtToken(string email, IEnumerable<string> roles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email)
            };

            foreach (string role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Audience"],
              claims: authClaims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Neuen Benutzer registrieren. 
        /// </summary>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public ActionResult Register(UserDto newUser)
        {
            return Ok();
        }
    }
}
