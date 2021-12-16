using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Web;
using System.Net;
using PCLStorage;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyCrypt
{


    class APIcalls
    {

        public static string makeAPICallItem(string id)
        {
            string currency = Application.Current.Properties["CurrencySettings"].ToString();

            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["id"] = id;
            queryString["convert"] = Application.Current.Properties["CurrencySettings"].ToString();//currency;

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", "b6c51de4-5dbc-4862-b03b-3526b0a49fbd");
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());


        }

        public static string makeAPICall()
        {
            string currency = Application.Current.Properties["CurrencySettings"].ToString();


            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "150";
            queryString["convert"] = Application.Current.Properties["CurrencySettings"].ToString();//currency;

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", "b6c51de4-5dbc-4862-b03b-3526b0a49fbd");
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());


        }

        public static dynamic theJasondata()
        {

            dynamic thedata = JsonConvert.DeserializeObject(APIcalls.makeAPICall());

            return thedata;
        }

        public static dynamic theJasondataItem(String theID)
        {

            dynamic thedata = JsonConvert.DeserializeObject(APIcalls.makeAPICallItem(theID));

            return thedata;
        }
    }

}
