using System;
using System.Collections.Generic;

namespace PreSchoolApp.Models.Entities
{
    public partial class Users
    {
        public Users()
        {
            C2p = new HashSet<C2p>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AspId { get; set; }
        public int? UnitsId { get; set; }

        public AspNetUsers Asp { get; set; }
        public Units Units { get; set; }
        public ICollection<C2p> C2p { get; set; }
    }
}
