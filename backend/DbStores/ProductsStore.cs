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
        var query = @"
SELECT * FROM [dbo].[Products]
WHERE [IsActive] = 1
ORDER BY [Order]
";

        using var connection = await _dapperConnections.CreateAsync();
        return (await connection.QueryAsync<DbProduct>(query)).ToList();
    }

    public async Task<DbProduct?> InsertWithMaxOrder(DbProduct product)
    {
        var query = @"
DECLARE @Order int = (SELECT MAX([Order]) FROM [dbo].[Products]) + 1;
IF @Order IS NULL SET @order = 1;

INSERT INTO [dbo].[Products]
	([IsActive]
	,[CreatedDateTime]
	,[CreatedByUserId]
	,[Order]
	,[Name]
	,[Price]
	,[InStock])
VALUES
	(1
	,@CreatedDateTime
	,@CreatedByUserId
	,@Order
	,@Name
	,@Price
	,@InStock);

DECLARE @Id int = SCOPE_IDENTITY();

SELECT * FROM [dbo].[Products] WHERE [Id] = @Id;
";

        var parameters = new DynamicParameters();
        parameters.Add("CreatedDateTime", product.CreatedDateTime);
        parameters.Add("CreatedByUserId", product.CreatedByUserId);
        parameters.Add("Name", product.Name);
        parameters.Add("Price", product.Price);
        parameters.Add("InStock", product.InStock);

        using var connection = await _dapperConnections.CreateAsync();
        return await connection.QueryFirstOrDefaultAsync<DbProduct>(query, parameters);
    }

    public async Task<DbProduct> UpdateProduct(DbProduct product)
    {
	    var query = @"
UPDATE [dbo].[Products]
SET [IsActive] = 0
WHERE [Id] = @Id;

DECLARE @Order [int];
DEClARE @InStock [bit];

SELECT @Order = [Order], @InStock = [InStock]
FROM [dbo].[Products]
WHERE [Id] = @Id;

INSERT INTO [dbo].[Products]
	([IsActive]
	,[CreatedDateTime]
	,[CreatedByUserId]
	,[Order]
	,[Name]
	,[Price]
	,[InStock])
VALUES
	(1
	,@CreatedDateTime
	,@CreatedByUserId
	,@Order
	,@Name
	,@Price
	,@InStock);

DECLARE @NewId int = SCOPE_IDENTITY();

SELECT * FROM [dbo].[Products] WHERE [Id] = @NewId;
";

	    var parameters = new DynamicParameters();
	    parameters.Add("Id", product.Id);
	    parameters.Add("CreatedDateTime", product.CreatedDateTime);
	    parameters.Add("CreatedByUserId", product.CreatedByUserId);
	    parameters.Add("Name", product.Name);
	    parameters.Add("Price", product.Price);

	    using var connection = await _dapperConnections.CreateAsync();
	    return await connection.QueryFirstOrDefaultAsync<DbProduct>(query, parameters);
    }
}
