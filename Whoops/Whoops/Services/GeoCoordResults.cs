using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whoops.Services
{
    public class GeoCoordResults
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
    }
}
