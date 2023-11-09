using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SellOrder
    {
        public Guid SellOrderID { get; set; }

        [Required]
        public string? StockSymbol { get; set; }

        [Required]
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000)]
        public uint Quantity { get; set; }

        [Range(1.0, 10000.0)]
        public double Price { get; set; }

        #region methods
        public bool ValuesEquals(object? other)
        {
            if (other == null) { return false; }
            SellOrder order = (SellOrder)other;
            return this.StockName == order.StockName
                && this.StockSymbol == order.StockSymbol
                && this.DateAndTimeOfOrder == order.DateAndTimeOfOrder
                && this.Quantity == order.Quantity
                && this.Price == order.Price;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) { return false; }
            SellOrder order = (SellOrder)obj;
            return this.SellOrderID == order.SellOrderID
                && this.StockName == order.StockName
                && this.StockSymbol == order.StockSymbol
                && this.DateAndTimeOfOrder == order.DateAndTimeOfOrder
                && this.Quantity == order.Quantity
                && this.Price == order.Price;
        }

        public override int GetHashCode()
        {
            return this.SellOrderID.GetHashCode();
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}
