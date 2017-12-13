using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PreSchoolApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PreSchoolApp.Controllers
{
    public class HomeController : Controller
    {
        PreSchoolDBRepository repository;

        public HomeController(PreSchoolDBRepository repository)
        {
            this.repository = repository;
        }

        // GET: /<controller>/
        public IActionResult Index(int temp)
        {
            repository.GetTodaysSchedules();
            return View();
        }

        [HttpPost]
        public IActionResult Index()
        {
            //int temp = SQLLibrary_new.SqlClass.AddChild("Nova", "Lo", 4);
            return View();
        }
    }
}
