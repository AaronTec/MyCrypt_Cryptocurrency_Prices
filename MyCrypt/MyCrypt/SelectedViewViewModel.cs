using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Web;
using System.Net;
using System.ComponentModel;
using PropertyChanged;

namespace MyCrypt
{
    [AddINotifyPropertyChangedInterface]
    public class SelectedViewViewModel
    {

        public string cryptoName { get; set; }
        public string symbol { get; set; }
        public string priceUSD { get; set; }
        public string percentage_1hr { get; set; }
        public string percentage_24hr { get; set; }
        public string percentage_7d { get; set; }
        public string percentage_30d { get; set; }
        public string marketCapp { get; set; }
        public string iconSource { get; set; }
        public string percentage_1hr_FrameColour { get; set; }
        public string percentage_24hr_FrameColour { get; set; }
        public string percentage_7d_FrameColour { get; set; }
        public string percentage_30d_FrameColour { get; set; }

        public SelectedViewViewModel()
        {
            Creating();
        }


        private void Creating()
        {
            string cryptID = (MainPage.SelectedMainindex).ToString();

            dynamic thedata = APIcalls.theJasondataItem(cryptID.ToString());

            string currencyType = Application.Current.Properties["CurrencySettings"].ToString();

            iconSource = "https://s2.coinmarketcap.com/static/img/coins/64x64/" + thedata["data"][cryptID]["id"].ToString() + ".png";
            cryptoName = thedata["data"][cryptID]["name"];
            symbol = thedata["data"][cryptID]["symbol"];
            priceUSD = thedata["data"][cryptID]["quote"][currencyType]["price"].ToString();
            percentage_1hr = thedata["data"][cryptID]["quote"][currencyType]["percent_change_1h"].ToString("0.00");
            percentage_24hr = thedata["data"][cryptID]["quote"][currencyType]["percent_change_24h"].ToString("0.00");
            percentage_7d = thedata["data"][cryptID]["quote"][currencyType]["percent_change_7d"].ToString("0.00");
            percentage_30d = thedata["data"][cryptID]["quote"][currencyType]["percent_change_30d"].ToString("0.00");
            marketCapp = (thedata["data"][cryptID]["quote"][currencyType]["market_cap"]).ToString("0.0"); 


            if (thedata["data"][cryptID]["quote"][currencyType]["percent_change_1h"] < 0)
                percentage_1hr_FrameColour = "#ff8b94";
             else
                percentage_1hr_FrameColour = "#a8e6cf";

            if (thedata["data"][cryptID]["quote"][currencyType]["percent_change_24h"] < 0)
                percentage_24hr_FrameColour = "#ff8b94";
            else
                percentage_24hr_FrameColour = "#a8e6cf";

            if (thedata["data"][cryptID]["quote"][currencyType]["percent_change_7d"] < 0)
                percentage_7d_FrameColour = "#ff8b94";
            else
                percentage_7d_FrameColour = "#a8e6cf";

            if (thedata["data"][cryptID]["quote"][currencyType]["percent_change_30d"] < 0)
                percentage_30d_FrameColour = "#ff8b94";
            else
                percentage_30d_FrameColour = "#a8e6cf";

        }




    }
}
