using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesServiceContracts.DTO.StockOrders
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }
        public string StockSymbol { get; set; } = string.Empty;
        public string StockName { get; set; } = string.Empty;
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }

        #region methods
        public bool ValuesEquals(object? other)
        {
            if (other == null) { return false; }
            SellOrderResponse order = (SellOrderResponse)other;
            return this.StockName == order.StockName
                && this.StockSymbol == order.StockSymbol
                && this.DateAndTimeOfOrder == order.DateAndTimeOfOrder
                && this.Quantity == order.Quantity
                && this.Price == order.Price;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) { return false; }
            SellOrderResponse order = (SellOrderResponse)obj;
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

    public static class SellOrderExtension
    {
        public static SellOrderResponse ToSellOrderResponse(this SellOrder order)
        {
            return new SellOrderResponse()
            {
                SellOrderID = order.SellOrderID,
                StockName = order.StockName, //should never throw error since it's required
                StockSymbol = order.StockSymbol, //should never throw error since it's required
                DateAndTimeOfOrder = order.DateAndTimeOfOrder,
                Quantity = order.Quantity,
                Price = order.Price
            };
        }
    }
}
