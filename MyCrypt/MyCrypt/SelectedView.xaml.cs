using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MyCrypt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectedView : ContentPage
    {
        int index = MainPage.SelectedMainindex;

        public SelectedView()
        {
            InitializeComponent();

            BindingContext = new SelectedViewViewModel();

            favButtonChecker();
        }

        private async void btn_Favourite_Clicked(object sender, EventArgs e)
        {
            

            /* Store the favourites in session data with a string. To sort through the string
             it gets turned into a list*/

            if (!Application.Current.Properties.ContainsKey("Favourites"))
            {
                Application.Current.Properties["Favourites"] = "";
            }
            else
            {
                try
                {
                    string theFavourties = (string)Application.Current.Properties["Favourites"];

                    List<string> FavouriteList = new List<string>(theFavourties.Split(',')); //The session data turning into a list

                    /* checks to see if the Favorites(session data) holds the selected crypto, if it does it will remove it
                     otherwise it will add it to the favourites*/

                    if (FavouriteList.Exists(element => element == index.ToString()))
                    {
                        FavouriteList.Remove(index.ToString());
                        Application.Current.Properties["Favourites"] = String.Join(",", FavouriteList);
                        btn_Favourite.Source = ImageSource.FromFile("nonFave.png");
                    }
                    else
                    {

                        Application.Current.Properties["Favourites"] = ((string)Application.Current.Properties["Favourites"] +
                            ',' + index.ToString()).TrimStart(',') ;
                        btn_Favourite.Source = ImageSource.FromFile("Fave.png");
                    }
                    
                }
                catch
                {
                    await DisplayAlert("Alert", "Something went wrong", "OK");

                }


            }

        }

        private async void btn_back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void favButtonChecker()
        {
            if (!Application.Current.Properties.ContainsKey("Favourites"))
            {
                Application.Current.Properties["Favourites"] = "";

            }

            /* Checks the session data to see if the selected crypto is a favourited*/

            try
            {
                if (Array.Exists(((string)Application.Current.Properties["Favourites"]).Split(','), element => element == index.ToString()))
                    btn_Favourite.Source = ImageSource.FromFile("Fave.png");
                else
                    btn_Favourite.Source = ImageSource.FromFile("nonFave.png");

            }
            catch
            {
                btn_Favourite.Source = ImageSource.FromFile("FaveImages/nonFave.png");
                await DisplayAlert("Alert", "Something went wrong", "OK");
            }

        }

        private async void btn_purchase_Clicked(object sender, EventArgs e)
        {

            if (!Application.Current.Properties.ContainsKey("PortfolioData"))
            {
                Application.Current.Properties["PortfolioData"] = "";

            }

            decimal bankAmount = (decimal)Application.Current.Properties["UsersMoney"];

            string theportfolioData = (string)Application.Current.Properties["PortfolioData"];

            List<string> portfolioList = new List<string>(theportfolioData.Split(','));

            decimal exchangerate, quantcrypto = 0, holding, priceOfCrypt, userBuyAmount;
            bool trigger = false;

            portfolioList.Remove("");


            for (int inx = 0; inx <= (portfolioList.Count - 1); inx++)
            {
                if (portfolioList[inx].Split('|')[0] == index.ToString())
                {
                    holding = decimal.Parse(portfolioList[inx].Split('|')[1], CultureInfo.InvariantCulture.NumberFormat);
                    priceOfCrypt = decimal.Parse(lbl_thePrice.Text, CultureInfo.InvariantCulture.NumberFormat);


                    string header = "Buying " + lbl_cryptName.Text;

                    string discription = String.Format("You own: {0}\nBank Amount: {1}\nPrice for 1: {2}", Decimal.Round((holding * priceOfCrypt), 2), bankAmount ,lbl_thePrice.Text );

                    exchangerate = 1 / priceOfCrypt;

                    string userSellAmountstr = await DisplayPromptAsync(header, discription, maxLength: 100000, keyboard: Keyboard.Numeric);

                    if (Decimal.TryParse(userSellAmountstr, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat, out userBuyAmount))
                    {

                        if (userBuyAmount > bankAmount)
                        {
                            trigger = true;
                            await DisplayAlert("Warning", "You have insufficient funds", "OK");
                            break;
                        }

                        Application.Current.Properties["UsersMoney"] = bankAmount - userBuyAmount; // deducting amount from bank

                        quantcrypto = exchangerate * userBuyAmount;

                        portfolioList[inx] = index.ToString() + "|" + (holding + quantcrypto).ToString();

                        portfolioList.Remove("");

                        Application.Current.Properties["PortfolioData"] = String.Join(",", portfolioList);

                        portfolioList.Remove("");

                        trigger = true;

                        break;

                    }
                    else if (userSellAmountstr == null)
                    {
                        trigger = true;
                        break;
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Please enter a valid number", "OK");
                        trigger = true;
                        break;
                    }
                }


            }

            if (!trigger)
            {
                string header = "Buying " + lbl_cryptName.Text;

                string discription = String.Format("You own: 0\nBank Amount: {0}\nPrice for 1: {1}",  bankAmount, lbl_thePrice.Text);


                priceOfCrypt = decimal.Parse(lbl_thePrice.Text, CultureInfo.InvariantCulture.NumberFormat);

                exchangerate = 1 / priceOfCrypt;

                string userSellAmountstr = await DisplayPromptAsync(header, discription, maxLength: 100000, keyboard: Keyboard.Numeric);

                if (Decimal.TryParse(userSellAmountstr, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat, out userBuyAmount))
                {

                    decimal usermoney = (decimal)Application.Current.Properties["UsersMoney"];

                    if (userBuyAmount > usermoney)
                    {
                        await DisplayAlert("Warning", "You have insufficient funds", "OK");
                    }
                    else
                    {

                        Application.Current.Properties["UsersMoney"] = usermoney - userBuyAmount;

                        quantcrypto = exchangerate * userBuyAmount;

                        portfolioList.Add(index.ToString() + "|" + quantcrypto.ToString() + ",");

                        portfolioList.Remove("");

                        Application.Current.Properties["PortfolioData"] = String.Join(",", portfolioList);
                    }


                }


            }


        }

        private async void btn_sell_Clicked(object sender, EventArgs e)
        {
            if (!Application.Current.Properties.ContainsKey("PortfolioData"))
            {
                Application.Current.Properties["PortfolioData"] = "";

            }

            decimal bankAmount = (decimal)Application.Current.Properties["UsersMoney"];

            string theportfolioData = (string)Application.Current.Properties["PortfolioData"];

            List<string> portfolioList = new List<string>(theportfolioData.Split(','));

            decimal exchangerate, quantcrypto = 0, holding, priceOfCrypt, userSellAmount;
            bool trigger = false;

            portfolioList.Remove("");


            for (int inx = 0; inx < portfolioList.Count; inx++)
            {
                if (portfolioList[inx].Split('|')[0] == index.ToString())
                {
                    holding = decimal.Parse(portfolioList[inx].Split('|')[1], CultureInfo.InvariantCulture.NumberFormat);
                    priceOfCrypt = decimal.Parse(lbl_thePrice.Text, CultureInfo.InvariantCulture.NumberFormat);


                    string header = "Selling" + lbl_cryptName.Text;

                    string discription = String.Format("You own: {0}\n Bank Amount: {1}\n Price for 1: {2}", Decimal.Round((holding * priceOfCrypt), 2), bankAmount, lbl_thePrice.Text);


                    exchangerate = 1 / priceOfCrypt;

                    string userSellAmountstr = await DisplayPromptAsync(header, discription, maxLength: 100000, keyboard: Keyboard.Numeric);

                    if (Decimal.TryParse(userSellAmountstr, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat, out userSellAmount))
                    {

                        

                        quantcrypto = exchangerate * userSellAmount;

                        if (holding > quantcrypto)
                        {

                            portfolioList[inx] = index.ToString() + "|" + (holding - quantcrypto).ToString();

                            portfolioList.Remove("");

                            Application.Current.Properties["PortfolioData"] = String.Join(",", portfolioList);

                            Application.Current.Properties["UsersMoney"] = (decimal)Application.Current.Properties["UsersMoney"] + userSellAmount; // adding amount to bank

                            trigger = true;

                            break;


                        }
                        else
                        {
                            bool answer = await DisplayAlert("Question?", "Would you like to sell all your Cryptocurrency?", "Yes", "No");

                            trigger = true;

                            if (answer)
                            {
                                portfolioList.RemoveAt(inx);
                                Application.Current.Properties["PortfolioData"] = String.Join(",", portfolioList);

                                Application.Current.Properties["UsersMoney"] = (decimal)Application.Current.Properties["UsersMoney"] + Decimal.Round((holding * priceOfCrypt), 2); // adding amount to bank

                                trigger = true;

                                break;
                            }
                            else
                            {
                                break;
                            }
                        }


                    }
                    else
                    {
                        trigger = true;
                    }

                }

            }

            if (!trigger) // If the user has not purchased and of the Selected Crypto
            {

                await DisplayAlert("Alert", "You have nothing to sell", "OK");
                trigger = false;
            }




        }

    }
}