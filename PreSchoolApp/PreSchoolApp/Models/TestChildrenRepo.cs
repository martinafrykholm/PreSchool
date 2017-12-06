using PreSchoolApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchoolApp.Models
{
    public class TestChildrenRepo
    {        
        //static List<Schedules> schedules = TestSchedulesRepo.GetSchedule();
        static List<Children> customers = new List<Children>
        {
            new Children{Id = 1, FirstName = "Sara", LastName = "Andersson", IsPresent = false, UnitsId = 1},
            new Children{Id = 2, FirstName = "Kalle", LastName = "Johansson", IsPresent = false, UnitsId = 1},
            new Children{Id = 3, FirstName = "Olle", LastName = "Lundmark", IsPresent = false, UnitsId = 1},
            new Children{Id = 4, FirstName = "Anna", LastName = "Marklund", IsPresent = false, UnitsId = 1},
            new Children{Id = 5, FirstName = "Pelle", LastName = "Eriksson", IsPresent = false, UnitsId = 1}
        };


        //public CustomersIndexVM[] GetAllCustomers()
        //{
        //    return customers
        //        .Select(c => new CustomersIndexVM
        //        {
        //            CompanyName = c.CompanyName,
        //            IsVip = c.CompanyName.StartsWith("S")
        //        })
        //        .ToArray();
        //}        
    }
}
