using LinkedIN.Model.People;
using LinkedIN.Application.Controllers;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.Globalization;

namespace LinkedIN.Application.Model
{
    public class Data
    {
        public Data(string _type, string str, int _likes, int _comments, string _href, DateTime input_date)
        {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            _date = input_date;
            date = input_date.ToShortDateString();
            type = _type;
            name = str;
            likes = _likes;
            comments = _comments;
            href = _href;
        }
        public Data(int _index, int _likes, int _comments)
        {
            index = _index;
            likes = _likes;
            comments = _comments;
        }
        public Data(int _index)
        {
            index = _index;
        }

        public int month;
        public int week;
        public int index;
        public string type;
        public string date;
        public DateTime _date;
        public string name;
        public string href;
        public int likes = 0, comments = 0;
        public int likesPercentage, commentsPercentage;
        public List<Data> activities = new List<Data>();
        internal void addStatistics(int _likes, int _comments)
        {
            likes += _likes;
            comments += _comments;
        }

        internal void addRaw(string str, string type, int _likes, int _comments, string _href, DateTime _date)
        {
            addStatistics(_likes, _comments);
            activities.Add(new Data(type, str, _likes, _comments, _href, _date));
        }
    }

    public class Profile
    {
        public void add(DateTime date, String str, int likes, int comments, string type, string href)
        {
            int i = DateTime.Now.Month - date.Month + 1;
            int k = (int)Math.Floor((double)(DateTime.Now.Subtract(date).Days / 7));
            if (monthes.ContainsKey(i))
                monthes[i].addStatistics(likes, comments);
            if (weeks.ContainsKey(k))
                weeks[k].addRaw(str, type, likes, comments, href, date);
        }
        public Profile(Person person)
        {
            this.person = person;
            firstDegreeConnections = person.NetworkStats.properties[0].Value;
            secondDegreeConnections = person.NetworkStats.properties[1].Value;
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