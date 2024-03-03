using AddProduct.Core.Contexts;
using AddProduct.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OneDollarShop.Controls
{
    /// <summary>
    /// Interaction logic for PosMainView.xaml
    /// </summary>
    public partial class PosMainView : UserControl
    {
        OneDollarContext onedollarContext = new OneDollarContext();
        List<Product> products { get; set; }
        public PosMainView()
        {
            InitializeComponent();
            LoadProudctsToListView();
        }


        private void LoadProudctsToListView()
        {
            //products.Clear();
            using(var dbContext = new OneDollarContext())
            {
                products = new List<Product>(dbContext.Products.ToList());
            }
            lvProductsList.ItemsSource = products;
           
        }

        private void txtSearchProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = txtSearchProduct.Text.ToLower();
            //var allPrdoucts = onedollarContext.Products;

            var findProduct = products.Where(p => p.Name.ToLower().Contains(searchTerm)).ToList();

            lvProductsList.ItemsSource = findProduct;
        }
    }
}
