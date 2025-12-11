using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
/// <summary>
/// Manages Order records in the database (add, update, delete, get).
/// </summary>
public class OrderRepository : IRepository<Order>
{
    private readonly IDataAccess _db;
    /// <summary>
    /// Creates a new OrderRepository with a database connection.
    /// </summary>

    public OrderRepository(IDataAccess db)
    {
        _db = db;
    }

    /// <summary>
    /// Adds a new Order to the database and returns its generated OrderId.
    /// </summary>

    public int Insert(Order entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrder_Insert", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter outputParam = new SqlParameter("@OrderId", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            command.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
            command.Parameters.AddWithValue("@OrderStatus", entity.Status);
            command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
            command.Parameters.Add(outputParam);

            connection.Open();
            command.ExecuteNonQuery();
            return (int)outputParam.Value;
        }
    }

    /// <summary>
    /// Updates an existing Order in the database.
    /// </summary>

    public void Update(Order entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrder_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@OrderId", entity.OrderId);
            command.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
            command.Parameters.AddWithValue("@OrderStatus", entity.Status);
            command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }


    /// <summary>
    /// Deletes an Order from the database using its OrderId.
    /// </summary>

    public void Delete(params object[] keyValues)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrder_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@OrderId", keyValues[0]);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Finds an Order by OrderId. Returns null if not found.
    /// </summary>
    public Order GetById(params object[] keyValues)
    {
        Order order = null;
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrder_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@OrderId", keyValues[0]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                order = new Order
                    ((int)reader["OrderId"],
                    (DateTime)reader["OrderDate"],
                    (OrderStatus)Enum.Parse(typeof(OrderStatus), (string)reader["OrderStatus"]),
                    (int)reader["CustomerId"]);
            }
            return order;
        }
    }

    /// <summary>
    /// Returns all Orders in the database.
    /// </summary>

    public IEnumerable<Order> GetAll()
    {
        var orders = new List<Order>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrder_GetAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                orders.Add(new Order
                (
                    (int)reader["OrderId"],
                    (DateTime)reader["OrderDate"],
                    (OrderStatus)Enum.Parse(typeof(OrderStatus), (string)reader["OrderStatus"]),
                    (int)reader["CustomerId"]
                ));
            }
            return orders;
        }
    }
}

