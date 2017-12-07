using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PreSchoolApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PreSchoolApp.Controllers
{
    public class TeacherController : Controller
    {
        PreSchoolDBRepository repository;

        public TeacherController(PreSchoolDBRepository repository)
        {
            this.repository = repository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            //var model = TestChildrenRepo.GetTestData();
            var model = repository.GetTodaysSchedules();
            return View(model);
        }
    }
}
