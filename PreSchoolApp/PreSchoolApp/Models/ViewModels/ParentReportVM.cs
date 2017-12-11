using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchoolApp.Models.ViewModels
{
    public class ParentReportVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public TimeSpan DropOffTime { get; set; }
        public TimeSpan PickupTime { get; set; }
    }
}
