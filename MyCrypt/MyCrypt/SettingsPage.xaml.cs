using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyCrypt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {

        public SettingsPage()
        {
            InitializeComponent();

            if (!Application.Current.Properties.ContainsKey("CurrencySettings"))
            {
                Application.Current.Properties["CurrencySettings"] = "USD";

            }



        }

        private void picked_SelectedIndexChanged(object sender, EventArgs e)
        {

            Application.Current.Properties["CurrencySettings"] = picked.Items[picked.SelectedIndex].ToString();


        }

        private async void PortfolioReset_Clicked(object sender, EventArgs e)
        {
            if (!Application.Current.Properties.ContainsKey("PortfolioData"))
            {
                Application.Current.Properties["PortfolioData"] = "";

            }

            if (!Application.Current.Properties.ContainsKey("UsersMoney"))
            {

                Application.Current.Properties["UsersMoney"] = 0;

            }

            bool answer = await DisplayAlert("WARNING", "Would you like to reset your Portfolio?", "Yes", "No");

            if (answer)
            {
                decimal thezero = 0;
                Application.Current.Properties["PortfolioData"] = "";
                Application.Current.Properties["UsersMoney"] = thezero;
            }

        }
    }
}