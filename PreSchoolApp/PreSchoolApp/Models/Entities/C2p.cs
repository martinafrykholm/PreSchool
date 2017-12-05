using System;
using System.Collections.Generic;

namespace PreSchoolApp.Models.Entities
{
    public partial class C2p
    {
        public int Uid { get; set; }
        public int Cid { get; set; }

        public Children C { get; set; }
        public Users U { get; set; }
    }
}
