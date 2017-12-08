using PreSchoolApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreSchoolApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace PreSchoolApp.Models
{
    public class PreSchoolDBRepository
    {
        private const int seconds = 0;
        PreSchoolAppContext context;

        public PreSchoolDBRepository(PreSchoolAppContext context)
        {
            this.context = context;
        }

        public TeacherStartVM GetTodaysSchedules()
        {
            int weekDay = (int)DateTime.Today.DayOfWeek;

            var ret = new TeacherStartVM
            {
                ChildItems = context.Schedules
                    .Where(o => o.Weekdays == weekDay)
                    .Select(o => new TeacherStartChildItemVM
                    {
                        DropOfTime = o.Dropoff.Value,
                        PickupTime = o.PickUp.Value,
                        IsPresent = o.Children.IsPresent,
                        FirstName = o.Children.FirstName,
                        LastName = o.Children.LastName,
                        Id = o.Children.Id
                    })
                    .OrderBy(o => o.IsPresent)
                    .ToArray()
            };

            ret.PresentChildrenCount = ret.ChildItems
                .Count(o => o.IsPresent);

            ret.NotPresentChildrenCount = ret.ChildItems
                .Count(o => o.IsPresent == false);

            ret.NotActiveCount = ret.ChildItems
                .Count(o => o.IsActive);

            return ret;
        }

        public void SetChildPresence(int childId)
        {
            var itemToUpdate = context.Children
                .SingleOrDefault(x => x.Id == childId);

            if (itemToUpdate.IsPresent)
            {
                itemToUpdate.IsPresent = false;
            }
            else
                itemToUpdate.IsPresent = true;

            context.SaveChanges();
        }

        public void AddChild(string firstName, string lastName, int unitId)
        {
            context.Children.Add(new Children
            {
                FirstName = firstName,
                LastName = lastName,
                UnitsId = unitId
            });
            context.SaveChanges();
        }

        public void AddPreSchool(string preschoolName)
        {
            context.PreSchools.Add(new PreSchools { PreSchoolName = preschoolName });
            context.SaveChanges();
        }

        public void AddUnit(string unitName, int preschoolID)
        {
            context.Units.Add(new Units { UnitName = unitName, PreSchoolsId = preschoolID });
            context.SaveChanges();
        }

        public void AddTeacher(string firstName, string lastName, string aspId, int unitId)
        {
            context.Users.Add(new Users { FirstName = firstName, LastName = lastName, AspId = aspId, UnitsId = unitId });
            context.SaveChanges();
        }

        //private int Get
        public string GetASPID(string username)
        {
            var aspId = context.AspNetUsers
                .Where(c => c.UserName == username)
                .Select(c => c.Id);

            return aspId.ToString();
        }

        private int GetScheduleID(int childId, int weekDayNr)
        {

            var scheduleID = context.Schedules
.SingleOrDefault(x => x.Weekdays == weekDayNr && x.ChildrenId == childId);

            return Convert.ToInt32(scheduleID);
        }


        public void EditSchedule(int dropOffHrs, int dropOffMins, int pickUpHrs, int pickUpMins, int weekdayNr, int childId)
        {

            int scheduleItemId = GetScheduleID(childId, weekdayNr);

            TimeSpan dropOff = new TimeSpan(dropOffHrs, dropOffMins, seconds);
            TimeSpan pickUp = new TimeSpan(pickUpHrs, pickUpMins, seconds);

            var itemToUpdate = context.Schedules
                .SingleOrDefault(x => x.Id == scheduleItemId);

            itemToUpdate.Dropoff = dropOff;
            itemToUpdate.PickUp = pickUp;

            context.SaveChanges();

        }

        public void AddParent(string firstName, string lastName, string aspId, int childId)
        {

            context.Users.Add(new Users { FirstName = firstName, LastName = lastName, AspId = aspId });


            context.SaveChanges();

            var userID = context.Users.SingleOrDefault(x => x.AspId == aspId);


            context.C2p.Add(new C2p { Uid = userID.Id, Cid = childId });
            context.SaveChanges();
        }
    }
}
