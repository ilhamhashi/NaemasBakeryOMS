using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
/// <summary>
/// Handles database operations for PickUp entities (add, update, delete, get).
/// </summary>
public class PickUpRepository : IRepository<PickUp>
{
    private readonly IDataAccess _db;
    /// <summary>
    /// Creates a new PickUpRepository with a database connection.
    /// </summary>

    public PickUpRepository(IDataAccess db)
    {
        _db = db;
    }
    /// <summary>
    /// Adds a new PickUp record to the database.
    /// Returns the newly generated CollectionId.
    /// </summary>
    /// <param name="entity">The PickUp record to add.</param>
    /// <returns>The ID of the new collection.</returns>

    public int Insert(PickUp entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPickUp_Insert", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter outputParam = new SqlParameter("@CollectionId", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            command.Parameters.AddWithValue("@CollectionDate", entity.CollectionDate);
            command.Parameters.AddWithValue("@OrderId", entity.OrderId);
            command.Parameters.Add(outputParam);

            connection.Open();
            command.ExecuteNonQuery();
            return (int)outputParam.Value;
        }
    }
    /// <summary>
    /// Updates an existing PickUp record in the database.
    /// </summary>

    public void Update(PickUp entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPickUp_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CollectionId", entity.CollectionId);
            command.Parameters.AddWithValue("@CollectionDate", entity.CollectionDate);
            command.Parameters.AddWithValue("@OrderId", entity.OrderId);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
    /// <summary>
    /// Deletes a PickUp record from the database using CollectionId.
    /// </summary>

    public void Delete(params object[] keyValues)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPickUp_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CollectionId", keyValues[0]);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
    /// <summary>
    /// Retrieves a PickUp record by ID from the database.
    /// Returns null if no match is found.
    /// </summary>
    public PickUp GetById(params object[] keyValues)
    {
        PickUp pickUp = null;
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPickUp_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CollectionId", keyValues[0]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                pickUp = new PickUp
                    ((int)reader["CollectionId"],
                    (DateTime)reader["CollectionDate"],
                    (int)reader["OrderId"]);
            }
        }
        return pickUp;
    }

    /// <summary>
    /// Retrieves all PickUp records stored in the database.
    /// </summary>

    public IEnumerable<PickUp> GetAll()
    {
        var pickUps = new List<PickUp>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPickUp_GetAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                pickUps.Add(new PickUp
                (
                    (int)reader["CollectionId"],
                    (DateTime)reader["CollectionDate"],
                    (int)reader["OrderId"]
                ));
            }
        }
        return pickUps;
    }


}