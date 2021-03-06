﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PreSchoolApp.Models;
using PreSchoolApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PreSchoolApp.Controllers
{
    [Authorize(Roles = "Parent")]
    public class ParentController : Controller
    {
        PreSchoolDBRepository repository;

        public ParentController(PreSchoolDBRepository repository)
        {
            this.repository = repository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var userName = User.Identity.Name;
            //var model = TestRepo.GetTestParentStartVM(id);
            var model = repository.GetParentStartVM(userName);

            return View(model);
        }

        //public IActionResult Index()
        //{
        //    //var model = TestRepo.GetTestParentStartVM();
        //    var model = repository.GetParentStartVM(1);
        //    //var model = repository.GetYourChild(9);

        //    return View(model);
        //}

        public IActionResult Report(int id)
        {
            var model = repository.GetParentReportVM(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Calendar(int id)
        {
            //var model = TestRepo.GetTestParentCalendarVM(id);
            var model = repository.GetChildsSchedule(id);

            return View(model);
        }

        [HttpPost, Route("UpdateCalendar/{id}/{weekDay}")]
        public IActionResult UpdateCalendar(int id, int weekDay, TimeSpan? pickUpTime, TimeSpan? dropOffTime)
        {
            //Metod: Uppdatera barnets kalender
            //TestRepo.UpdateCalendar(id, weekDay, isDropOff, time);
            repository.UpdateChildCalendar(id, weekDay, pickUpTime, dropOffTime);

            //return RedirectToAction("Index");
            return RedirectToAction("Calendar", new { id = id });
        }

        [HttpPost]
        public IActionResult ReportDelay(int id, int delay)
        {
            repository.AddDelayTime(id, delay);
            return RedirectToAction("Report", new { id = id });
        }

        [HttpPost]
        public IActionResult ReportIllness(int id)
        {
            repository.ReportSick(id);
            return RedirectToAction("Report", new { id = id });
        }
    }
}
