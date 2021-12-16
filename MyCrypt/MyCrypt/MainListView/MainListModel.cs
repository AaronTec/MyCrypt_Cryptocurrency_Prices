using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MyCrypt
{
    public class MainListModel
    {
        public string name { get; set; }
        public string symbol { get; set; }
        public string currencytype { get; set; }
        public string priceUSD { get; set; }
        public int index { get; set; }
        public string iconIdentity { get; set; }
        public string graph7d { get; set; }
        public string percentageChange { get; set; }

    }
}
