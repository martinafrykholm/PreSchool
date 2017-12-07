using PreSchoolApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreSchoolApp.Models.Entities;
using PreSchoolApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace PreSchoolApp.Models
{
    public class ChildrenDBRepository
    {

        PreSchoolAppContext context;

        public ChildrenDBRepository(PreSchoolAppContext context)
        {
            this.context = context;
        }

        public TeacherStartVM GetTodaysSchedules()
        {
            int weekDay = (int)DateTime.Today.DayOfWeek;

            var ret = new TeacherStartVM
            {
                ChildItems = context.Schedules
                    .Where(o => o.Weekdays == weekDay)
                    .Select(o => new TeacherStartChildItemVM
                    {
                        DropOfTime = o.Dropoff.Value,
                        PickupTime = o.PickUp.Value,
                        IsPresent = o.Children.IsPresent,
                        FirstName = o.Children.FirstName,
                        LastName = o.Children.LastName
                    })
                    .OrderBy(o => o.IsPresent)
                    .ToArray()
            };

            ret.PresentChildrenCount = ret.ChildItems
                .Count(o => o.IsPresent);

            return ret;
        }

    }
}
