using PreSchoolApp.Models.Entities;
using PreSchoolApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchoolApp.Models.ViewModels
{
    public class TeacherStartVM
    {
        public TeacherStartChildItemVM[] CheckInChildItems { get; set; }
        public TeacherStartChildItemVM[] CheckOutChildItems { get; set; }
        public TeacherStartChildItemVM[] NotCheckedInChildItems { get; set; }
    }
    
    public class TeacherStartChildItemVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public TimeSpan DropOfTime { get; set; }
        public TimeSpan PickupTime { get; set; }
        public bool IsPresent { get; set; }
        public bool IsActive { get; set; }
        public int MinutesLate { get; set; }
    }
}


