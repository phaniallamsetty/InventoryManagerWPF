using InventoryManagerWPF.DAL;
using InventoryManagerWPF.Models;
using System.Configuration;
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

namespace InventoryManagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProductRepository _productRepository;
        private List<Product> _products;
        public MainWindow()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["InventoryDB"].ConnectionString;
            _productRepository = new ProductRepository(connectionString);
            LoadProducts();
        }

        private void LoadProducts() 
        {
            _products = _productRepository.GetAllProducts().ToList();
            ProductsDataGrid.ItemsSource = _products;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var newProduct = new Product
            {
                ProductName = ProductNameTextBox.Text,
                Category = CategoryTextBox.Text,
                Quantity = int.Parse(QuantityTextBox.Text),
                Price = decimal.Parse(PriceTextBox.Text)
            };

            _productRepository.AddProduct(newProduct);
            LoadProducts();
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is Product selectedProduct) 
            {
                selectedProduct.ProductName = ProductNameTextBox.Text;
                selectedProduct.Category = CategoryTextBox.Text;
                selectedProduct.Quantity = int.Parse(QuantityTextBox.Text);
                selectedProduct.Price = decimal.Parse(PriceTextBox.Text);

                _productRepository.UpdateProduct(selectedProduct);
                LoadProducts();
            }
        }
    }
}