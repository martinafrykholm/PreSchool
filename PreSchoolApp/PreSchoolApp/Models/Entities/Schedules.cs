using System;
using System.Collections.Generic;

namespace PreSchoolApp.Models.Entities
{
    public partial class Schedules
    {
        public int Id { get; set; }
        public string Weekdays { get; set; }
        public TimeSpan? Dropoff { get; set; }
        public TimeSpan? PickUp { get; set; }
        public int? ChildrenId { get; set; }

        public Children Children { get; set; }
    }
}
