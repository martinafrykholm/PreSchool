using PreSchoolApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchoolApp.Models
{

    public class TestSchedulesRepo
    {
        static TimeSpan dropOff1 = new TimeSpan(8, 00, 00);
        static TimeSpan pickUp1 = new TimeSpan(16, 00, 00);
        static TimeSpan dropOff2 = new TimeSpan(9, 00, 00);
        static TimeSpan pickUp2 = new TimeSpan(17, 00, 00);

        static List<Schedules> schedules = new List<Schedules>
        {
            new Schedules{Id = 1, ChildrenId = 1, Dropoff = dropOff1, PickUp = pickUp1, Weekdays = "Monday"},
            new Schedules{Id = 1, ChildrenId = 1, Dropoff = dropOff1, PickUp = pickUp1, Weekdays = "Tuesday"},
            new Schedules{Id = 1, ChildrenId = 1, Dropoff = dropOff1, PickUp = pickUp1, Weekdays = "Wednesd"},
            new Schedules{Id = 1, ChildrenId = 1, Dropoff = dropOff1, PickUp = pickUp1, Weekdays = "Thursda"},
            new Schedules{Id = 1, ChildrenId = 1, Dropoff = dropOff1, PickUp = pickUp1, Weekdays = "Friday"},
            new Schedules{Id = 2, ChildrenId = 2, Dropoff = dropOff2, PickUp = pickUp2, Weekdays = "Monday"},
            new Schedules{Id = 2, ChildrenId = 2, Dropoff = dropOff2, PickUp = pickUp2, Weekdays = "Tuesday"},
            new Schedules{Id = 2, ChildrenId = 2, Dropoff = dropOff2, PickUp = pickUp2, Weekdays = "Wednesd"},
            new Schedules{Id = 2, ChildrenId = 2, Dropoff = dropOff2, PickUp = pickUp2, Weekdays = "Thursda"},
            new Schedules{Id = 2, ChildrenId = 2, Dropoff = dropOff2, PickUp = pickUp2, Weekdays = "Friday"},
        };        
    }
}
