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

        //public ParentStartVM GetYourChild(LoginVM loginVM)
        //{
        //    int weekDay = (int)DateTime.Today.DayOfWeek;

        //    var childrenOfParent = context.Users
        //        .Where(o => o.Id == loginVM.UserName)
        //        .SelectMany(o => o.C2p.Select(op => op.Cid))
                
            

        //    return null;
        //}

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
                        Id = o.Children.Id,
                        IsActive = (bool)o.Children.IsIll
                    })
                    .OrderBy(o => o.IsPresent)
                    .ToArray()
            };

            ret.PresentChildrenCount = ret.ChildItems
                .Count(o => o.IsPresent && o.IsActive == false);

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

            if (itemToUpdate.IsPresent == false && itemToUpdate.IsIll == false)
            {
                itemToUpdate.IsPresent = true;
            }
            else if (itemToUpdate.IsPresent == true && itemToUpdate.IsIll == false)
            {
                itemToUpdate.IsIll = true;
            }
            else if (itemToUpdate.IsIll == true && itemToUpdate.IsPresent == true)
            {
                itemToUpdate.IsIll = false;
                itemToUpdate.IsPresent = false;
            }

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
                .Single(c => c.UserName == username)
                .Id;

            return aspId;
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

        public void AddParent(EditUserVM edituser)           
        {
            string aspnetId = GetASPID(edituser.FirstName);
            context.Users.Add(new Users {FirstName= edituser.FirstName, LastName= edituser.LastName, AspId = aspnetId });
            
            context.SaveChanges();

            var userID = context.Users.SingleOrDefault(x => x.AspId == aspnetId);
            
            context.C2p.Add(new C2p { Uid = userID.Id, Cid = edituser.ChildID });
            context.SaveChanges();
        }


        public bool IsParent(LoginVM loginVM)
        {
            string aspId = GetASPID(loginVM.UserName);
            int userId = GetUserID(aspId);


            //int childId = GetChildId(userId);

            return context.C2p
                .Any(x => x.Uid == userId);


        }

        private int GetUserID(string aspId)
        {
            return context.Users
                .SingleOrDefault(x => x.AspId == aspId).Id;
        }

        private int GetChildId(int userId)
        {

            return context.C2p
                .SingleOrDefault(x => x.Uid == userId).Cid;
        }
    }
}
