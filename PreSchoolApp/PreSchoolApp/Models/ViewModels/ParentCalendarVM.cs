using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchoolApp.Models.ViewModels
{
    public class ParentCalendarVM
    {
        public string FirstName { get; set; }
        public string[] Weekdays { get; set; }
        public int ChildId { get; set; }
        public TimeSpan[] PickupTimes { get; set; }
        public TimeSpan[] DropOffTimes { get; set; }
        public TimeSpan[] AllTimes { get; set; }
    }
}
