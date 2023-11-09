using Entities;
using EntitiesServiceContracts.Contracts;
using EntitiesServiceContracts.DTO.StockOrders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesServices.Services
{
    public class StocksService : IStocksService
    {
        private readonly List<BuyOrder> _buyOrders = new List<BuyOrder>();
        private readonly List<SellOrder> _sellOrders = new List<SellOrder>();

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            ModelValidatorResult result = ModelValidator.ValidateModel(buyOrderRequest);
            if ( result.Valid )
            {
                BuyOrder buyOrder = new BuyOrder()
                {
                    BuyOrderID = Guid.NewGuid(),
                    StockName = buyOrderRequest.StockName,
                    StockSymbol = buyOrderRequest.StockSymbol,
                    DateAndTimeOfOrder = buyOrderRequest.DateAndTimeOfOrder,
                    Price = buyOrderRequest.Price,
                    Quantity = buyOrderRequest.Quantity
                };
                //then we check if the buy order already exists
                if (_buyOrders.FirstOrDefault(o => o.ValuesEquals(buyOrder) ) != null ) { 
                    throw new ArgumentException("there cannot be duplicate records."); 
                }
                //we add the element
                _buyOrders.Add(buyOrder);
                //after adding it we return the added element
                return buyOrder.ToBuyOrderResponse();
            }
            throw new ValidationException(String.Join("\n", result.Errors));
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            ModelValidatorResult result = ModelValidator.ValidateModel(sellOrderRequest);
            if (result.Valid)
            {
                SellOrder sellOrder = new SellOrder()
                {
                    SellOrderID = Guid.NewGuid(),
                    StockName = sellOrderRequest.StockName,
                    StockSymbol = sellOrderRequest.StockSymbol,
                    DateAndTimeOfOrder = sellOrderRequest.DateAndTimeOfOrder,
                    Price = sellOrderRequest.Price,
                    Quantity = sellOrderRequest.Quantity
                };
                //then we check if the buy order already exists
                if (_sellOrders.FirstOrDefault(o => o.ValuesEquals(sellOrder)) != null)
                {
                    throw new ArgumentException("there cannot be duplicate records.");
                }
                //we add the element
                _sellOrders.Add(sellOrder);
                //after adding it we return the added element
                return sellOrder.ToSellOrderResponse();
            }
            throw new ValidationException(String.Join("\n", result.Errors));
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrderResponse> result = _buyOrders.Select(order => order.ToBuyOrderResponse() ).ToList();
            return result;
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrderResponse> result = _sellOrders.Select(order => order.ToSellOrderResponse()  ).ToList();
            return result;
        }
    }
}
