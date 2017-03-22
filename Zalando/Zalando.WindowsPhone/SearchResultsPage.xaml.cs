using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Zalando.Data;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Zalando
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchResultsPage : Page
    {
        public SearchResultsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<Product> productList = new List<Product>();
            productList.Add(new Product() { Price = "12.99", ProductId = 1, ProductImage = "https://i6.ztat.net/catalog_hd/4B/E2/1I/07/EK/13/4BE21I07E-K13@14.jpg", ProductName = "Tommy HilfigerRORY - Daunenmantel - blue", ProductSize = "M" });
            productList.Add(new Product() { Price = "55.99", ProductId = 2, ProductImage = "https://i6.ztat.net/catalog_hd/4B/E2/1I/07/EK/13/4BE21I07E-K13@14.jpg", ProductName = "Tommy HilfigerGeroni -Cardigan - gray", ProductSize = "S" });
            lstProducts.ItemsSource = productList;
        }
    }
}
