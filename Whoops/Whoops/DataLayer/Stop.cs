﻿using System;

namespace Whoops.DataLayer
{
    public class Stop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public int Order { get; set; }
        public DateTime Arrival { get; set; }
    }
}