using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Globalization;
using PropertyChanged;
using System.Linq;

namespace MyCrypt
{
    [AddINotifyPropertyChangedInterface]
    class PortfolioListView
    {

        public decimal bankAmount { get; set; }

        public decimal totalCryptoValue { get; set; }

        public ObservableCollection<PortfolioListModel> portfolioList { get; set; }

        List<PortfolioListModel> storagelist = new List<PortfolioListModel>();

        public PortfolioListView()
        {

            portfolioList = new ObservableCollection<PortfolioListModel>();

            creating();

            try
            {
                
            }
            catch
            {
                bankAmount = 0;
            }
            

            bool isEmpty = !storagelist.Any();

            if (!isEmpty)
            {
                insertIntoListView();
            }

            

        }

        private void creating()
        {

            if (!Application.Current.Properties.ContainsKey("PortfolioData"))
            {
                Application.Current.Properties["PortfolioData"] = "";

            }

            if (!Application.Current.Properties.ContainsKey("UsersMoney"))
            {
                decimal filler = 0;
                Application.Current.Properties["UsersMoney"] = filler;

            }

            bankAmount = (decimal)Application.Current.Properties["UsersMoney"];

            string theportfolioData = (string)Application.Current.Properties["PortfolioData"];

            List<string> portfolioLists = new List<string>(theportfolioData.Split(','));

            //creating a string from PortfolioData of ids to use for the API

            string idStr = "";

            portfolioLists.Remove("");

            string[] arrid = new string[portfolioLists.Count];

            for(int i = 0; i < portfolioLists.Count; i++)
            {
                idStr += portfolioLists[i].Split('|')[0] + ",";
                arrid[i] = portfolioLists[i].Split('|')[0];
            }

            idStr = idStr.Trim(',');

            if (idStr == "")
                return;

            dynamic thedata = APIcalls.theJasondataItem(idStr);

            portfolioList.Clear();

            for(int i =0; i < portfolioLists.Count; i++)
            {
                string stris = arrid[i];

                decimal cryptfromlst = decimal.Parse(portfolioLists[i].Split('|')[1], CultureInfo.InvariantCulture.NumberFormat);
                decimal usdamount = Convert.ToDecimal(thedata["data"][stris]["quote"][Application.Current.Properties["CurrencySettings"].ToString()]["price"]);

                totalCryptoValue += Decimal.Round(cryptfromlst * usdamount, 2);

                storagelist.Add(new PortfolioListModel
                {

                    name = thedata["data"][stris]["name"],
                    symbol = thedata["data"][stris]["symbol"],
                    holdings = Decimal.Round(cryptfromlst * usdamount, 2),
                    cryptoHoldings = Decimal.Round(cryptfromlst, 6),
                    index = thedata["data"][stris]["id"],
                    currencytype = Application.Current.Properties["CurrencySettings"].ToString(),
                    iconIdentity = "https://s2.coinmarketcap.com/static/img/coins/64x64/" + thedata.data[stris].id.ToString() + ".png",

                });
            }

        }

        public void insertIntoListView()
        {

            portfolioList.Clear();

            foreach (var item in storagelist)
            {
                portfolioList.Add(new PortfolioListModel
                {

                    name = item.name,
                    symbol = item.symbol,
                    holdings = item.holdings,
                    cryptoHoldings = item.cryptoHoldings,
                    index = item.index,
                    currencytype = item.currencytype,
                    iconIdentity = item.iconIdentity,

                });
            }
        }


    }
}
