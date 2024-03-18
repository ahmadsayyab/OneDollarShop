using AddProduct.Core.Contexts;
using AddProduct.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class ProductStatistics : INotifyPropertyChanged
    {
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if(quantity != value)
                {
                    quantity = value;
                    OnPropertyChanged();
                }
            }

        }       
        
        private double totalPrice;
        public double TotalPrice
        {
            get { return totalPrice; }
            set
            {
                if(totalPrice != value)
                {
                    totalPrice = value;
                    OnPropertyChanged();
                }
            }

        }       
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }


    /// <summary>
    /// Interaction logic for PosMainView.xaml
    /// </summary>
    public partial class PosMainView : UserControl
    {
        ProductStatistics productStatistics = new ProductStatistics();
        //OneDollarContext onedollarContext = new OneDollarContext();
        List<Product> products { get; set; }
        
        public PosMainView()
        {
            InitializeComponent();
            LoadProudctsToListView();
            this.DataContext = productStatistics;
           
           
        }


        private void LoadProudctsToListView()
        {
            //products.Clear();
            using(var dbContext = new OneDollarDbContext())
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

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Image image)
            {
                //image.Tag = Visibility.Visible;

                if (image.Tag == null)
                {
                    image.Tag = Visibility.Visible;
                }
            }
          


        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Image image)
            {
                //image.Tag = Visibility.Collapsed;
                if (image.Tag == null)
                {
                    image.Tag = Visibility.Collapsed;
                }
            }

        }


        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {

            if (sender is Button button && button.DataContext is Product clickedProduct)
            {
                // Check if the product already exists in the DataGrid
                //var existingProduct = dgSellDetail.Items.Cast<Product>().FirstOrDefault(p => p.Name == clickedProduct.Name);

                //if (existingProduct != null)
                //{
                //    productStatistics.Quantity++;
                //    //var quantity = clickedProduct.Quantity;
                //    //quantity++;
                //    //int Quantity+= 1;
                //}
                //else
                //{
                dgSellDetail.ItemsSource = new List<Product> { clickedProduct };
                //}
            }

        }

        }
}
