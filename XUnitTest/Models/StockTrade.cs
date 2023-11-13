using System.ComponentModel.DataAnnotations;

namespace XUnitTest.Models
{
    public class StockTrade
    {
        [Required]
        public string? StockSymbol { get; set; }

        public string? StockName { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Range(0, double.MaxValue)]
        public uint Quantity {  get; set; }
    }
}
