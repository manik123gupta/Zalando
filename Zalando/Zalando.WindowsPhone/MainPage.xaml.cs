using System;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Zalando.Helper;
using Zalando.Network;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Zalando
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        BasicNetwork basicNetwork;
        string gender = string.Empty;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void Gender_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if ((sender as Button).Name.ToLower() == "btnmale")
            {
                btnMale.Foreground = new SolidColorBrush(Colors.Black);
                btnFemale.Foreground = new SolidColorBrush(Colors.Gray);
                gender = "male";
            }
            else
            {
                btnMale.Foreground = new SolidColorBrush(Colors.Gray);
                btnFemale.Foreground = new SolidColorBrush(Colors.Black);
                gender = "female";
            }
        }

        private void Search_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StringBuilder url = new StringBuilder(UrlResources.articles);
            int page = 1, pageSize = 5;
            url.Append("&page=" + page + "&pageSize=" + pageSize);
            if (!string.IsNullOrWhiteSpace(gender))
            {
                url.Append("&gender=" + gender);
            }
            basicNetwork = new BasicNetwork();
            basicNetwork.Url = url.ToString();
            basicNetwork.MakeRequest((obj, response) =>
            {
                var resObj = DataHelper.GetResponseObject(response);
                if (resObj.ResponseStatus == APIResponseStatus.Success)
                {
                    try
                    {

                        this.Frame.Navigate(typeof(SearchResultsPage));
                    }
                    catch (Exception ex)
                    {

                    }
                }
            });
        }

        private void Search_GotFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            StringBuilder url = new StringBuilder(UrlResources.facetsUrl);
            basicNetwork = new BasicNetwork();
            basicNetwork.Url = url.ToString();
            basicNetwork.MakeRequest((obj, response) =>
            {
                var resObj = DataHelper.GetResponseObject(response.Text, JsonResponseType.JArray);
                if (resObj.ResponseStatus == APIResponseStatus.Success)
                {
                    try
                    {
                        var token = resObj.JsonArray.GetObject("categories");
                        this.Frame.Navigate(typeof(SearchResultsPage));
                    }
                    catch (Exception ex)
                    {

                    }
                }
            });
        }
    }
}