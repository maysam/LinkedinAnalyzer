using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinkedIN.Model.People
{
    /// <summary>
    /// </summary>
    public class CurrentShare
    {
        /// <summary>
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// </summary>
        public long TimeStamp { get; set; }
        /// <summary>
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// </summary>
        public Content Content { get; set; }
    }
}
