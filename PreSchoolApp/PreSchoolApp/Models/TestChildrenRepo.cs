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
        static List<Children> children = new List<Children>
        {
            new Children{Id = 1, FirstName = "Sara", LastName = "Andersson", IsPresent = false, UnitsId = 1},
            new Children{Id = 2, FirstName = "Kalle", LastName = "Johansson", IsPresent = false, UnitsId = 1},
            //new Children{Id = 3, FirstName = "Olle", LastName = "Lundmark", IsPresent = false, UnitsId = 1},
            //new Children{Id = 4, FirstName = "Anna", LastName = "Marklund", IsPresent = false, UnitsId = 1},
            //new Children{Id = 5, FirstName = "Pelle", LastName = "Eriksson", IsPresent = false, UnitsId = 1}
        };

        public static TeacherStartVM[] GetAllChildren()
        {
            children[0].Schedules = TestSchedulesRepo.GetChildSchedule(1);
            children[1].Schedules = TestSchedulesRepo.GetChildSchedule(2);

            TeacherStartVM[] teacherStartVM = new TeacherStartVM[children.Count];

            for (int i = 0; i < children.Count; i++)
            {
                teacherStartVM[i].Child = children[i];
            }
            return teacherStartVM;
        }

    }
}
