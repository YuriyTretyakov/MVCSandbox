using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Whoops.ViewModels.Index
{
    public class StopViewModel
    {
        [Required]
        [StringLength(100,MinimumLength = 3)]
        public string Name { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public DateTime Arrival { get; set; }
    }
}
