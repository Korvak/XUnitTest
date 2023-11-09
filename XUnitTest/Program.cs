//entities
using EntitiesServices.Services;
using EntitiesServiceContracts.Contracts;
//finhub
using FinhubServiceContracts.Contracts;
using FinhubServices.Services;
using FinhubServices.ConfigOptions;
//Main
using XUnitTest.ConfigOptions;

var builder = WebApplication.CreateBuilder(args);
//Finhub Service config option setup
builder.Services.Configure<FinhubOptions>(builder.Configuration.GetSection("Finhub") );
//Main config option setup
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions") );
//controllers
builder.Services.AddControllersWithViews();
//services setup
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IStocksService, StocksService>();
builder.Services.AddScoped<IFinhubService, FinhubService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
