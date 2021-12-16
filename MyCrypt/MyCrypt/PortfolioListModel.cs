using System;
using System.Collections.Generic;
using System.Text;

namespace MyCrypt
{
    public class PortfolioListModel
    {

        public string name { get; set; }
        public string symbol { get; set; }
        public string currencytype { get; set; }
        public decimal cryptoHoldings { get; set; }
        public decimal holdings { get; set; }
        public int index { get; set; }
        public string iconIdentity { get; set; }

    }
}
