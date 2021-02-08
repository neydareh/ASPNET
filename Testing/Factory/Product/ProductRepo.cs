using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Testing.Models;

namespace Testing
{
    public class ProductRepo : IProductRepo
    {
        private readonly IDbConnection _conn;

        public ProductRepo(IDbConnection connection)
        {
            _conn = connection;
        }

        public void DeleteProduct (Product product)
        {
            _conn.Execute("DELETE FROM reviews WHERE ProductID = @id", new { id = product.ProductID });
            _conn.Execute("DELETE FROM sales WHERE ProductID = @id", new { id = product.ProductID });
            _conn.Execute("DELETE FROM products WHERE ProductID = @id", new { id = product.ProductID });
        }

        public IEnumerable<Category> GetCategories()
        {
            string sql = "SELECT * FROM categories;";
            return _conn.Query<Category>(sql);
        }

        public void InsertProduct(Product productToInsert)
        {
            string sql = "INSERT INTO products (Name, Price, CategoryID) " +
                "Values(@name, @price, @categoryID);";

            _conn.Execute(sql, new {
                name = productToInsert.Name,
                price = productToInsert.Price,
                categoryID = productToInsert.CategoryID
            });
        }

        public Product AssignCategory()
        {
            var categoryList = GetCategories();
            var product = new Product();
            product.Categories = categoryList;
            return product;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            string sql = "SELECT * FROM products;";
            return _conn.Query<Product>(sql);
        }

        public Product GetProduct(int id)
        {
            string sql = "SELECT * FROM products WHERE ProductID = @id;";
            return _conn.QuerySingle<Product>(sql, new { id = id });
        }

        public void UpdateProduct(Product product)
        {
            string sql = "UPDATE products SET Name = @name, Price = @price WHERE ProductID = @id";

            _conn.Execute(sql, new { 
                name = product.Name,
                price = product.Price,
                id = product.ProductID
            });
        }
    }
}
