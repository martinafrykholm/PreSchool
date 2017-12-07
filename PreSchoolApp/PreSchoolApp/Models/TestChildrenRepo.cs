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
        public static List<Children> children = new List<Children>
        {
            new Children{Id = 1, FirstName = "Sara", LastName = "Andersson", IsPresent = true, UnitsId = 1},
            new Children{Id = 2, FirstName = "Kalle", LastName = "Johansson", IsPresent = true, UnitsId = 1},
            new Children{Id = 3, FirstName = "Olle", LastName = "Lundmark", IsPresent = false, UnitsId = 1},
            new Children{Id = 4, FirstName = "Anna", LastName = "Marklund", IsPresent = true, UnitsId = 1},
            new Children{Id = 5, FirstName = "Pelle", LastName = "Eriksson", IsPresent = false, UnitsId = 1}
        };

        public static TeacherStartVM[] GetAllChildren()
        {
            children[0].Schedules = TestSchedulesRepo.GetChildSchedule(1);
            children[1].Schedules = TestSchedulesRepo.GetChildSchedule(2);
            children[2].Schedules = TestSchedulesRepo.GetChildSchedule(2);
            children[3].Schedules = TestSchedulesRepo.GetChildSchedule(1);
            children[4].Schedules = TestSchedulesRepo.GetChildSchedule(2);


            TeacherStartVM[] teacherStartVM = new TeacherStartVM[children.Count];
            TeacherStartVM teacherStartVM1 = new TeacherStartVM();
            TeacherStartVM teacherStartVM2 = new TeacherStartVM();
            TeacherStartVM teacherStartVM3 = new TeacherStartVM();
            TeacherStartVM teacherStartVM4 = new TeacherStartVM();
            TeacherStartVM teacherStartVM5 = new TeacherStartVM();

            teacherStartVM[0] = teacherStartVM1;
            teacherStartVM[1] = teacherStartVM2;
            teacherStartVM[2] = teacherStartVM3;
            teacherStartVM[3] = teacherStartVM4;
            teacherStartVM[4] = teacherStartVM5;


            for (int i = 0; i < children.Count; i++)
            {
                teacherStartVM[i].Child = children[i];
            }
            return teacherStartVM;
        }
    }
}
