using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductsCatalog.Models;
using ProductsCatalog.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductsCatalog.Controllers
{
    [Route("api")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public AuthController(UserManager<ApplicationUser> userManager, 
                                SignInManager<ApplicationUser> signInManager,
                                IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignUp([FromBody] SignupModel signupModel)
        {
            var result = await this.userManager.CreateAsync(new ApplicationUser()
                                                                {
                                                                    UserName = signupModel.UserName,
                                                                    Email = signupModel.Email
                                                                }, signupModel.Password);

            if (result.Succeeded)
            {
                return Content($"account created for {signupModel.UserName}");
            }

            return Content($"account creation for {signupModel.UserName} falied!");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn([FromBody] LoginModel loginModel)
        {
            var result = await this.signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, true, false);
            if (result.Succeeded)
            {
                var _user = await this.userManager.FindByNameAsync(loginModel.UserName);
                
                var claims = new[]{
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, loginModel.UserName),
                    new Claim(JwtRegisteredClaimNames.Email,_user.Email),
                    new Claim("my-custom-claim","my-custom-value"),
                    new Claim(ClaimTypes.Name, loginModel.UserName),
                    new Claim(ClaimTypes.GivenName, _user.Name)
                };

                var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:SecretKey"])), SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                        issuer: this.configuration["Jwt:Issuer"],
                        audience: this.configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMonths(3),
                        signingCredentials: credentials
                    );


                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    username=loginModel.UserName,
                    name=loginModel.UserName
                });
            }

            return Content($"login for {loginModel.UserName} failed");
        }
    }
}
