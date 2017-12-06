using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PreSchoolApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PreSchoolApp.Controllers
{
    public class CreateController : Controller
    {

        IdentityErrorDescriber errorDescriber = new IdentityErrorDescriber();
        IdentityError error = new IdentityError();
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        RoleManager<IdentityRole> roleManager;

        public CreateController
            (
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(CreateUserVM createUserVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result =
                await userManager.CreateAsync(new IdentityUser(createUserVM.UserName), createUserVM.PassWord);

            if (!result.Succeeded)
            {
                //var needsUpper = errorDescriber.PasswordRequiresUpper();
                //ModelState.AddModelError(
                //    nameof(CreateUserVM.PassWord), needsUpper.ToString());

                ModelState.AddModelError(
                    nameof(CreateUserVM.PassWord), "Felaktigt lösenord");
                //var toShort = errorDescriber.PasswordTooShort(7);
                //ModelState.AddModelError(
                //    nameof(CreateUserVM.PassWord), toShort.ToString());
                         
                return View();

            }


            return RedirectToAction(nameof(Index));
        }
    }
}
