using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using userManagement_API.Repository;
using userManagement_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace userManagement_API.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        private IBooksRepository _booksRepo ;

        public LoginController(IConfiguration config, IBooksRepository bookRepository)
        {
            _configuration = config;
            _booksRepo = bookRepository;
        }


      

        // POST api/<LoginController>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserInfo login)
        {
            IActionResult reponse = Unauthorized();
            var user = AuthenticateUser(login);

            if(user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                reponse = Ok(new { token = tokenString });
            }
            return reponse;

        }

        private string GenerateJSONWebToken(UserInfo userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                null,
                expires:DateTime.Now.AddMinutes(120),
                signingCredentials:credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private UserInfo AuthenticateUser(UserInfo login)
        {
            UserInfo user = null;
            user = _booksRepo.GetUserInfo(login.Email,login.Password);
            return user;
        }

        
      
    }
}
