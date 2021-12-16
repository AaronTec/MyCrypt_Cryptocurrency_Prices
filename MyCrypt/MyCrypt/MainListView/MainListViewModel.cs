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

namespace MyCrypt
{

    public class MainListViewModel
    {

        public ObservableCollection<MainListModel> Dailylist { get; set; }

        string currency = Application.Current.Properties["CurrencySettings"].ToString();


        public MainListViewModel()
        {
            Dailylist = new ObservableCollection<MainListModel>();

            Creating();

        }

        private void Creating()
        {
            //Collects the content from the API and displays it in the List View
            dynamic thedata = APIcalls.theJasondata();

            for (int i = 0; i < thedata["data"].Count; i++)
            {
                Dailylist.Add(new MainListModel
                {
                    name = thedata["data"][i]["name"],
                    priceUSD = (thedata["data"][i]["quote"][Application.Current.Properties["CurrencySettings"].ToString()]["price"]).ToString("0.000"),
                    index = thedata["data"][i]["id"],
                    currencytype = Application.Current.Properties["CurrencySettings"].ToString(),
                    iconIdentity = "https://s2.coinmarketcap.com/static/img/coins/64x64/" + thedata["data"][i]["id"] + ".png",
                    graph7d = "https://s3.coinmarketcap.com/generated/sparklines/web/7d/usd/" + thedata["data"][i]["id"] + ".png",
                    percentageChange = "7Day % :" + Decimal.Round((decimal)thedata["data"][i]["quote"][Application.Current.Properties["CurrencySettings"].ToString()]["percent_change_7d"], 4).ToString()
                });

            }

        }

    }





}
