using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyCrypt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Portfolio : ContentPage
    {
        //public static int Selectedportfolioindex { get; set; } = 0;

        public Portfolio()
        {
            InitializeComponent();

            BindingContext = new PortfolioListView();
        }

        protected void ListItems_Refreshing(object sender, EventArgs e)
        {

            BindingContext = new PortfolioListView();

            listxamlView.EndRefresh();
        }

        private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        {
            var mydetails = e.Item as PortfolioListModel;

            MainPage.SelectedMainindex = mydetails.index;

            await Navigation.PushModalAsync(new SelectedView());
        }


        protected async void btn_Bank_Add_Clicked(object sender, EventArgs e)
        {

            if (!Application.Current.Properties.ContainsKey("PortfolioData"))
            {
                Application.Current.Properties["PortfolioData"] = "";

            }

            if (!Application.Current.Properties.ContainsKey("UsersMoney"))
            {

                Application.Current.Properties["UsersMoney"] = 0;

            }

            decimal bankAmount = 0, usersinputdec;

            if (!Application.Current.Properties.ContainsKey("UsersMoney"))
            {

                Application.Current.Properties["UsersMoney"] = bankAmount;

            }

            bankAmount = (decimal)Application.Current.Properties["UsersMoney"];

            string discription = "You own: " + bankAmount;

            string userinput = await DisplayPromptAsync("Adding to Bank Account", discription, maxLength: 100000, keyboard: Keyboard.Numeric);

            if (Decimal.TryParse(userinput, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat, out usersinputdec))
            {
                Application.Current.Properties["UsersMoney"] = (bankAmount + usersinputdec);

            }

            BindingContext = new PortfolioListView();

            listxamlView.EndRefresh();

        }
    }
}