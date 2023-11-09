using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Xunit.Abstractions;
using Moq;
//Service to be tested
using FinhubServiceContracts.Contracts;
using FinhubServiceContracts.DTO;
using FinhubServices.Services;
using FinhubServices.ConfigOptions;
using Microsoft.Extensions.Options;
using FinhubEntities.Models;
using System.Reflection;

namespace UnitTest
{
    public class FinhubServiceTest
    {
        private readonly ITestOutputHelper _helper;

        public FinhubServiceTest(ITestOutputHelper helper) 
        {
            _helper = helper;
        }


        protected IFinhubService GetFinhubService()
        {
            //we create the mock config
            IConfiguration config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>()
            {
                { "Finhub:Weburl" , "https://finnhub.io/api/v1/" },
                { "Finhub:QuoteUrl" , "quote" },
                { "Finhub:ApiKey" , "cko11apr01qjh1e0br0gcko11apr01qjh1e0br10" },
                { "Finhub:CompanyProfileUrl" , "stock/profile2" }
            }).Build();
            //we create the mock options
            var configMock = new Mock<IOptions<FinhubOptions>>();
            //we get the data
            FinhubOptions finhubOptions = config.GetSection("Finhub").Get<FinhubOptions>();
            //we setup the data
            configMock.Setup(o => o.Value).Returns(finhubOptions);
            IFinhubService service = new FinhubService(configMock.Object);
            return service;
        }


        [Fact]
        public async void Invalid_GetCompanyProfile() 
        {
            //arrange
            IFinhubService service = GetFinhubService();
            string stockSymbol = "";
            //assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //act
                await service.GetCompanyProfile(stockSymbol);
            });
        }


        [Fact]
        public async void Valid_GetCompanyProfile()
        {
            //arrange
            IFinhubService service = GetFinhubService();
            string stockSymbol = "MSFT";
            //since this is a real time service, the result will be updated every so often , so we need to synchronize it before doing the tests
            FinhubCompanyProfileResponse expected = new FinhubCompanyProfileResponse()
            {
                Country = "US",
                Currency = "USD",
                EstimateCurrency = "USD",
                Exchange = "NASDAQ NMS - GLOBAL MARKET",
                FinnhubIndustry = "Technology",
                Ipo = DateTime.Parse("1986-03-13"),
                Logo = "https://static2.finnhub.io/file/publicdatany/finnhubimage/stock_logo/MSFT.svg",
                MarketCapitalization = 2687654.82677846,
                Name = "Microsoft Corp",
                Phone = "14258828080.0",
                ShareOutstanding = 7432.26,
                Ticker = "MSFT",
                Weburl = "https://www.microsoft.com/en-us",
                RetrievalDate = DateTime.UtcNow
            };
            //act
            FinhubCompanyProfileResponse? actual = await service.GetCompanyProfile(stockSymbol);
            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void Invalid_GetStockQuote()
        {
            //arrange
            IFinhubService service = GetFinhubService();
            string stockSymbol = "";
            //assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //act
                await service.GetStockPriceQuote(stockSymbol);
            });
        }

        [Fact]
        public async void Valid_GetStockQuote()
        {
            //arrange
            IFinhubService service = GetFinhubService();
            string stockSymbol = "MSFT";
            //since this is a real time service, the result will be updated every so often , so we need to synchronize it before doing the tests
            FinhubQuoteResponse expected = new FinhubQuoteResponse()
            {
                CurrentPrice = 363.2,
                Change = 2.67,
                PercentageChange = 0.7406,
                DayHighPrice = 363.87,
                DayLowPrice = 360.55,
                DayOpenPrice = 361.68,
                PreviousClosePrice = 360.53,
                RetrievalDate = DateTime.UtcNow
            };
            //act
            FinhubQuoteResponse? actual = await service.GetStockPriceQuote(stockSymbol);
            //assert
            Assert.Equal(expected, actual);
        }
    }
}
