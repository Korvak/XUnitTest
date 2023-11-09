using FinhubEntities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinhubServiceContracts.DTO
{
    public class FinhubCompanyProfileRequest
    {
        [Required]
        public string? StockSymbol { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj != null || obj is FinhubCompanyProfileRequest)
            {
                FinhubCompanyProfileRequest data = (FinhubCompanyProfileRequest) obj;
                return data.StockSymbol == StockSymbol;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return StockSymbol?.GetHashCode() ?? base.GetHashCode();
        }
    }


    public class FinhubCompanyProfileResponse
    {
        [Required]
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Country { get; set; }

        [Required]
        public string? Currency { get; set; }
        public string? EstimateCurrency { get; set; }
        public string? Logo { get; set; }
        public string? Weburl { get; set; }

        [Required]
        public string? Ticker { get; set; }
        public string? FinnhubIndustry { get; set; }

        [Required]
        public DateTime? Ipo { get; set; }
        public string? Exchange { get; set; }

        [Required]
        public double? MarketCapitalization { get; set; }
        public double? ShareOutstanding { get; set; }
        public DateTime RetrievalDate { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj != null || obj is FinhubCompanyProfileResponse)
            {
                FinhubCompanyProfileResponse data = (FinhubCompanyProfileResponse)obj;
                return data.Name == Name
                    && data.Country == Country
                    && data.Currency == Currency
                    && data.Ticker == Ticker
                    && data.Ipo == Ipo
                    && data.Exchange == Exchange
                    && data.FinnhubIndustry == FinnhubIndustry;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Ticker?.GetHashCode() ?? base.GetHashCode();
        }
    }

    public static class FinhubCompanyProfileExtension
    {
        public static FinhubCompanyProfileResponse ToCompanyProfileResponse(this FinhubCompanyProfileModel model )
        {
            return new FinhubCompanyProfileResponse()
            {
                Name = model.name,
                Phone = model.phone,
                Country = model.country,
                Currency = model.currency,
                EstimateCurrency = model.estimateCurrency,
                Logo = model.logo,
                Weburl = model.weburl, 
                Ticker = model.ticker,
                Exchange = model.exchange,
                FinnhubIndustry = model.finnhubIndustry,
                Ipo = model.ipo,
                MarketCapitalization = model.marketCapitalization,
                ShareOutstanding = model.shareOutstanding,
                RetrievalDate = DateTime.UtcNow
            };
        }
    }
}
