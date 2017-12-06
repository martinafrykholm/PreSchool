using PreSchoolApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchoolApp.Models
{

    public class TestSchedulesRepo
    {
        static TimeSpan dropOff = new TimeSpan(8, 00, 00);
        static TimeSpan pickUp = new TimeSpan(16, 00, 00);

        static List<Schedules> schedules = new List<Schedules>
        {
            new Schedules{Id = 1, ChildrenId = 1, Dropoff = dropOff, PickUp = pickUp, Weekdays = "Monday"},
            new Schedules{Id = 1, ChildrenId = 1, Dropoff = dropOff, PickUp = pickUp, Weekdays = "Tuesday"},
            new Schedules{Id = 1, ChildrenId = 1, Dropoff = dropOff, PickUp = pickUp, Weekdays = "Wednesd"},
            new Schedules{Id = 1, ChildrenId = 1, Dropoff = dropOff, PickUp = pickUp, Weekdays = "Thursda"},
            new Schedules{Id = 1, ChildrenId = 1, Dropoff = dropOff, PickUp = pickUp, Weekdays = "Friday"},
            new Schedules{Id = 2, ChildrenId = 2, Dropoff = dropOff, PickUp = pickUp, Weekdays = "Monday"},
            new Schedules{Id = 2, ChildrenId = 2, Dropoff = dropOff, PickUp = pickUp, Weekdays = "Tuesday"},
            new Schedules{Id = 2, ChildrenId = 2, Dropoff = dropOff, PickUp = pickUp, Weekdays = "Wednesd"},
            new Schedules{Id = 2, ChildrenId = 2, Dropoff = dropOff, PickUp = pickUp, Weekdays = "Thursda"},
            new Schedules{Id = 2, ChildrenId = 2, Dropoff = dropOff, PickUp = pickUp, Weekdays = "Friday"},
        };

        static public List<Schedules> GetSchedule()
        {
            return schedules;
        }

        //static public List<Schedules> GetScheduleForChild(int id)
        //{
        //    List<Schedules> childSchedule = 

        //    return schedules;
        //}

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
