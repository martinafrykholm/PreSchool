using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PreSchoolApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using SQLLibrary_new;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PreSchoolApp.Controllers
{
    public class LoginController : Controller
    {
        
       
        //IdentityDbContext identityContext;
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        RoleManager<IdentityRole> roleManager;

        public LoginController(//IdentityDbContext identityContext,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {
            //this.identityContext = identityContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;

           // identityContext.Database.EnsureCreated();
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
                return View();

            //Här skapas cookien
            var result = await signInManager.PasswordSignInAsync(
            loginVM.UserName, loginVM.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(
                    nameof(LoginVM.UserName), "Felaktigt namn eller lösenord"); //lägger in felmeddelande                
                return View();

                
            }
           
 
            return RedirectToAction(nameof(TeacherController));
        }


    }
}
