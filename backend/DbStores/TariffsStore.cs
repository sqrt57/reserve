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

    public async Task<IReadOnlyCollection<DbTariff>> GetTariffs()
    {
        var query = @"
SELECT * FROM [dbo].[Tariffs]
WHERE [IsActive] = 1
ORDER BY [CreatedDateTime] DESC
";

        using var connection = await _dapperConnections.CreateAsync();
        return (await connection.QueryAsync<DbTariff>(query)).ToList();
    }
}
