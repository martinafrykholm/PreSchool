using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PreSchoolApp.Models.Entities
{
    public partial class PreSchoolAppContext : DbContext
    {
        public PreSchoolAppContext(DbContextOptions<PreSchoolAppContext> c) :base(c)
        {

        }
    }
}
