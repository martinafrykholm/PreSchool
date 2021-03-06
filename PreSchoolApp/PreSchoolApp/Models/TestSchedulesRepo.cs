﻿using PreSchoolApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchoolApp.Models
{
    public enum Weekdays
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5
    }


    public class TestSchedulesRepo
    {
        static TimeSpan dropOff1 = new TimeSpan(8, 00, 00);
        static TimeSpan pickUp1 = new TimeSpan(16, 00, 00);
        static TimeSpan dropOff2 = new TimeSpan(9, 00, 00);
        static TimeSpan pickUp2 = new TimeSpan(17, 00, 00);

        static List<Schedules> schedules1 = new List<Schedules>
        {
            new Schedules{Id = 1, ChildrenId = 1, Dropoff = dropOff1, PickUp = pickUp1, Weekdays = 1},
            new Schedules{Id = 1, ChildrenId = 1, Dropoff = dropOff1, PickUp = pickUp1, Weekdays = 2},
            new Schedules{Id = 1, ChildrenId = 1, Dropoff = dropOff1, PickUp = pickUp1, Weekdays = 3},
            new Schedules{Id = 1, ChildrenId = 1, Dropoff = dropOff1, PickUp = pickUp1, Weekdays = 4},
            new Schedules{Id = 1, ChildrenId = 1, Dropoff = dropOff1, PickUp = pickUp1, Weekdays = 5},            
        };

        static List<Schedules> schedules2 = new List<Schedules>
        {            
            new Schedules{Id = 2, ChildrenId = 2, Dropoff = dropOff2, PickUp = pickUp2, Weekdays = 1},
            new Schedules{Id = 2, ChildrenId = 2, Dropoff = dropOff2, PickUp = pickUp2, Weekdays = 2},
            new Schedules{Id = 2, ChildrenId = 2, Dropoff = dropOff2, PickUp = pickUp2, Weekdays = 3},
            new Schedules{Id = 2, ChildrenId = 2, Dropoff = dropOff2, PickUp = pickUp2, Weekdays = 4},
            new Schedules{Id = 2, ChildrenId = 2, Dropoff = dropOff2, PickUp = pickUp2, Weekdays = 5},
        };

        public static List<Schedules> GetChildSchedule(int id)
        {
            if (id == 1)
            {
                return schedules1;
            }

            else
            {
                return schedules2;
            }
        }
    }
}
