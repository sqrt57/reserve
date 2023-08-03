using System.Data;
using backend.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;

namespace backend.DbStores;

public class ProductsStore
{
    private readonly DapperConnections _dapperConnections;

    public ProductsStore(DapperConnections dapperConnections)
    {
        _dapperConnections = dapperConnections;
    }

    public async Task<IReadOnlyCollection<DbProduct>> GetAllProducts()
    {
        var query = @$"
SELECT * FROM [dbo].[Products]
WHERE [IsActive] = 1
ORDER BY [Order]
";

        using var connection = await _dapperConnections.CreateAsync();
        return (await connection.QueryAsync<DbProduct>(query)).ToList();
    }
}
