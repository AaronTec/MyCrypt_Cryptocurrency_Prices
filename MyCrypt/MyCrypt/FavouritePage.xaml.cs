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
    public partial class FavouritePage : ContentPage
    {
        public FavouritePage()
        {
            InitializeComponent();

            if (!Application.Current.Properties.ContainsKey("Favourites"))
            {
                Application.Current.Properties["Favourites"] = "";

            }

            BindingContext = new FavouriteListViewModel();

        }

        private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        {
            var mydetails = e.Item as MainListModel;

            MainPage.SelectedMainindex = mydetails.index;

            await Navigation.PushModalAsync(new SelectedView());

        }

        protected void ListItems_Refreshing(object sender, EventArgs e)
        {

            BindingContext = new FavouriteListViewModel();

            listxamlView.EndRefresh();
        }

    }
}