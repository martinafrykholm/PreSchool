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

            //foreach (var item in times)
            //{
            //    pickUpTimes.Add((TimeSpan)item.PickUp);
            //}

            //return pickUpTimes.ToArray();
            return times;
        }

        private TimeSpan[] GetDropOffTimes(int childID)
        {
            List<TimeSpan> dropOffTimes = new List<TimeSpan>();

            var times = context.Schedules
                .Where(x => x.ChildrenId == childID)
                .Select(o => o.Dropoff.Value)
                .ToArray();

            //foreach (var item in times)
            //{
            //    dropOffTimes.Add(item.Dropoff.Value);
            //}

            //return dropOffTimes.ToArray();
            return times;
        }

        //public ParentStartVM[] GetYourChild(LoginVM loginVM)
        //{
        //    int weekDay = (int)DateTime.Today.DayOfWeek;
        //    ParentStartVM startVM = new ParentStartVM();
        //    string aspId = GetASPID(loginVM.UserName);
        //    int userId = GetUserID(aspId);

        //    var childrenOfParent = context.Users
        //        .Where(o => o.Id == userId)
        //        .Select(o => o.C2p.Select(op => op.Cid));


        //    foreach (var item in childrenOfParent)
        //    {
        //        int item2 = Convert.ToInt32(item);
        //        startVM.Id = item2;
        //    }


        //    List<ParentStartVM> parentStartVM = new List<ParentStartVM>();

        //    //List<Children> children = new List<Children>();

        //    foreach (var item in childrenOfParent)
        //    {
        //        int intItem = Convert.ToInt32(item);
        //        var child = context.Children
        //            .SingleOrDefault(x => x.Id == intItem);
        //        parentStartVM.Add(new ParentStartVM
        //        {
        //            FirstName = child.FirstName,
        //            LastName = child.LastName,
        //            DropOfTime = context.Schedules.SingleOrDefault(x => x.Id == intItem && x.Weekdays == weekDay).Dropoff.Value,
        //            PickupTime = context.Schedules.SingleOrDefault(x => x.Id == intItem && x.Weekdays == weekDay).PickUp.Value
        //        });
        //    }
        //    return parentStartVM.ToArray();
        //}

        //public ParentStartVM GetParentStartVM(LoginVM loginVM)
        //{
        //    string aspId = GetASPID(loginVM.UserName);
        //    int userId = GetUserID(aspId);
        //    int[] childIds = GetChildrenId(userId);

        //    int weekDay = (int)DateTime.Today.DayOfWeek;

        //    ParentStartVM parentStartVM = new ParentStartVM();
        //    List<ParentStartChildItemVM> pscivm = new List<ParentStartChildItemVM>();

        //    foreach (var item in childIds)
        //    {
        //        var ret = context.Schedules
        //            .Where(o => o.Weekdays == weekDay && o.Id == item)
        //            .Select(o => new ParentStartChildItemVM
        //            {
        //                DropOfTime = o.Dropoff.Value,
        //                PickupTime = o.PickUp.Value,
        //                FirstName = o.Children.FirstName,
        //                LastName = o.Children.LastName,
        //                Id = o.Children.Id,
        //                IsActive = o.Children.IsIll.Value,
        //                IsPresent = o.Children.IsPresent
        //            })
        //            .FirstOrDefault();
        //        pscivm.Add(ret);
        //    }

        //    parentStartVM.ChildItems = pscivm.ToArray();

        //    return parentStartVM;
        //}

        public ParentStartVM[] GetYourChild(int userId)
        {
            int weekDay = (int)DateTime.Today.DayOfWeek;
            ParentStartVM startVM = new ParentStartVM();
            //string aspId = GetASPID(loginVM.UserName);
            //int userId = GetUserID(aspId);

            //var childrenOfParent = context.Users
            //    .Where(o => o.Id == userId)
            //    .Select(o => o.C2p.Select(op => op.Cid));

            //foreach (var item in childrenOfParent)
            //{
            //    int item2 = Convert.ToInt32(item);
            //    startVM.Id = item2;
            //}

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

        //public ParentStartVM[] GetParentStartVM(int userId)
        //{
        //    //string aspId = GetASPID(loginVM.UserName);
        //    //int userId = GetUserID(aspId);
        //    int[] childIds = GetChildrenId(userId);

        //    int weekDay = (int)DateTime.Today.DayOfWeek;

        //    List<ParentStartVM> pscivm = new List<ParentStartVM>();

        //    for (int i = 0; i < childIds.Length; i++)
        //    {

        //        var tmp = context.Children
        //            .Include("Schedules")
        //            .Where(c => c.Id == childIds[i])
        //            .ToList();

        //        foreach (Children child in tmp)
        //        {
        //            foreach (Schedules schedule in child.Schedules)
        //            {
        //                if (schedule.Weekdays == weekDay)
        //                {
        //                    ParentStartVM parentStartVM = new ParentStartVM();
        //                    parentStartVM.DropOfTime = schedule.Dropoff == null ? default(TimeSpan) : (TimeSpan)schedule.Dropoff;
        //                    parentStartVM.PickupTime = schedule.PickUp == null ? default(TimeSpan) : (TimeSpan)schedule.PickUp;
        //                    parentStartVM.FirstName = schedule.Children.FirstName;
        //                    parentStartVM.Id = schedule.Children.Id;
        //                    parentStartVM.IsActive = schedule.Children.IsIll == null ? false : (bool)schedule.Children.IsIll;
        //                    parentStartVM.IsPresent = schedule.Children.IsPresent;
        //                    parentStartVM.MinutesLate = schedule.Children.MinLate == null ? default(int) : (int)schedule.Children.MinLate; //ta bort om denna strular
        //                    pscivm.Add(parentStartVM);
        //                }
        //            }
        //        }
        //    }
        //    //foreach (var item in childIds)
        //    //{
        //    //    var ret = context.Schedules
        //    //        .Where(o => o.Weekdays == weekDay && o.ChildrenId == item)
        //    //        .Select(o => new ParentStartVM
        //    //        {
        //    //            DropOfTime = o.Dropoff.Value,
        //    //            PickupTime = o.PickUp.Value,
        //    //            FirstName = o.Children.FirstName,
        //    //            Id = o.Children.Id,
        //    //            IsActive = o.Children.IsIll.Value,
        //    //            IsPresent = o.Children.IsPresent,
        //    //            MinutesLate = o.Children.MinLate.Value
        //    //        })
        //    //        .SingleOrDefault();
        //    //    pscivm.Add(ret);
        //    //}
        //    return pscivm.ToArray();
        //}

        public ParentStartVM[] GetParentStartVM(string username)
        {
            string aspId = GetASPID(username);
            int userId = GetUserID(aspId);
            int[] childIds = GetChildrenId(userId);

            int weekDay = (int)DateTime.Today.DayOfWeek;

            List<ParentStartVM> pscivm = new List<ParentStartVM>();

            for (int i = 0; i < childIds.Length; i++)
            {
                var tmp = context.Children
                    .Include(o => o.Schedules)
                    .Where(c => c.Id == childIds[i])
                    .ToList();

                foreach (Children child in tmp)
                {
                    foreach (Schedules schedule in child.Schedules)
                    {
                        if (schedule.Weekdays == weekDay)
                        {
                            ParentStartVM parentStartVM = new ParentStartVM();
                            parentStartVM.DropOfTime = schedule.Dropoff == null ? default(TimeSpan) : (TimeSpan)schedule.Dropoff;
                            parentStartVM.PickupTime = schedule.PickUp == null ? default(TimeSpan) : (TimeSpan)schedule.PickUp;
                            parentStartVM.FirstName = schedule.Children.FirstName;
                            parentStartVM.Id = schedule.Children.Id;
                            parentStartVM.IsActive = schedule.Children.IsIll == null ? false : (bool)schedule.Children.IsIll;
                            parentStartVM.IsPresent = schedule.Children.IsPresent;
                            //parentStartVM.MinutesLate = schedule.Children.MinLate == null ? default(int) : (int)schedule.Children.MinLate; //ta bort om denna strular
                            pscivm.Add(parentStartVM);
                        }
                    }
                }
            }

            return pscivm.ToArray();
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
                }
            }
            return parentReportVM;
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
                        Id = o.Children.Id,
                        IsActive = (bool)o.Children.IsIll,
                        MinutesLate = o.Children.MinLate == null ? default(int) : (int)o.Children.MinLate //ta bort vid strul
                    })
                    .OrderBy(o => o.DropOfTime)
                    .ToArray()
            };

            foreach (var child in ret.ChildItems)
            {
                if (child.MinutesLate > 0)
                {
                    UpdateChildTime(child);
                }
            }

            ret.PresentChildrenCount = ret.ChildItems
                .Count(o => o.IsPresent && o.IsActive == false);

            ret.NotPresentChildrenCount = ret.ChildItems
                .Count(o => o.IsPresent == false);

            ret.NotActiveCount = ret.ChildItems
                .Count(o => o.IsActive);

            return ret;
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

        //public void EditSchedule(int childId, int weekdayNr, TimeSpan? pickUpTime, TimeSpan? dropOffTime)
        //{

        //    int scheduleItemId = GetScheduleID(childId, weekdayNr);

        //    var itemToUpdate = context.Schedules
        //        .SingleOrDefault(x => x.Id == scheduleItemId);

        //    itemToUpdate.Dropoff = dropOffTime;
        //    itemToUpdate.PickUp = pickUpTime;

        //    context.SaveChanges();
        //}

        //public void EditSchedule(int dropOffHrs, int dropOffMins, int pickUpHrs, int pickUpMins, int weekdayNr, int childId)
        //{
        //    int scheduleItemId = GetScheduleID(childId, weekdayNr);

        //    TimeSpan dropOff = new TimeSpan(dropOffHrs, dropOffMins, seconds);
        //    TimeSpan pickUp = new TimeSpan(pickUpHrs, pickUpMins, seconds);

        //    var itemToUpdate = context.Schedules
        //        .SingleOrDefault(x => x.Id == scheduleItemId);

        //    itemToUpdate.Dropoff = dropOff;
        //    itemToUpdate.PickUp = pickUp;

        //    context.SaveChanges();
        //}

        public void AddParent(EditUserVM edituser, int childCode)
        {
            string aspnetId = GetASPID(edituser.FirstName);
            context.Users.Add(new Users { FirstName = edituser.FirstName, LastName = edituser.LastName, AspId = aspnetId });

            context.SaveChanges();


            var userID = context.Users.SingleOrDefault(x => x.AspId == aspnetId);
            //edituser.ChildID = childId;
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

        private int[] GetChildrenId(int userId)
        {
            return context.C2p
                .Where(x => x.Uid == userId)
                .Select(x => x.Cid)
                .ToArray();

            //return new int[] { 7 };
        }

        private string GetChildNameFromID(int childID)
        {
            return context.Children
                .SingleOrDefault(x => x.Id == childID).FirstName;
        }

        //public void ChildToParent(CreateUserVM createuserVM)
        //{
        //    string aspnetId = GetASPID(createuserVM.UserName);
        //    var userId = GetUserID(aspnetId);
        //    //var userID = context.Users.SingleOrDefault(x => x.AspId == aspnetId);
        //    //edituser.ChildID = childId;
        //    context.C2p.Add(new C2p
        //    {
        //        Uid = userId,
        //        Cid = createuserVM.ChildCode
        //    });
        //    context.SaveChanges();

        //}

        public void AddDelayTime(int childID, int delay)
        {
            var itemToUpdate = context.Children
                .SingleOrDefault(x => x.Id == childID);

            itemToUpdate.MinLate = delay;

            context.SaveChanges();

            //TeacherStartChildItemVM teacherVM = new TeacherStartChildItemVM();
            //teacherVM.MinutesLate = delay;

            //TimeSpan ts = new TimeSpan(delay);
            //if (!teacherVM.IsPresent)
            //{
            //    teacherVM.DropOfTime.Add(ts);
            //}
            //else
            //{
            //    teacherVM.PickupTime.Add(ts);
            //}



        }
    }
}
