using PreSchoolApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreSchoolApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

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

        public ParentCalendarVM GetChildsSchedule(int id)
        {
            var childsSchedule = context.Schedules
                .Select(x => x.ChildrenId == id);

            ParentCalendarVM parentCalendar = new ParentCalendarVM
            {
                FirstName = GetChildNameFromID(id),
                ChildId = id,
                AllTimes = Utils.GetAllTimes(),
                Weekdays = Utils.GetWeekdays(),
                DropOffTimes = GetDropOffTimes(id),
                PickupTimes = GetPickUpTimes(id)
            };
            return parentCalendar;
        }

        private TimeSpan[] GetPickUpTimes(int childID)
        {
            List<TimeSpan> pickUpTimes = new List<TimeSpan>();

            var times = context.Schedules
                .Where(x => x.ChildrenId == childID)
                .Select(o => o.PickUp.Value)
                .ToArray();
            
            return times;
        }

        private TimeSpan[] GetDropOffTimes(int childID)
        {
            List<TimeSpan> dropOffTimes = new List<TimeSpan>();

            var times = context.Schedules
                .Where(x => x.ChildrenId == childID)
                .Select(o => o.Dropoff.Value)
                .ToArray();

            return times;
        }

        internal void ReportSick(int childId)
        {
            var itemToUpdate = context.Children
                .SingleOrDefault(x => x.Id == childId);

            if ((bool)itemToUpdate.IsIll)
            {
                itemToUpdate.IsIll = false;
                itemToUpdate.IsPresent = false;
            }
            else
            {
                itemToUpdate.IsIll = true;
                itemToUpdate.IsPresent = true;
            }

            context.SaveChanges();
        }

        public ParentStartVM[] GetYourChild(int userId)
        {
            int weekDay = (int)DateTime.Today.DayOfWeek;
            ParentStartVM startVM = new ParentStartVM();
          
            var childrenOfParent = GetChildrenId(userId);

            List<ParentStartVM> parentStartVM = new List<ParentStartVM>();

            foreach (var item in childrenOfParent)
            {
                int intItem = Convert.ToInt32(item);
                var child = context.Children
                    .SingleOrDefault(x => x.Id == intItem);
                parentStartVM.Add(new ParentStartVM
                {
                    FirstName = child.FirstName,
                    Id = child.Id,
                    MinutesLate = child.MinLate.Value,
                    IsActive = child.IsIll.Value,
                    IsPresent = child.IsPresent,
                    DropOfTime = context.Schedules.SingleOrDefault(x => x.Id == intItem && x.Weekdays == weekDay).Dropoff.Value,
                    PickupTime = context.Schedules.SingleOrDefault(x => x.Id == intItem && x.Weekdays == weekDay).PickUp.Value
                });
            }
            return parentStartVM.ToArray();
        }

        public ParentStartVM[] GetParentStartVM(string username)
        {
            string aspId = GetASPID(username);
            int userId = GetUserID(aspId);
            int[] childIds = GetChildrenId(userId);

            int weekDay = (int)DateTime.Today.DayOfWeek;

            var ret = context.Schedules
                .Where(o => o.Weekdays == weekDay && childIds.Contains(o.Children.Id))
                .Select(schedule => new ParentStartVM
                {
                    DropOfTime = schedule.Dropoff,
                    PickupTime = schedule.PickUp,
                    FirstName = schedule.Children.FirstName,
                    Id = schedule.Children.Id,
                    IsActive = schedule.Children.IsIll == null ? false : (bool)schedule.Children.IsIll,
                    IsPresent = schedule.Children.IsPresent
                })
                .ToArray();

            return ret;
        }

        public ParentReportVM GetParentReportVM(int childId)
        {
            int weekDay = (int)DateTime.Today.DayOfWeek;

            var child = context.Children
                .Include(o => o.Schedules)
                .Where(c => c.Id == childId)
                .SingleOrDefault();

            ParentReportVM parentReportVM = new ParentReportVM();

            foreach (Schedules schedule in child.Schedules)
            {
                if (schedule.Weekdays == weekDay)
                {
                    parentReportVM.DropOffTime = schedule.Dropoff == null ? default(TimeSpan) : (TimeSpan)schedule.Dropoff;
                    parentReportVM.PickupTime = schedule.PickUp == null ? default(TimeSpan) : (TimeSpan)schedule.PickUp;
                    parentReportVM.FirstName = schedule.Children.FirstName;
                    parentReportVM.ChildId = schedule.Children.Id;
                    parentReportVM.IsActive = schedule.Children.IsIll == null ? false : (bool)schedule.Children.IsIll;
                    parentReportVM.IsPresent = schedule.Children.IsPresent;
                    parentReportVM.MinLate = (int)schedule.Children.MinLate;
                }
            }
            return parentReportVM;
        }

        public TeacherStartVM GetTodaysSchedules()
        {
            int weekDay = (int)DateTime.Today.DayOfWeek;

            var items = context.Schedules
                    .Where(o => o.Weekdays == weekDay)
                    .Select(o => new TeacherStartChildItemVM
                    {
                        DropOfTime = o.Dropoff.Value,
                        PickupTime = o.PickUp.Value,
                        IsPresent = o.Children.IsPresent,
                        FirstName = o.Children.FirstName,
                        LastName = o.Children.LastName,
                        Id = o.Children.Id,
                        IsActive = (bool)o.Children.IsIll,
                        MinutesLate = o.Children.MinLate == null ? default(int) : (int)o.Children.MinLate //ta bort vid strul
                    })
                    .OrderBy(o => o.DropOfTime)
                    .ToArray();

            foreach (var child in items)
            {
                if (child.MinutesLate > 0)
                    UpdateChildTime(child);
            }

            return new TeacherStartVM
            {
                CheckInChildItems = items
                    .Where(o => o.IsPresent && !o.IsActive)
                    .OrderBy(o => o.PickupTime)
                    .ToArray(),
                NotCheckedInChildItems = items
                    .Where(o => !o.IsPresent && !o.IsActive)
                    .OrderBy(o => o.DropOfTime)
                    .ToArray(),
                CheckOutChildItems = items
                    .Where(o => !o.IsPresent && o.IsActive)
                    .OrderBy(o => o.DropOfTime)
                    .ToArray(),
                SickChildren = items
                    .Where(o => o.IsPresent && o.IsActive)
                    .OrderBy(o => o.FirstName)
                    .ToArray()
            };
        }

        private void UpdateChildTime(TeacherStartChildItemVM child)
        {
            TimeSpan tmp = new TimeSpan(0, child.MinutesLate, 00);

            if (child.IsPresent == false)
            {
                child.DropOfTime += tmp;
            }
            else if (child.IsPresent == true)
            {
                child.PickupTime += tmp;
            }
        }

        public void SetChildPresence(int childId)
        {
            var itemToUpdate = context.Children
                .SingleOrDefault(x => x.Id == childId);

            if (itemToUpdate.IsPresent == false && itemToUpdate.IsIll == false)
            {
                itemToUpdate.IsPresent = true;
                itemToUpdate.MinLate = 0;
            }
            else if (itemToUpdate.IsPresent == true && itemToUpdate.IsIll == false)
            {
                itemToUpdate.IsIll = true;
                itemToUpdate.IsPresent = false;
                itemToUpdate.MinLate = 0;
            }
            else if (itemToUpdate.IsIll == true && itemToUpdate.IsPresent == true)
            {
                itemToUpdate.IsIll = false;
                itemToUpdate.IsPresent = false;
            }
            else
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

        public void UpdateChildCalendar(int id, int weekDay, TimeSpan? pickUpTime, TimeSpan? dropOffTime)
        {
            var itemToUpdate = context.Schedules
                .SingleOrDefault(x => x.ChildrenId == id && x.Weekdays == weekDay + 1);

            if (pickUpTime != null)
            {
                itemToUpdate.PickUp = pickUpTime;
            }
            else if (dropOffTime != null)
            {
                itemToUpdate.Dropoff = dropOffTime;
            }
            context.SaveChanges();
        }
 
        public void AddParent(EditUserVM edituser, int childCode, string name)
        {
            string aspnetId = GetASPID(name);
            context.Users.Add(new Users { FirstName = edituser.FirstName, LastName = edituser.LastName, AspId = aspnetId });

            context.SaveChanges();
            
            var userID = context.Users.SingleOrDefault(x => x.AspId == aspnetId);
        
            context.C2p.Add(new C2p
            {
                Uid = userID.Id,
                Cid = childCode
            });
            context.SaveChanges();
        }

        public bool IsParent(LoginVM loginVM)
        {
            string aspId = GetASPID(loginVM.UserName);
            int userId = GetUserID(aspId);

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

        private int[] GetChildrenId(int userId)
        {
            return context.C2p
                .Where(x => x.Uid == userId)
                .Select(x => x.Cid)
                .ToArray();
        }

        private string GetChildNameFromID(int childID)
        {
            return context.Children
                .SingleOrDefault(x => x.Id == childID).FirstName;
        }
        
        public void AddDelayTime(int childID, int delay)
        {
            var itemToUpdate = context.Children
                .SingleOrDefault(x => x.Id == childID);

            itemToUpdate.MinLate = delay;

            context.SaveChanges();
        }
    }
}
