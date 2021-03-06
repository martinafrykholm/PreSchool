﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchoolApp.Models
{
    public class Utils
    {
        public static TimeSpan[] GetAllTimes()
        {
            List<TimeSpan> allTimeSpans = new List<TimeSpan>();
            int hour = 7;
            int minute = 0;

            for (int i = 0; i < 61; i++)
            {
                TimeSpan allTimes = new TimeSpan(hour, minute, 00);
                allTimeSpans.Add(allTimes);
                minute = minute + 10;
                if (minute == 60)
                {
                    hour++;
                    minute = 0;
                }
            }

            return allTimeSpans.ToArray();
        }

        public static string[] GetWeekdays()
        {
            string[] weekdays = new string[]
            {
                "Måndag",
                "Tisdag",
                "Onsdag",
                "Torsdag",
                "Fredag"
            };

            return weekdays;
        }
    }
}
