using Dapper;
using InventoryManagerWPF.Models;
using System.Data.SqlClient;

namespace InventoryManagerWPF.DAL
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Product>("SELECT * FROM Products");
            }
        }

        public void AddProduct(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "INSERT INTO Products (ProductName, Category, Quantity, Price) VALUES (@ProductName, @Category, @Quantity, @Price)";
                connection.Execute(sql, product);
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "UPDATE Products SET ProductName = @ProductName, Category = @Category, Quantity = @Quantity, Price = @Price WHERE ProductID = @ProductID";
                connection.Execute(sql, product);
            }
        }

        public void DeleteProduct(int productID) 
        {
            using (var connection = new SqlConnection(_connectionString)) 
            {
                Product product = new Product();
                product.ProductID = productID;
                var sql = "DELETE FROM Products WHERE ProductID = @ProductID";
                connection.Execute(sql, product);
            }
        }
    }
}
