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
    /// Interaction logic for ProductMainView.xaml
    /// </summary>
    public partial class ProductMainView : UserControl
    {
        OneDollarDbContext context = new OneDollarDbContext();
        AddProduct addProduct;
        public ObservableCollection<Product> Products { get; set; }
        
        public ProductMainView()
        {
            InitializeComponent();
            Products = new ObservableCollection<Product>();
            RefreshDataGrid();

        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            addProduct = new AddProduct();
            addProduct.ShowDialog();
        }

        public void RefreshDataGrid()
        {
            Products.Clear();
            using (var dbContext = new OneDollarDbContext())
            {
                Products = new ObservableCollection<Product>(dbContext.Products.ToList());
            }

            dgProductDetail.ItemsSource = Products;

        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Product editProduct)
            {
                using (var dbContext = new OneDollarDbContext())
                {
                    // Retrieve the product details from SQLite based on the ID
                    Product? productToEdit = dbContext.Products.FirstOrDefault(p => p.Id == editProduct.Id);
                    addProduct = new AddProduct(productToEdit);
                    addProduct.ShowDialog();


                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Product deleteProduct)
            {
                MessageBoxResult confirmation = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmation == MessageBoxResult.Yes)
                {
                    context.Products.Remove(deleteProduct);
                    context.SaveChanges();
                    RefreshDataGrid();

                }
            }
        }

        private void txtSearchProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = txtSearchProduct.Text.ToLower();
            //var products = context.Products;


            var searchProducts = Products.Where(p =>
                p.Name.ToLower().Contains(searchTerm) ||
                p.Comments.ToLower().Contains(searchTerm)
            ).ToList();

            dgProductDetail.ItemsSource = searchProducts;
        }
    }
}
