using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using System.Web;
using Newtonsoft.Json;

using System.Collections.ObjectModel;


namespace MyCrypt
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public static int SelectedMainindex { get; set; } = 0;


        public MainPage()
        {

            InitializeComponent();

            if (!Application.Current.Properties.ContainsKey("CurrencySettings"))
            {
                Application.Current.Properties["CurrencySettings"] = "USD";

            }

            indic.IsVisible = true;
            BindingContext = new MainListViewModel();
            indic.IsVisible = false;

        }

        private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        {
            var mydetails = e.Item as MainListModel;

            SelectedMainindex = mydetails.index;

            await Navigation.PushModalAsync(new SelectedView());

        }

        private async void btn_FavouritePage_Clicked(object sender, EventArgs e)
        {

            FavouritePage contactPage = new FavouritePage();
            await Navigation.PushModalAsync(contactPage);


        }

        private async void btn_PortfolioChange_Clicked(object sender, EventArgs e)
        { 
            Portfolio contactPage = new Portfolio();
            await Navigation.PushModalAsync(contactPage);

        }


        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e) // Search bar function
        {
            listxamlView.BeginRefresh();

            var vm = BindingContext as MainListViewModel;

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                listxamlView.ItemsSource = vm.Dailylist;
            else
                listxamlView.ItemsSource = vm.Dailylist.Where(i => i.name.ToLower().Contains(e.NewTextValue.ToLower()));

            listxamlView.EndRefresh();

        }

        private async void Settings_Clicked(object sender, EventArgs e)
        {

            SettingsPage contactPage = new SettingsPage();
            await Navigation.PushModalAsync(contactPage);


        }

        protected void ListItems_Refreshing(object sender, EventArgs e)
        {
            BindingContext = new MainListViewModel();

            listxamlView.EndRefresh();
        }
        

        


    
    }
}

