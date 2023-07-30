using System.Data;
using backend.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;

namespace backend.DbStores;

public class TariffsStore
{
    private readonly DapperConnections _dapperConnections;

    public TariffsStore(DapperConnections dapperConnections)
    {
        _dapperConnections = dapperConnections;
    }

    public async Task<Tariff> GetTariff()
    {
        var query = @"
SELECT TOP 1 * FROM [dbo].[Tariffs]
WHERE [IsActive] = 1
ORDER BY [CreatedDateTime] DESC
";

        using var connection = await _dapperConnections.CreateAsync();
        return await connection.QuerySingleOrDefaultAsync<Tariff>(query);
    }
}
