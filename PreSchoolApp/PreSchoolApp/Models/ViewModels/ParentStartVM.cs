using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchoolApp.Models.ViewModels
{
    public class ParentStartVM
    {
        //public List <ParentStartItemVM> { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public TimeSpan DropOfTime { get; set; }
        public TimeSpan PickupTime { get; set; }
    }
}
