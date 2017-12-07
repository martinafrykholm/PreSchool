using PreSchoolApp.Models.Entities;
using PreSchoolApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchoolApp.Models
{
    public class TestChildrenRepo
    {

        public static TeacherStartVM GetTestData()
        {

            TimeSpan dropOff1 = new TimeSpan(8, 00, 00);
            TimeSpan pickUp1 = new TimeSpan(18, 00, 00);
            TimeSpan dropOff2 = new TimeSpan(9, 00, 00);
            TimeSpan pickUp2 = new TimeSpan(17, 00, 00);

            TeacherStartChildItemVM[] children = new TeacherStartChildItemVM[5]
            {
            new TeacherStartChildItemVM{FirstName = "Sara", LastName = "Andersson", IsPresent = true, DropOfTime = dropOff1, PickupTime = pickUp1},
            new TeacherStartChildItemVM{FirstName = "Kalle", LastName = "Johansson", IsPresent = false, DropOfTime = dropOff1, PickupTime = pickUp2},
            new TeacherStartChildItemVM{FirstName = "Olle", LastName = "Lundmark", IsPresent = true, DropOfTime = dropOff2, PickupTime = pickUp1},
            new TeacherStartChildItemVM{FirstName = "Anna", LastName = "Marklund", IsPresent = false, DropOfTime = dropOff1, PickupTime = pickUp2},
            new TeacherStartChildItemVM{FirstName = "Pelle", LastName = "Eriksson", IsPresent = true, DropOfTime = dropOff2, PickupTime = pickUp2}

            };

            TeacherStartVM teacherStartVM = new TeacherStartVM();

            teacherStartVM.ChildItems = children;

            int counter = 0;
            foreach (var item in children)
            {
                if (item.IsPresent)
                {
                    counter++;
                }
            }

            teacherStartVM.PresentChildrenCount = counter;

            return teacherStartVM;
        }
    }
}

