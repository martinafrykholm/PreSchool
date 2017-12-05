using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PreSchoolApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PreSchoolApp.Controllers
{
    public class LoginController : Controller
    {

        LoginVM login;
        IdentityDbContext identityContext;
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        RoleManager<IdentityRole> roleManager;

        public LoginController(IdentityDbContext identityContext,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {
            this.identityContext = identityContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;

            identityContext.Database.EnsureCreated();
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(HomeLoginVM loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            //Här skapas cookien
            var result = await signInManager.PasswordSignInAsync(
            loginVM.UserName, loginVM.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(
                    nameof(HomeLoginVM.UserName), "Felaktigt namn eller lösenord"); //lägger in felmeddelande                
                return View(loginVM);
            }

            return RedirectToAction(nameof(Search));
        }
    }
}
