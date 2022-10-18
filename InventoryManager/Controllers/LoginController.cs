using Infraestructure.Security;
using InventoryManager.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InventoryManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        HelperToken helper;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            this.helper = new HelperToken(configuration);
            _configuration = configuration;
        }

        //RECIBIMOS CON LA CLASE LOGINMODEL EL USUARIO Y EL PASSWORD Y DEVOLVEMOS EL TOKEN O UNATHORIZED DEPENDIENDO DE SI LAS CREDENCIALES SON O NO CORRECTAS
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            User user = new User
            {
                UserName = model.UserName,
                Password = model.Password,
                Role = "User"
            };

            if (AuthorizedUser(user))
            {
                var claims = new Claim[]
                {
                new Claim(ClaimTypes.Role,user.Role)
                };

                JwtSecurityToken token = new JwtSecurityToken
                    (
                     issuer: helper.Issuer
                     , audience: helper.Audience
                     , claims: claims
                     , expires: DateTime.UtcNow.AddMinutes(10)
                     , notBefore: DateTime.UtcNow
                     , signingCredentials:
                    new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256)
                    );
                return Ok(
                    new
                    {
                        response =
                        new JwtSecurityTokenHandler().WriteToken(token)
                    });
            }
            else
            {
                return Unauthorized();
            }
        }

        private bool AuthorizedUser(User user)
        {
            return user.UserName == _configuration["FakeUser:User"] && user.Password == _configuration["FakeUser:Password"];
        }
    }
}