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

    public Task<Tariff> GetActualTariff()
    {
        var query = @"
SELECT TOP 1 * FROM [dbo].[Tariffs]
WHERE [IsActual] = 1
ORDER BY [CreatedDateTime] DESC
";

        using var connection = _dapperConnections.Create();
        return connection.QuerySingleOrDefaultAsync<Tariff>(query);
    }
}