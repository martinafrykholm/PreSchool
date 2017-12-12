using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PreSchoolApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using SQLLibrary_new;
using PreSchoolApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PreSchoolApp.Controllers
{
    public class CreateController : Controller
    {
        const string RoleParent = "Parent";
        const string RoleTeacher = "Teacher";
        const string RoleAdmin = "Admin";


        IdentityErrorDescriber errorDescriber = new IdentityErrorDescriber();
        IdentityError error = new IdentityError();
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        RoleManager<IdentityRole> roleManager;
        PreSchoolDBRepository repository;



        public CreateController
            (
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            PreSchoolDBRepository repository
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.repository = repository;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            //var result = await roleManager.CreateAsync(new IdentityRole(RoleParent));
            //var result2 = await roleManager.CreateAsync(new IdentityRole(RoleTeacher));
            //var result3 = await roleManager.CreateAsync(new IdentityRole(RoleAdmin));

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
            var user = new IdentityUser(createUserVM.UserName);
            var result =
                await userManager.CreateAsync(user, createUserVM.PassWord);
            //new IdentityUser(createUserVM.UserName), createUserVM.PassWord);

            if (!result.Succeeded)
            {
                //var needsUpper = errorDescriber.PasswordRequiresUpper();
                //ModelState.AddModelError(
                //    nameof(CreateUserVM.PassWord), needsUpper.ToString());

                ModelState.AddModelError(
                    nameof(CreateUserVM.PassWord), "Felaktigt format på lösenord");
                //var toShort = errorDescriber.PasswordTooShort(7);
                //ModelState.AddModelError(
                //    nameof(CreateUserVM.PassWord), toShort.ToString());

                return View();

            }
            await userManager.AddToRoleAsync(user, RoleParent);

            int childCode = createUserVM.ChildCode;
            //repository.ChildToParent(createUserVM);


            return RedirectToAction("EditUser", new { ChildCode = childCode });
        }

        [HttpGet]
        public IActionResult EditUser(int ChildCode)
        {
            ViewBag.ChildCode = ChildCode;
            return View();
        }

        public IActionResult EditUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditUser(EditUserVM editUserVM, int ChildCode)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ChildCode = ChildCode;
                return View();
            }

            int childid = ChildCode;
            //string aspID = repository.GetASPID(editUserVM.FirstName);

            //var userId = userManager.GetUserId(HttpContext.User);
            repository.AddParent(editUserVM, childid);

            return RedirectToAction("Index", "Teacher");
        }
    }
}
