﻿using EntitiesServiceContracts.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesServiceContracts.DTO.StockOrders
{
    public class SellOrderRequest
    {
        [Required]
        public string? StockSymbol { get; set; }

        [Required]
        public string? StockName { get; set; }

        [MaxDate("2000-01-01")]
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000)]
        public uint Quantity { get; set; }

        [Range(1.0, 10000.0)]
        public double Price { get; set; }
        public Guid SellOrderID { get; set; }
    }
}
