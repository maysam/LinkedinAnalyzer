using LinkedIN.Model.People;
using LinkedIN.Application.Controllers;
using System;
using System.Collections.Generic;

namespace LinkedIN.Application.Model
{

    public class Data
    {
        public Data(int _index, int _likes, int _comments)
        {
            index = _index;
            likes = _likes;
            comments = _comments;
        }
        public int month;
        public int week;
        public int index;
        public string name;
        public int likes, comments;
        public int likesPercentage, commentsPercentage;
    }

    public class Profile
    {
        public Profile(Person l_profile)
        {
            this.person = l_profile;
            DateTime _month = DateTime.Now;
            DateTime _week = DateTime.Now;
            Random rnd = new Random();
            monthes = new List<Data>();
            weeks = new List<Data>();
            int k = 0;
            for (int i = 1; i <= 4; i++, _month = _month.AddMonths(-1))
            {
                Data month = new Data(i, rnd.Next(1, 100), rnd.Next(1, 100));
                monthes.Add(month);
                month.month = _month.Month;
                if (i == 1)
                {
                    month.name = "Current Month"; // (31 likes +70% 42 comments -32%)
                }
                else
                {
                    month.name = _month.ToString("MMMM yyyy"); // (31 likes +70% 42 comments -32%)
                }
                for (int j = 1; (_week.Month == _month.Month && _week.Day > 3) || (_week.Month > _month.Month && _week.Day <= 3); j++, _week = _week.AddDays(-7))
                {
                    Data week = new Data(k, rnd.Next(1, 100), rnd.Next(1, 100));
                    weeks.Add(week);
                    week.month = _month.Month;
                    week.week = j;
                    //week.likes = rnd.Next(1,100);
                    //week.comments = rnd.Next(1, 100);
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
        public int[] likes { get; set; }
        public int[] comments { get; set; }
        public List<Data> monthes { get; set; }
        public List<Data> weeks { get; set; }
        public void processPercentages()
        {
            Data last_month = null;
            foreach (var month in monthes)
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
            foreach (var week in weeks)
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