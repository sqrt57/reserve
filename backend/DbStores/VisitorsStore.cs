﻿using backend.Models;
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

    public Task<IEnumerable<Visitor>> GetOpenVisitors()
    {
        var query = @"
SELECT * FROM [dbo].[Visitors]
WHERE [IsActive] = 1 AND ([CloseDateTime] IS NULL OR [Payed] IS NULL)
ORDER BY [Id]
";

        using var connection = _dapperConnections.Create();
        return connection.QueryAsync<Visitor>(query);
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
        ,[Payed])
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
        ,@Payed);

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
        parameters.Add("Payed", visitor.Payed);

        using var connection = _dapperConnections.Create();
        var id = await connection.ExecuteScalarAsync<int>(query);
        visitor.Id = id;
        return visitor;
    }

    public async Task<Visitor> UpdateVisitor(Visitor visitor)
    {
        var query = @"
UPDATE [dbo].[Visitors] SET
    [IsActive] = @IsActive
    ,[BadgeNumber] = @BadgeNumber
    ,[Name] = @Name
    ,[OpenDateTime] = @OpenDateTime
    ,[OpenedByUserId] = @OpenedByUserId
    ,[CloseDateTime] = @CloseDateTime
    ,[ClosedByUserId] = @ClosedByUserId
    ,[PaymentAcceptedByUserId] = @PaymentAcceptedByUserId
    ,[Billed] = @Billed
    ,[Payed] = @Payed
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
        parameters.Add("Payed", visitor.Payed);

        using var connection = _dapperConnections.Create();
        var id = await connection.ExecuteScalarAsync<int>(query);
        visitor.Id = id;
        return visitor;
    }
}