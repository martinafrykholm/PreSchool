using System;
using System.Collections.Generic;

namespace PreSchoolApp.Models.Entities
{
    public partial class Children
    {
        public Children()
        {
            C2p = new HashSet<C2p>();
            Schedules = new HashSet<Schedules>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? UnitsId { get; set; }
        public bool IsPresent { get; set; }
        public bool? IsIll { get; set; }
        public int? MinLate { get; set; }

        public Units Units { get; set; }
        public ICollection<C2p> C2p { get; set; }
        public ICollection<Schedules> Schedules { get; set; }
    }
}
