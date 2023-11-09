using EntitiesServices.Services;
using EntitiesServiceContracts.Contracts;
using EntitiesServiceContracts.DTO.StockOrders;
using System.ComponentModel.DataAnnotations;
using Xunit.Abstractions;

namespace UnitTest
{
    public class StockServiceTest
    {
        private readonly ITestOutputHelper _helper;

        public StockServiceTest(ITestOutputHelper helper)
        {
            _helper = helper;
        }

        #region CreateBuyOrder
        /// <summary>
        /// when the createBuyOrder request null it has to throw argument null exception
        /// </summary>
        [Fact]
        public async void Null_CreateBuyOrder()
        {
            //arrange
            IStocksService service = new StocksService();
            BuyOrderRequest? request = null;
            //assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //act
                await service.CreateBuyOrder(request);
            });
        }

        /// <summary>
        /// when any of the required arguments of the Request model are empty then it should throw a validation error
        /// </summary>
        [Fact]
        public async void InvalidRequest_CreateBuyOrder()
        {
            //arrange
            IStocksService service = new StocksService();
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                DateAndTimeOfOrder = DateTime.UtcNow,
                Quantity = 0,
                Price = 0
            };
            //assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //act
                await service.CreateBuyOrder(request);
            });
        }


        /// <summary>
        /// when we successfully create a buyOrder, the request must be correct
        /// </summary>
        [Fact]
        public async void ValidResponse_CreateBuyOrder()
        {
            //arrange
            IStocksService service = new StocksService();
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1000,
                Quantity = 50,
                DateAndTimeOfOrder = DateTime.Parse("1922-01-11 10:10:10.232")
            };
            BuyOrderResponse expected = new BuyOrderResponse()
            {
                BuyOrderID = Guid.NewGuid(),
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1000,
                Quantity = 50,
                DateAndTimeOfOrder = DateTime.Parse("1922-01-11 10:10:10.232")
            };
            expected.DateAndTimeOfOrder = request.DateAndTimeOfOrder;
            //act
            BuyOrderResponse response = await service.CreateBuyOrder(request);
            _helper.WriteLine(response.ToString());
            //we set the GUID since it will always be different
            expected.BuyOrderID = response.BuyOrderID;
            //assert
            Assert.Equal(expected, response);
        }

        /// <summary>
        /// When we add the same object twice, it should throw error
        /// the basis is every field must be the same except for the GUID since it
        /// will always be unique.
        /// </summary>
        [Fact]
        public async void Duplicate_CreateBuyOrder()
        {
            //arrange
            IStocksService service = new StocksService();
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1000,
                Quantity = 50,
                DateAndTimeOfOrder = DateTime.Parse("1922-01-11 10:10:10.232")
            };
            //assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //act
                await service.CreateBuyOrder(request);
                await service.CreateBuyOrder(request);
            });
        }

        /// <summary>
        /// when we successfully create a buyOrder, we have to check if it was added to the entities
        /// </summary>
        [Fact]
        public async void AddedOrder_CreateBuyOrder()
        {
            //arrange
            IStocksService service = new StocksService();
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1000,
                Quantity = 50,
                DateAndTimeOfOrder = DateTime.Parse("1922-01-11 10:10:10.232")
            };
            BuyOrderResponse? expected;
            //act
            BuyOrderResponse response = await service.CreateBuyOrder(request);

            //we get the expected through the response
            List<BuyOrderResponse> buyOrders = await service.GetBuyOrders();
            //we fetch the buyOrder through the various orders
            expected = buyOrders.FirstOrDefault(r => response.BuyOrderID == r.BuyOrderID);
            //assert
            Assert.Equal(expected, response);
        }
        #endregion

        #region GetBuyOrders

        /// <summary>
        /// At Service creation the List should be empty
        /// </summary>
        [Fact]
        public async void Empty_GetBuyOrders()
        {
            //arrange
            var service = new StocksService();
            List<BuyOrderResponse> expected = new List<BuyOrderResponse>();
            //act
            List<BuyOrderResponse> orders = await service.GetBuyOrders();
            //assert
            Assert.Equal(expected, orders);
        }

        [Fact]
        public async void Added_GetBuyOrders()
        {
            //arrange
            var service = new StocksService();
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 9000,
                Quantity = 70,
                DateAndTimeOfOrder = DateTime.Parse("1922-01-11 10:10:10.232")
            };
            List<BuyOrderResponse> expected = new List<BuyOrderResponse>();
            //act
            BuyOrderResponse response = await service.CreateBuyOrder(request);
            expected.Add(response);
            List<BuyOrderResponse> orders = await service.GetBuyOrders();
            //assert
            Assert.Equal(expected, orders);
        }

        #endregion

        #region CreateSellOrder
        /// <summary>
        /// when the CreateSellOrder request null it has to throw argument null exception
        /// </summary>
        [Fact]
        public async void Null_CreateSellOrder()
        {
            //arrange
            IStocksService service = new StocksService();
            SellOrderRequest? request = null;
            //assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //act
                await service.CreateSellOrder(request);
            });
        }

        /// <summary>
        /// when any of the required arguments of the Request model are empty then it should throw a validation error
        /// </summary>
        [Fact]
        public async void InvalidRequest_CreateSellOrder()
        {
            //arrange
            IStocksService service = new StocksService();
            SellOrderRequest? request = new SellOrderRequest()
            {
                DateAndTimeOfOrder = DateTime.UtcNow,
                Quantity = 0,
                Price = 0
            };
            //assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //act
                await service.CreateSellOrder(request);
            });
        }


        /// <summary>
        /// when we successfully create a SellOrder, the request must be correct
        /// </summary>
        [Fact]
        public async void ValidResponse_CreateSellOrder()
        {
            //arrange
            IStocksService service = new StocksService();
            SellOrderRequest? request = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1000,
                Quantity = 50,
                DateAndTimeOfOrder = DateTime.Parse("1922-01-11 10:10:10.232")
            };
            SellOrderResponse expected = new SellOrderResponse()
            {
                SellOrderID = Guid.NewGuid(),
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1000,
                Quantity = 50,
                DateAndTimeOfOrder = DateTime.Parse("1922-01-11 10:10:10.232")
            };
            expected.DateAndTimeOfOrder = request.DateAndTimeOfOrder;
            //act
            SellOrderResponse response = await service.CreateSellOrder(request);
            _helper.WriteLine(response.ToString());
            //we set the GUID since it will always be different
            expected.SellOrderID = response.SellOrderID;
            //assert
            Assert.Equal(expected, response);
        }

        /// <summary>
        /// When we add the same object twice, it should throw error
        /// the basis is every field must be the same except for the GUID since it
        /// will always be unique.
        /// </summary>
        [Fact]
        public async void Duplicate_CreateSellOrder()
        {
            //arrange
            IStocksService service = new StocksService();
            SellOrderRequest? request = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1000,
                Quantity = 50,
                DateAndTimeOfOrder = DateTime.Parse("1922-01-11 10:10:10.232")
            };
            //assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //act
                await service.CreateSellOrder(request);
                await service.CreateSellOrder(request);
            });
        }

        /// <summary>
        /// when we successfully create a sellOrder, we have to check if it was added to the entities
        /// </summary>
        [Fact]
        public async void AddedOrder_CreateSellOrder()
        {
            //arrange
            IStocksService service = new StocksService();
            SellOrderRequest? request = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1000,
                Quantity = 50,
                DateAndTimeOfOrder = DateTime.Parse("1922-01-11 10:10:10.232")
            };
            SellOrderResponse? expected;
            //act
            SellOrderResponse response = await service.CreateSellOrder(request);

            //we get the expected through the response
            List<SellOrderResponse> sellOrders = await service.GetSellOrders();
            //we fetch the buyOrder through the various orders
            expected = sellOrders.FirstOrDefault(r => response.SellOrderID == r.SellOrderID);
            //assert
            Assert.Equal(expected, response);
        }
        #endregion

    }
}