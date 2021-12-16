using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using PCLStorage;
using System.Collections.ObjectModel;
using System.Reflection;
using Xamarin.Forms;
using System.Threading;

namespace MyCrypt
{

    public class FavouriteListViewModel
    {
        public ObservableCollection<MainListModel> Favouritelist { get; set; }

        public FavouriteListViewModel()
        {
            Favouritelist = new ObservableCollection<MainListModel>();

            Creating();

        }

        private void Creating()
        {

            List<string> FavouriteList = new List<string>(((string)Application.Current.Properties["Favourites"]).Split(','));

            dynamic thedata = APIcalls.theJasondataItem(((string)Application.Current.Properties["Favourites"]));

            Favouritelist.Clear();

            foreach (string id in FavouriteList)
            {
                Favouritelist.Add(new MainListModel
                {
                    name = thedata["data"][id]["name"],
                    priceUSD = (thedata["data"][id]["quote"][Application.Current.Properties["CurrencySettings"].ToString()]["price"]).ToString("0.000"), //SettingClass.makecurrencyAPICall(thedata.data[inx].quote.USD.price),
                    index = thedata["data"][id]["id"],
                    currencytype = Application.Current.Properties["CurrencySettings"].ToString(),
                    iconIdentity = "https://s2.coinmarketcap.com/static/img/coins/64x64/" + thedata["data"][id]["id"] + ".png",
                    graph7d = "https://s3.coinmarketcap.com/generated/sparklines/web/7d/usd/" + thedata["data"][id]["id"] + ".png",
                    percentageChange = "7Day % :" + Decimal.Round((decimal)thedata["data"][id]["quote"][Application.Current.Properties["CurrencySettings"].ToString()]["percent_change_7d"], 4).ToString()
                });

            }

        }



    }





}
