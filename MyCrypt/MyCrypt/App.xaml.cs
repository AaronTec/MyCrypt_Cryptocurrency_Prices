using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyCrypt
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            if (!Application.Current.Properties.ContainsKey("UsersMoney"))
            {
                decimal filler = 0;
                Application.Current.Properties["UsersMoney"] = filler;

            }

        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
