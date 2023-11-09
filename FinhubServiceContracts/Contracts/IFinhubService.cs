using FinhubServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinhubServiceContracts.Contracts
{
    public interface IFinhubService
    {

        Task<FinhubCompanyProfileResponse?> GetCompanyProfile(string stockSymbol);
        Task<FinhubQuoteResponse?> GetStockPriceQuote(string stockSymbol);

    }
}
