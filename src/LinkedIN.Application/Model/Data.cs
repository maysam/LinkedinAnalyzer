using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinkedIN.Application.Model {

    public class Data
    {
        public Data() { }
        public Data(string str, int _likes, int _comments, string _href, long timestamp)
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            _date = origin.AddMilliseconds(timestamp);            
            date = _date.ToShortDateString();
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

        internal void addRaw(Data item)
        {
            addStatistics(item.likes, item.comments);
            activities.Add(item);
        }
    }

    public class UserPost : Data
    {
        public UserPost(string content, int likes_count, int comments_count, string href, long timestamp)
            : base(content, likes_count, comments_count, href, timestamp)
        {
        }
    }

    public class GroupPost : Data
    {
        public GroupPost(string content, int likes_count, int comments_count, string href, long timestamp)
            : base(content, likes_count, comments_count, href, timestamp)
        {
        }
    }

    public class GroupPostComment : Data
    {
        public GroupPostComment(string content, string href, long timestamp)
            : base(content, 0, 0, href, timestamp)
        {
        }
    }


}
