using System;
using System.ComponentModel.DataAnnotations;

namespace Whoops.ViewModels.Index
{
    public class IndexViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Location { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }
}
