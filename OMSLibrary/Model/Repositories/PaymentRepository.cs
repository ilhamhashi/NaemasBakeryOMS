using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;

/// <summary>
/// Handles database operations for Payment entities (add, update, delete, get).
/// </summary>

public class PaymentRepository : IRepository<Payment>
{
    private readonly IDataAccess _db;

    /// <summary>
    /// Creates a new PaymentRepository with a database connection.
    /// </summary>

    public PaymentRepository(IDataAccess db)
    {
        _db = db;
    }

    /// <summary>
    /// Adds a new Payment record to the database.
    /// Returns the newly generated PaymentId.
    /// </summary>

    public int Insert(Payment entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPayment_Insert", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter outputParam = new SqlParameter("@PaymentId", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            command.Parameters.AddWithValue("@PaymentDate", entity.PaymentDate);
            command.Parameters.AddWithValue("@PaymentAmount", entity.PaymentAmount);
            command.Parameters.AddWithValue("@OrderId", entity.OrderId);
            command.Parameters.AddWithValue("@PaymentMethodId", entity.PaymentMethodId);
            command.Parameters.Add(outputParam);

            connection.Open();
            command.ExecuteNonQuery();
            return (int)outputParam.Value;
        }
    }
    /// <summary>
    /// Updates an existing Payment record in the database.
    /// </summary>

    public void Update(Payment entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPayment_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PaymentId", entity.OrderId);
            command.Parameters.AddWithValue("@PaymentDate", entity.PaymentDate);
            command.Parameters.AddWithValue("@PaymentAmount", entity.PaymentAmount);
            command.Parameters.AddWithValue("@OrderId", entity.OrderId);
            command.Parameters.AddWithValue("@PaymentMethodId", entity.PaymentMethodId);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Deletes a Payment record from the database using PaymentId.
    /// </summary>

    public void Delete(params object[] keyValues)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPayment_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PaymentId", keyValues[0]);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Retrieves a Payment record by ID from the database.
    /// Returns null if no match is found.
    /// </summary>

    public Payment GetById(params object[] keyValues)
    {
        Payment payment = null;
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPayment_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PaymentId", keyValues[0]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                payment = new Payment
                    ((int)reader["PaymentId"],
                    (decimal)reader["PaymentAmount"],
                    (DateTime)reader["PaymentDate"],
                    (int)reader["OrderId"],
                    (int)reader["PaymentMethodId"]);
            }
            return payment;
        }
    }
    /// <summary>
    /// Retrieves all Payment records stored in the database.
    /// </summary>
    public IEnumerable<Payment> GetAll()
    {
        var payments = new List<Payment>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPayment_GetAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                payments.Add(new Payment
                (
                    (int)reader["PaymentId"],
                    (decimal)reader["PaymentAmount"],
                    (DateTime)reader["PaymentDate"],
                    (int)reader["OrderId"],
                    (int)reader["PaymentMethodId"]
                ));
            }
            return payments;
        }
    }
}
