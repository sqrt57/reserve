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

    public async Task<IReadOnlyCollection<Visitor>> GetOpenVisitors(DateTime? minCloseTime)
    {
        var minCloseTimeFilter = minCloseTime.HasValue ? "OR [CloseDateTime] > @CloseDateTime" : "";
        var query = @$"
SELECT * FROM [dbo].[Visitors]
WHERE [IsActive] = 1 AND ([CloseDateTime] IS NULL OR [Paid] IS NULL {minCloseTimeFilter})
ORDER BY [Id]
";

        var parameters = new DynamicParameters();
        if (minCloseTime.HasValue)
            parameters.Add("CloseDateTime", minCloseTime.Value);

        using var connection = await _dapperConnections.CreateAsync();
        return (await connection.QueryAsync<Visitor>(query, parameters)).ToList();
    }

    public async Task<Visitor?> GetVisitorById(int visitorId)
    {
        var query = @$"
SELECT * FROM [dbo].[Visitors]
WHERE [Id] = @Id
";

        var parameters = new DynamicParameters();
        parameters.Add("Id", visitorId);

        using var connection = await _dapperConnections.CreateAsync();
        return await connection.QueryFirstOrDefaultAsync<Visitor>(query, parameters);
    }

    public async Task<Visitor> CreateVisitor(Visitor visitor)
    {
        var query = @"
INSERT INTO [dbo].[Visitors]
        ([IsActive]
        ,[BadgeNumber]
        ,[Name]
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
        parameters.Add("OpenDateTime", visitor.OpenDateTime);
        parameters.Add("OpenedByUserId", visitor.OpenedByUserId);
        parameters.Add("CloseDateTime", visitor.CloseDateTime);
        parameters.Add("ClosedByUserId", visitor.ClosedByUserId);
        parameters.Add("PaymentAcceptedByUserId", visitor.PaymentAcceptedByUserId);
        parameters.Add("Billed", visitor.Billed);
        parameters.Add("Paid", visitor.Paid);

        using var connection = await _dapperConnections.CreateAsync();
        var id = await connection.ExecuteScalarAsync<int>(query, parameters);
        visitor.Id = id;
        return visitor;
    }

    public async Task<bool> UpdateVisitor(Visitor visitor)
    {
        var query = @"
UPDATE [dbo].[Visitors] SET
    [IsActive] = 1
    ,[BadgeNumber] = @BadgeNumber
    ,[Name] = @Name
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
