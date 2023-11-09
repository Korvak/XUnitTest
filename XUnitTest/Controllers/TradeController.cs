using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
//main
using XUnitTest.ConfigOptions;
//Finhub
using FinhubServiceContracts.Contracts;
using FinhubServiceContracts.DTO;
//Entities
using EntitiesServiceContracts.Contracts;
using EntitiesServiceContracts.DTO;
using EntitiesServiceContracts.DTO.StockOrders;

namespace FinhubStockTest.Controllers
{
    public class TradeController : Controller
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinhubService _finhubService;
        private readonly IStocksService _stocksService;

        public TradeController(IFinhubService finhubService, IStocksService stocksService, IOptions<TradingOptions> options) {
            _tradingOptions = options.Value;
            _finhubService = finhubService;
            _stocksService = stocksService;
        }


        [Route("/")]
        public IActionResult Home()
        {
            return RedirectToActionPermanent("Index");
        }


        [Route("Trade/Index/{stockSymbol?}")]
        public async Task<IActionResult> Index([FromRoute] string? stockSymbol)
        {
            if (string.IsNullOrWhiteSpace(stockSymbol) )
            {
                stockSymbol = _tradingOptions.DefaultStockSymbol ?? "";
            }
            FinhubCompanyProfileResponse? companyProfile = await _finhubService.GetCompanyProfile(
                new FinhubCompanyProfileRequest()
                {
                    StockSymbol = stockSymbol
                });
            FinhubQuoteResponse? stockQuote = await _finhubService.GetStockPriceQuote(
                new FinhubQuoteRequest()
                {
                    StockSymbol = stockSymbol
                });
            BuyOrderRequest request = new BuyOrderRequest()
            {
                StockName = companyProfile?.Name,
                StockSymbol = stockSymbol,
                Price = stockQuote?.CurrentPrice ?? -1,
                Quantity =  1,
            };
            //we make the request
            BuyOrderResponse? model = await _stocksService.CreateBuyOrder(request);
            return View(model);
        }
    }
}
