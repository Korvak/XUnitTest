using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EntitiesServiceContracts.DTO.StockOrders
{
    public class BuyOrderResponse
    {
        public Guid BuyOrderID { get; set; }
        public string StockSymbol { get; set; } = string.Empty;
        public string StockName { get; set; } = string.Empty;
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) { return false; }
            BuyOrderResponse order = (BuyOrderResponse) obj;
            return this.BuyOrderID == order.BuyOrderID
                && this.StockName == order.StockName
                && this.StockSymbol == order.StockSymbol
                && this.DateAndTimeOfOrder == order.DateAndTimeOfOrder
                && this.Quantity == order.Quantity
                && this.Price == order.Price;
        }

        public override int GetHashCode()
        {
            return BuyOrderID.GetHashCode();
        }

    }





    public static class BuyOrderExtension {

        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder order)
        {
            return new BuyOrderResponse()
            {
                //the values cannot be empty or they will fail the [Required] attribute validation
                BuyOrderID = order.BuyOrderID,
                StockName = order.StockName,
                StockSymbol = order.StockSymbol,
                DateAndTimeOfOrder = order.DateAndTimeOfOrder,
                Quantity = order.Quantity,
                Price = order.Price,
            };
        }
    }
}
