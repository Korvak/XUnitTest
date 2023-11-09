using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FinhubEntities.Models;

namespace FinhubServiceContracts.DTO
{
    public class FinhubQuoteRequest
    {
        [Required]
        public string? StockSymbol { get; set; }
    }


    public class FinhubQuoteResponse
    {
        [Required]
        public double? CurrentPrice { get; set; }

        [Required]
        public double? Change { get; set; }

        [Required]
        public double? PercentageChange { get; set; }

        [Required]
        public double? DayHighPrice { get; set; }

        [Required]
        public double? DayLowPrice { get; set; }

        [Required]
        public double? DayOpenPrice { get; set; }

        [Required]
        public double? PreviousClosePrice { get; set; }
        public DateTime RetrievalDate { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj != null && obj is FinhubQuoteResponse) { 
                FinhubQuoteResponse other = (FinhubQuoteResponse) obj;

                return other.CurrentPrice == CurrentPrice
                        && other.Change == Change
                        && other.PercentageChange == PercentageChange
                        && other.DayHighPrice == DayHighPrice
                        && other.DayLowPrice == DayLowPrice
                        && other.DayOpenPrice == DayOpenPrice
                        && other.PreviousClosePrice == PreviousClosePrice;
            }
            return false;
        }

        public override int GetHashCode()
        {
            //the id is basically it's time stamp
            return RetrievalDate.GetHashCode();
        }
    }

    public static class FinhubQuoteResponseExtension
    {
        public static FinhubQuoteResponse ToQuoteResponse(this FinhubQuoteModel model)
        {
            return new FinhubQuoteResponse()
            {
                CurrentPrice = model.c,
                Change = model.d,
                PercentageChange = model.dp,
                DayHighPrice = model.h,
                DayLowPrice = model.l,
                DayOpenPrice = model.o,
                PreviousClosePrice = model.pc,
                RetrievalDate = DateTime.UtcNow
            };
        }
    }
}
