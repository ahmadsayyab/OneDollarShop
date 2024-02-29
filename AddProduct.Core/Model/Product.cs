using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddProduct.Core.Model
{
    //public class Product
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Comments { get; set; }
    //    public int CostPrice { get; set; }
    //    public int SalePrice { get; set; }
    //    public int Stock { get; set; }
    //}


    public class Product : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string comments;
        public string Comments
        {
            get { return comments; }
            set
            {
                if (comments != value)
                {
                    comments = value;
                    OnPropertyChanged(nameof(Comments));
                }
            }
        }

        private int costPrice;
        public int CostPrice
        {
            get { return costPrice; }
            set
            {
                if (costPrice != value)
                {
                    costPrice = value;
                    OnPropertyChanged(nameof(CostPrice));
                }
            }
        }

        private int salePrice;
        public int SalePrice
        {
            get { return salePrice; }
            set
            {
                if (salePrice != value)
                {
                    salePrice = value;
                    OnPropertyChanged(nameof(SalePrice));
                }
            }
        }

        private int stock;
        public int Stock
        {
            get { return stock; }
            set
            {
                if (stock != value)
                {
                    stock = value;
                    OnPropertyChanged(nameof(Stock));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
