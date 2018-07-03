using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProductsCatalog.Models;
using ProductsCatalog.ViewModels;

namespace ProductsCatalog.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public UserController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        // GET api/values/5
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            ApplicationUser _user = await GetUser();
            if (_user != null)
            {
                UserViewModel userViewModel = new UserViewModel()
                {
                    FullName = _user.Name,
                    Username = _user.UserName,
                    SavedSearches = _user.SavedSearches
                };
                    
                return Ok(userViewModel);
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("save-product-searches")]
        public async Task<IActionResult> SaveProductSearches([FromBody] UserViewModel userViewModel)
        {
            ApplicationUser _user = await GetUser();
            if (_user != null)
            {
                _user.SavedSearches = userViewModel.SavedSearches;
                var result = await this.userManager.UpdateAsync(_user);
            }
            return Ok();
        }

        private async Task<ApplicationUser> GetUser()
        {
            ApplicationUser _user = null;
            var claims = User.Claims;
            string name = "";
            foreach (var c in claims)
            {
                if (c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                {
                    name = c.Value;
                }
            }
            if (name != "")
            {
                _user = await this.userManager.FindByNameAsync(name);
            }

            return _user;
        }
    }
}