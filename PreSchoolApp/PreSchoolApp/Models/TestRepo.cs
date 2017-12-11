using PreSchoolApp.Models.Entities;
using PreSchoolApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchoolApp.Models
{
    public class TestRepo
    {
        public static TimeSpan dropOff1 = new TimeSpan(8, 00, 00);
        public static TimeSpan dropOff2 = new TimeSpan(9, 00, 00);
        public static TimeSpan dropOff3 = new TimeSpan(8, 30, 00);
        public static TimeSpan dropOff4 = new TimeSpan(9, 00, 00);
        public static TimeSpan dropOff5 = new TimeSpan(9, 00, 00);

        public static TimeSpan pickUp1 = new TimeSpan(16, 00, 00);
        public static TimeSpan pickUp2 = new TimeSpan(17, 00, 00);
        public static TimeSpan pickUp3 = new TimeSpan(16, 30, 00);
        public static TimeSpan pickUp4 = new TimeSpan(17, 00, 00);
        public static TimeSpan pickUp5 = new TimeSpan(17, 00, 00);

        public static TimeSpan[] dropOffs = new TimeSpan[5];
        public static TimeSpan[] pickUps = new TimeSpan[5];

        public List<TimeSpan> dropOffTimes = new List<TimeSpan>();

        public static TeacherStartVM GetTestTeacherStartVM()
        {
            TeacherStartChildItemVM[] children = new TeacherStartChildItemVM[5]
            {
            new TeacherStartChildItemVM{FirstName = "Sara", LastName = "Andersson", IsPresent = true, DropOfTime = dropOff1, PickupTime = pickUp1},
            new TeacherStartChildItemVM{FirstName = "Kalle", LastName = "Johansson", IsPresent = false, DropOfTime = dropOff1, PickupTime = pickUp2},
            new TeacherStartChildItemVM{FirstName = "Olle", LastName = "Lundmark", IsPresent = true, DropOfTime = dropOff2, PickupTime = pickUp1},
            new TeacherStartChildItemVM{FirstName = "Anna", LastName = "Marklund", IsPresent = false, DropOfTime = dropOff1, PickupTime = pickUp2},
            new TeacherStartChildItemVM{FirstName = "Pelle", LastName = "Eriksson", IsPresent = true, DropOfTime = dropOff2, PickupTime = pickUp2}
            };

            TeacherStartVM teacherStartVM = new TeacherStartVM();

            teacherStartVM.ChildItems = children;

            int counter = 0;
            foreach (var item in children)
            {
                if (item.IsPresent)
                {
                    counter++;
                }
            }

            teacherStartVM.PresentChildrenCount = counter;

            return teacherStartVM;
        }

        internal static ParentReportVM GetTestParentReportData()
        {
            ParentReportVM parentReportVM = new ParentReportVM();
            parentReportVM.DropOffTime = dropOff1;
            parentReportVM.PickupTime = pickUp1;
            parentReportVM.FirstName = "Olle";
            parentReportVM.LastName = "Olsson";
            parentReportVM.Id = 1;
            parentReportVM.IsActive = true;

            return parentReportVM;
        }

        public static ParentStartVM GetTestParentStartVM()
        {
            TimeSpan dropOff1 = new TimeSpan(8, 00, 00);
            TimeSpan pickUp1 = new TimeSpan(16, 00, 00);
            TimeSpan dropOff2 = new TimeSpan(9, 00, 00);
            TimeSpan pickUp2 = new TimeSpan(17, 00, 00);

            ParentStartChildItemVM parentStartVM1 = new ParentStartChildItemVM
            {
                FirstName = "Kalle",
                LastName = "Persson",
                Id = 7,
                DropOfTime = dropOff1,
                PickupTime = pickUp1,
                IsActive = true,
                IsPresent = false
            };

            ParentStartChildItemVM parentStartVM2 = new ParentStartChildItemVM
            {
                FirstName = "Olle",
                LastName = "Karlsson",
                Id = 10,
                DropOfTime = dropOff2,
                PickupTime = pickUp2,
                IsActive = false,
                IsPresent = true
            };

            ParentStartChildItemVM parentStartVM3 = new ParentStartChildItemVM
            {
                FirstName = "Sara",
                LastName = "Andersson",
                Id = 3,
                DropOfTime = dropOff1,
                PickupTime = pickUp2,
                IsActive = false,
                IsPresent = true
            };

            ParentStartChildItemVM parentStartVM4 = new ParentStartChildItemVM
            {
                FirstName = "Anna",
                LastName = "Olsson",
                Id = 4,
                DropOfTime = dropOff2,
                PickupTime = pickUp1,
                IsPresent = false,
                IsActive = false,
            };

            //ParentStartChildItemVM[] parentStartChildItemVM = new ParentStartChildItemVM[4];
            List<ParentStartChildItemVM> parentStartChildItemVM = new List<ParentStartChildItemVM>();

            parentStartChildItemVM.Add(parentStartVM1);
            parentStartChildItemVM.Add(parentStartVM2);
            //parentStartChildItemVM.Add(parentStartVM3);
            //parentStartChildItemVM.Add(parentStartVM4);


            string[] delayTimes = new string[7];
            delayTimes[0] = "Meddela försening";
            delayTimes[1] = "10 minuter";
            delayTimes[2] = "20 minuter";
            delayTimes[3] = "30 minuter";
            delayTimes[4] = "40 minuter";
            delayTimes[5] = "50 minuter";
            delayTimes[6] = "60 minuter";


            ParentStartVM parentStartVM = new ParentStartVM();
            parentStartVM.ChildItems = parentStartChildItemVM.ToArray();
            //parentStartVM.Day = DateTime.Today.DayOfWeek.ToString();
            //parentStartVM.DelayTime = delayTimes;

            return parentStartVM;
        }

        internal static void UpdateCalendar(int id, int weekDay, bool isDropOff, TimeSpan time)
        {
            if (time.Hours > 0 && time.Hours < 18)
            {
                if (isDropOff)
                {
                    dropOffs[weekDay] = time;
                }
                else
                {
                    pickUps[weekDay] = time;
                }
            }
        }

        public static ParentCalendarVM GetTestParentCalendarVM(int id)
        {
            if (pickUps[0].Hours == 0)
            {
                pickUps[0] = pickUp1;
                pickUps[1] = pickUp2;
                pickUps[2] = pickUp3;
                pickUps[3] = pickUp4;
                pickUps[4] = pickUp5;

                dropOffs[0] = dropOff1;
                dropOffs[1] = dropOff2;
                dropOffs[2] = dropOff3;
                dropOffs[3] = dropOff4;
                dropOffs[4] = dropOff5;
            }

            string[] weekDays = new string[5];
            weekDays[0] = "Måndag";
            weekDays[1] = "Tisdag";
            weekDays[2] = "Onsdag";
            weekDays[3] = "Torsdag";
            weekDays[4] = "Fredag";


            ParentCalendarVM parentCalendarVM = new ParentCalendarVM();
            parentCalendarVM.DropOffTimes = dropOffs;
            parentCalendarVM.PickupTimes = pickUps;
            parentCalendarVM.Weekdays = weekDays;
            parentCalendarVM.FirstName = "Kalle";
            parentCalendarVM.ChildId = 10;
            parentCalendarVM.AllTimes = GetAllTimes();

            return parentCalendarVM;
        }

        public static TimeSpan[] GetAllTimes()
        {
            List<TimeSpan> allTimeSpans = new List<TimeSpan>();
            int hour = 8;
            int minute = 0;

            for (int i = 0; i < 55; i++)
            {
                TimeSpan allTimes = new TimeSpan(hour, minute, 00);
                allTimeSpans.Add(allTimes);
                minute = minute + 10;
                if (minute == 60)
                {
                    hour++;
                    minute = 0;
                }
            }

            return allTimeSpans.ToArray();
        }
    }
}

