using PreSchoolApp.Models.Entities;
using PreSchoolApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchoolApp.Models
{
    public class UserDBRepository
    {
        PreSchoolAppContext context;
        public UserDBRepository(PreSchoolAppContext context)
        {
            this.context = context;
        }

        public void AddParent(EditUserVM user)
        {
            context.Users.Add(new Users
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                
            }
                 );
            context.SaveChanges();

        }
    }
}
