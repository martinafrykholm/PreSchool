using System;
using System.Collections.Generic;

namespace PreSchoolApp.Models.Entities
{
    public partial class PreSchools
    {
        public PreSchools()
        {
            Units = new HashSet<Units>();
        }

        public int Id { get; set; }
        public string PreSchoolName { get; set; }

        public ICollection<Units> Units { get; set; }
    }
}
