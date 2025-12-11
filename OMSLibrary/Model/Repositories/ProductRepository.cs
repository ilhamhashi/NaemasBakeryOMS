using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;
using System.Data.Common;

namespace OrderManagerLibrary.Model.Repositories
{
    /// <summary>
    /// Handles database operations for products (add, update, delete, get.)
    /// </summary>
    public class ProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Creates a new ProductRepository with a database connection
        /// </summary>
        private readonly IDataAccess _db;

        public ProductRepository(IDataAccess db)
        {
            _db = db;
        }

        /// <summary>
        /// Adds a new product to the database.
        /// Returns the newly generated ProductId.
        /// </summary>
        public int Insert(Product entity)
        {
            using SqlConnection connection = _db.GetConnection();
            using (SqlCommand command = new SqlCommand("spProduct_Insert", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter outputParam = new SqlParameter("@ProductId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@Description", entity.Description);
                command.Parameters.AddWithValue("@Price", entity.Price);
                command.Parameters.Add(outputParam);

                connection.Open();
                command.ExecuteNonQuery();
                return (int)outputParam.Value;
            }
        }

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        public void Update(Product entity)
        {
            using SqlConnection connection = _db.GetConnection();
            using (SqlCommand command = new SqlCommand("spProduct_Update", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", entity.ProductId);
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@Description", entity.Description);
                command.Parameters.AddWithValue("@Price", entity.Price);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes a product from the database using ProductId.
        /// </summary>
        /// <param name="keyValues">The ID of the product to delete.</param>
        public void Delete(params object[] keyValues)
        {
            using SqlConnection connection = _db.GetConnection();
            using (SqlCommand command = new SqlCommand("spProduct_Delete", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", keyValues[0]);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Retrieves a product by ID from the database.
        /// Returns null if no match is found.
        /// </summary>
        public Product GetById(params object[] keyValues)
        {
            Product product = null;
            using SqlConnection connection = _db.GetConnection();
            using (SqlCommand command = new SqlCommand("spProduct_GetById", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", keyValues[0]);
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    product = new Product(
                        (int)reader["ProductId"],
                        (string)reader["Name"],
                        (string)reader["Description"],
                        (decimal)reader["Price"]);
                }
                return product;
            }
        }

        /// <summary>
        /// Retrieves all products stored in the database.
        /// </summary>
        /// <returns>A list of all products.</returns>
        public IEnumerable<Product> GetAll()
        {
            var products = new List<Product>();
            using SqlConnection connection = _db.GetConnection();
            using (SqlCommand command = new SqlCommand("spProduct_GetAll", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product(
                        (int)reader["ProductId"],
                        (string)reader["Name"],
                        (string)reader["Description"],
                        (decimal)reader["Price"]));
                }
                return products;
            }
        }
    }
}
