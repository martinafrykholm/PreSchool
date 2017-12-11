using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PreSchoolApp.Models;
using PreSchoolApp.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PreSchoolApp.Controllers
{
    public class ParentController : Controller
    {
        PreSchoolDBRepository repository;

        public ParentController(PreSchoolDBRepository repository)
        {
            this.repository = repository;
        }

        // GET: /<controller>/
        //public IActionResult Index(LoginVM loginVM)
        //{
        //    var model = repository.GetYourChild(loginVM);
        //    return View(model);
        //}

        public IActionResult Index(int id)
        {
            var model = TestRepo.GetTestParentStartVM(id);            

            return View(model);
        }
                
        public IActionResult Report()
        {
            //var model = TestRepo.GetTestParentStartVM(id);

            var model = TestRepo.GetTestParentReportData();
            return View(model);
        }

        [HttpGet]
        public IActionResult Calendar(int id)
        {
            var model = TestRepo.GetTestParentCalendarVM(id);

            return View(model);
        }

        [HttpPost, Route("UpdateCalendar/{id}/{weekDay}")]
        public IActionResult UpdateCalendar(int id, int weekDay, TimeSpan? pickUpTime, TimeSpan? dropOffTime)
        {
            //Metod: Uppdatera barnets kalender
            //TestRepo.UpdateCalendar(id, weekDay, isDropOff, time);
            //var model = TestRepo.GetTestParentCalendarVM(id);        
            repository.EditSchedule(id, weekDay, pickUpTime, dropOffTime);

            return RedirectToAction(nameof(Calendar));
        }

        [HttpPost]
        public IActionResult ReportDelay(int id, int delay)
        {
            return RedirectToAction(nameof(Report));
        }

        [HttpPost]
        public IActionResult ReportIllness(int id)
        {
            return RedirectToAction(nameof(Report));
        }
    }
}
