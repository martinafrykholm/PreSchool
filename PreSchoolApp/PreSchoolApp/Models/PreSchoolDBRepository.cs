using PreSchoolApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreSchoolApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace PreSchoolApp.Models
{
    public class PreSchoolDBRepository
    {

        PreSchoolAppContext context;

        public PreSchoolDBRepository(PreSchoolAppContext context)
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


        public void AddChild(string firstName, string lastName, int unitId)
        {
            context.Children.Add(new Children
            {
                FirstName = firstName,
                LastName = lastName,
                UnitsId = unitId
            });

        }

        public void AddPreSchool(string preschoolName)
        {
            context.PreSchools.Add(new PreSchools { PreSchoolName = preschoolName });

        }

        public void AddUnit(string unitName, int preschoolID)
        {
            context.Units.Add(new Units {UnitName= unitName, PreSchoolsId= preschoolID });

        }

        public void AddTeacher(string firstName, string lastName, string aspId, int unitId)
        {
            context.Users.Add(new Users { FirstName = firstName, LastName = lastName, AspId = aspId, UnitsId = unitId });
        }

        //private int Get

    }
}
