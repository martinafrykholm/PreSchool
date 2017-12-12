using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchoolApp.Models.ViewModels
{
    //public class ParentStartVM
    //{
    //    //public string Day { get; set; }
    //    //public string[] DelayTime { get; set; }
    //    public ParentStartChildItemVM[] ChildItems { get; set; }
    //}

    public class ParentStartVM
    {
        public string FirstName { get; set; }
        public int Id { get; set; }
        public int MinutesLate { get; set; }
        public bool IsPresent { get; set; }
        public bool IsActive { get; set; }
        public TimeSpan DropOfTime { get; set; }
        public TimeSpan PickupTime { get; set; }
    }
}
