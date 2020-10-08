using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;

namespace Person
{
    partial class Program
    {
        public class data
        {
            public List<records> records { get; set; }
            public pageInfo pageInfo { get; set; }
        }

      
        public class records
        {
            public double temperature { get; set; }
            public string time { get; set; }
            public int id { get; set; }
            public string path { get; set; }
            public int state { get; set; }
            public int type { get; set; }
            public string personId { get; set; }
            public int model { get; set; }
        }

        public class pageInfo
        {
            public int index { get; set; }
            public int size { get; set; }
            public int total { get; set; }
            public int length { get; set; }
        }

        public class Root
        {
            public data data { get; set; }
            public int result { get; set; }
            public bool success { get; set; }

        }

    }

}
