using LinkedIN.Model.People;
using LinkedIN.Application.Controllers;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.Globalization;

namespace LinkedIN.Application.Model
{

    public class Profile
    {
        public void add(Data item)
        {
            int i = DateTime.Now.Month - item._date.Month + 1;
            int k = (int)Math.Floor((double)(DateTime.Now.Subtract(item._date).Days / 7));
            if (monthes.ContainsKey(i))
                monthes[i].addStatistics(item.likes, item.comments);
            if (weeks.ContainsKey(k))
                weeks[k].addRaw(item);
        }

        public Profile(Person person)
        {
            this.person = person;
            if (person.NetworkStats != null)
            {
            firstDegreeConnections = person.NetworkStats.properties[0].Value;
            secondDegreeConnections = person.NetworkStats.properties[1].Value;
            }
            DateTime _month = DateTime.Now;
            DateTime _week = DateTime.Now;
            monthes = new SortedDictionary<int, Data>();
            weeks = new SortedDictionary<int, Data>();
            int k = 0;
            for (int i = 1; i <= 4; i++, _month = _month.AddMonths(-1))
            {
                Data month = new Data(i);
                monthes.Add(i, month);
                month.month = _month.Month;
                if (i == 1)
                {
                    month.name = "Current Month";
                }
                else
                {
                    month.name = _month.ToString("MMMM yyyy");
                }
                for (int j = 1; (_week.Month == _month.Month && _week.Day > 3) || (_week.Month > _month.Month && _week.Day <= 3); j++, _week = _week.AddDays(-7))
                {
                    Data week = new Data(k);
                    weeks.Add(k, week);
                    week.month = _month.Month;
                    week.week = j;
                    week.date = _week.ToString("dd.MM.yyyy");
                    if (j == 1 && i == 1)
                    {
                        week.name = "Current Week";
                    }
                    else
                    {
                        week.name = _week.AddDays(-6).ToString("dd.MM.yyyy") + " - " + _week.ToString("dd.MM.yyyy");
                    }
                    k++;
                }
            }
        }
        public Person person { get; set; }
        public int firstDegreeConnections { get; set; }
        public int secondDegreeConnections { get; set; }
        public SortedDictionary<int,Data> monthes { get; set; }
        public SortedDictionary<int, Data> weeks { get; set; }
        public void processPercentages()
        {
            Data last_month = null;
            foreach (Data month in monthes.Values)
            {
                if (last_month != null)
                {
                    if (month.comments > 0)
                        last_month.commentsPercentage = 100 * last_month.comments / month.comments - 100;
                    if (month.likes > 0)
                        last_month.likesPercentage = 100 * last_month.likes / month.likes - 100;
                }
                last_month = month;
            }
            Data last_week = null;
            foreach (Data week in weeks.Values)
            {
                if (last_week != null)
                {
                    if (week.comments > 0)
                        last_week.commentsPercentage = 100 * last_week.comments / week.comments - 100;
                    if (week.likes > 0)
                        last_week.likesPercentage = 100 * last_week.likes / week.likes - 100;
                }
                last_week = week;
            }
        }
    }
}