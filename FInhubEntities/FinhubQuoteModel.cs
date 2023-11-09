using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinhubEntities.Models
{
    public class FinhubQuoteModel
    {
        [Required]
        public double? c { get; set; }
        [Required]
        public double? d { get; set; }
        [Required]
        public double? dp { get; set; }
        [Required]
        public double? h { get; set; }
        [Required]
        public double? l { get; set; }
        [Required]
        public double? o { get; set; }
        [Required]
        public double? pc { get; set; }
        [Required]
        public double? t { get; set; }
    }
}
