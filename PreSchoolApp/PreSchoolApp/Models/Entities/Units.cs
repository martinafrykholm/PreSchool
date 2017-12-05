using System;
using System.Collections.Generic;

namespace PreSchoolApp.Models.Entities
{
    public partial class Units
    {
        public Units()
        {
            Children = new HashSet<Children>();
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string UnitName { get; set; }
        public int PreSchoolsId { get; set; }

        public PreSchools PreSchools { get; set; }
        public ICollection<Children> Children { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
