using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinhubEntities.Models
{
    public class FinhubCompanyProfileModel
    {
        public string? country { get; set; }
        public string? currency { get; set; }
        public string? estimateCurrency { get; set; }
        public string? exchange { get; set; }
        public string? finnhubIndustry { get; set; }
        public DateTime? ipo { get; set; }
        public string? logo { get; set; }
        public double? marketCapitalization { get; set; }
        public string? name { get; set; }
        public string? phone { get; set; }
        public double? shareOutstanding { get; set; }
        public string? ticker { get; set; }
        public string? weburl { get; set; }
    }
}
