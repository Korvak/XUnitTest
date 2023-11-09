using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinhubServices.ConfigOptions
{
    public class FinhubOptions
    {
        [Required]
        public string? ApiKey { get; set; }

        [Required]
        public string? Weburl { get; set; }

        [Required]
        public string? QuoteUrl { get; set;}

        [Required]
        public string? CompanyProfileUrl { get; set;}
    }
}
