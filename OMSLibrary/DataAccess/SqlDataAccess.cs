using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace OrderManagerLibrary.DataAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfigurationRoot _config;
    // TODO add "new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();" to DI

    public SqlDataAccess(IConfigurationRoot config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(
        string storedProcedure,
        U parameters,
        string connectionId = "DefaultConnection")
    {
        using SqlConnection cnn = new SqlConnection(_config.GetConnectionString(connectionId));
        return await cnn.QueryAsync<T>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(
        string storedProcedure,
        T parameters,
        string connectionId = "DefaultConnection")
    {
        using SqlConnection cnn = new SqlConnection(_config.GetConnectionString(connectionId));
        await cnn.ExecuteAsync(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
    }



}
