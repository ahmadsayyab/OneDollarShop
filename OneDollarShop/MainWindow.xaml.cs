using AddProduct.Core.Contexts;
using AddProduct.Core.Model;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OneDollarShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OneDollarContext context = new OneDollarContext();
        AddProduct addProduct;

        public ObservableCollection<Product> Products { get; set; }
        public MainWindow()
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

        //private void LoadData()
        //{
           
        //    var productDetail = context.Products.ToList();
        //    dgProductDetail.ItemsSource = productDetail;
          
        //}

        public void RefreshDataGrid()
        {
            Products.Clear();
            using (var dbContext = new OneDollarContext())
            {
                Products = new ObservableCollection<Product>(dbContext.Products.ToList());
            }

            dgProductDetail.ItemsSource = Products;

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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Product editProduct)
            {
                using (var dbContext = new OneDollarContext())
                {
                    // Retrieve the product details from SQLite based on the ID
                    Product? productToEdit = dbContext.Products.FirstOrDefault(p => p.Id == editProduct.Id);
                    addProduct = new AddProduct(productToEdit);
                    addProduct.ShowDialog();
                    
                 
                }
            }


        }

        private void txtSearchProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = txtSearchProduct.Text.ToLower();
            var products = context.Products;

           
            var searchProducts = products.Where(p =>
                p.Name.ToLower().Contains(searchTerm) ||
                p.Comments.ToLower().Contains(searchTerm)
            ).ToList();

            dgProductDetail.ItemsSource = searchProducts;
        }

    
    }
}