﻿using System.Data;
using backend.Exceptions;
using Microsoft.Data.SqlClient;

namespace backend.DbStores;

public class DapperConnections
{
    private readonly string _connectionString;

    public DapperConnections(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new ConfigurationMissingException(
                                "Missing DefaultConnection connection string configuration");
    }

    public async Task<IDbConnection> CreateAsync()
    {
        var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}
