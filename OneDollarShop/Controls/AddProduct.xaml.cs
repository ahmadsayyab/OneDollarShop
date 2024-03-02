using AddProduct.Core.Contexts;
using AddProduct.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;



namespace OneDollarShop.Controls
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        
        int id;


        public AddProduct()
        {
            InitializeComponent();
        }


        public AddProduct(Product product) : this()
        {

            id = product.Id;
            txtProductName.Text = product.Name;
            txtComments.Text = product.Comments;
            txtCostPrice.Text = product.CostPrice.ToString();
            txtSalePrice.Text = product.SalePrice.ToString();
            txtStock.Text = product.Stock.ToString();
        }
       
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            using (var dbContext = new OneDollarContext())
            {
                Product? existingProduct = dbContext.Products.FirstOrDefault(p => p.Id == id);

                if (existingProduct != null)
                {
                    existingProduct.Name = txtProductName.Text;
                    existingProduct.Comments = txtComments.Text;
                    existingProduct.CostPrice = int.Parse(txtCostPrice.Text);
                    existingProduct.SalePrice = int.Parse(txtSalePrice.Text);
  
                }
                else
                {
                    Product product = new Product
                    {
                        Name = txtProductName.Text,
                        Comments = txtComments.Text,
                        CostPrice = int.Parse(txtCostPrice.Text),
                        SalePrice = int.Parse(txtSalePrice.Text),
                        Stock = int.Parse(txtStock.Text)
                    };

                    dbContext.Products.Add(product);
                   
                }
                dbContext.SaveChanges();
                //var mainWindow = Application.Current.MainWindow as ProductMainView;
                //mainWindow?.RefreshDataGrid();
                ProductMainView productMainView = new ProductMainView();
                productMainView.RefreshDataGrid();
            

                this.Close();
            }
             
        }

        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void ResetControls()
        //{
        //    txtProductName.Clear();
        //    txtComments.Clear();
        //    txtSalePrice.Clear();
        //    txtStock.Clear();
        //    txtCostPrice.Clear();
        //}

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //ResetControls();
        }
    }
}
