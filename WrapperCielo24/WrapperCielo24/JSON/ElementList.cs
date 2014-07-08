using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WrapperCielo24.JSON
{
    public class ElementList
    {
        public int Version { get; set; }
        public DateTime StartTime { get; set; }
        public string Language { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class Element
    {
        public DateTime StartTime { get; set; }
        public string Language { get; set; }
        public DateTime EndTime { get; set; }
    }
}
