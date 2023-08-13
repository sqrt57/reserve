using System.Data;
using backend.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;

namespace backend.DbStores;

public class VisitorsStore
{
    private readonly DapperConnections _dapperConnections;

    public VisitorsStore(DapperConnections dapperConnections)
    {
        _dapperConnections = dapperConnections;
    }

    public async Task<IReadOnlyCollection<(DbVisitor Visitor, DbTariff Tariff)>> GetOpenVisitors(DateTime? minCloseTime)
    {
        var minCloseTimeFilter = minCloseTime.HasValue ? "OR V.[CloseDateTime] > @CloseDateTime" : "";
        var query = @$"
SELECT V.*, T.* FROM [dbo].[Visitors] V
INNER JOIN [dbo].[Tariffs] T
ON V.[TariffId] = t.[Id]
WHERE V.[IsActive] = 1 AND (V.[CloseDateTime] IS NULL OR V.[Paid] IS NULL {minCloseTimeFilter})
ORDER BY V.[OpenDateTime]
";

        var parameters = new DynamicParameters();
        if (minCloseTime.HasValue)
            parameters.Add("CloseDateTime", minCloseTime.Value);

        using var connection = await _dapperConnections.CreateAsync();
        return (await connection.QueryAsync<DbVisitor, DbTariff, (DbVisitor, DbTariff)>(
                query,
                (visitor, tariff) => (visitor, tariff),
                param: parameters,
                splitOn: "Id"))
            .ToList();
    }

    public async Task<(DbVisitor? Visitor, DbTariff? Tariff)> GetVisitorById(int visitorId)
    {
        var query = @$"
SELECT V.*, T.* FROM [dbo].[Visitors] V
INNER JOIN [dbo].[Tariffs] T
ON V.[TariffId] = t.[Id]
WHERE V.[Id] = @Id
";

        var parameters = new DynamicParameters();
        parameters.Add("Id", visitorId);

        using var connection = await _dapperConnections.CreateAsync();

        await connection.QueryFirstOrDefaultAsync<DbVisitor>(query, parameters);
        var result = await connection.QueryAsync<DbVisitor, DbTariff, (DbVisitor, DbTariff)>(
            query,
            (visitor, tariff) => (visitor, tariff),
            param: parameters,
            splitOn: "Id");
        return result.FirstOrDefault();
    }

    public async Task<int> CreateVisitor(DbVisitor visitor)
    {
        var query = @"
INSERT INTO [dbo].[Visitors]
        ([IsActive]
        ,[BadgeNumber]
        ,[Name]
        ,[TariffId]
        ,[OpenDateTime]
        ,[OpenedByUserId]
        ,[CloseDateTime]
        ,[ClosedByUserId]
        ,[PaymentAcceptedByUserId]
        ,[Billed]
        ,[Paid])
    VALUES
        (1
        ,@BadgeNumber
        ,@Name
        ,@TariffId
        ,@OpenDateTime
        ,@OpenedByUserId
        ,@CloseDateTime
        ,@ClosedByUserId
        ,@PaymentAcceptedByUserId
        ,@Billed
        ,@Paid);

SELECT CAST(SCOPE_IDENTITY() as int);
";

        var parameters = new DynamicParameters();
        parameters.Add("BadgeNumber", visitor.BadgeNumber);
        parameters.Add("Name", visitor.Name);
        parameters.Add("TariffId", visitor.TariffId);
        parameters.Add("OpenDateTime", visitor.OpenDateTime);
        parameters.Add("OpenedByUserId", visitor.OpenedByUserId);
        parameters.Add("CloseDateTime", visitor.CloseDateTime);
        parameters.Add("ClosedByUserId", visitor.ClosedByUserId);
        parameters.Add("PaymentAcceptedByUserId", visitor.PaymentAcceptedByUserId);
        parameters.Add("Billed", visitor.Billed);
        parameters.Add("Paid", visitor.Paid);

        using var connection = await _dapperConnections.CreateAsync();
        var id = await connection.ExecuteScalarAsync<int>(query, parameters);
        return id;
    }

    public async Task<bool> UpdateVisitor(DbVisitor visitor)
    {
        var query = @"
UPDATE [dbo].[Visitors] SET
    [IsActive] = 1
    ,[BadgeNumber] = @BadgeNumber
    ,[Name] = @Name
    ,[TariffId] = @TariffId
    ,[OpenDateTime] = @OpenDateTime
    ,[OpenedByUserId] = @OpenedByUserId
    ,[CloseDateTime] = @CloseDateTime
    ,[ClosedByUserId] = @ClosedByUserId
    ,[PaymentAcceptedByUserId] = @PaymentAcceptedByUserId
    ,[Billed] = @Billed
    ,[Paid] = @Paid
WHERE [Id] = @Id
";

        var parameters = new DynamicParameters();
        parameters.Add("Id", visitor.Id);
        parameters.Add("BadgeNumber", visitor.BadgeNumber);
        parameters.Add("Name", visitor.Name);
        parameters.Add("TariffId", visitor.TariffId);
        parameters.Add("OpenDateTime", visitor.OpenDateTime);
        parameters.Add("OpenedByUserId", visitor.OpenedByUserId);
        parameters.Add("CloseDateTime", visitor.CloseDateTime);
        parameters.Add("ClosedByUserId", visitor.ClosedByUserId);
        parameters.Add("PaymentAcceptedByUserId", visitor.PaymentAcceptedByUserId);
        parameters.Add("Billed", visitor.Billed);
        parameters.Add("Paid", visitor.Paid);

        using var connection = await _dapperConnections.CreateAsync();
        var updatedRows = await connection.ExecuteAsync(query, parameters);
        return updatedRows > 0;
    }
}
