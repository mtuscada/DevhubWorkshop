using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ShoppingCart.Models;
using Dapper;

namespace ShoppingCart.DataSourcing;

public interface IRepository 
{
    Task<List<ShoppingItem>> GetShoppingItemsAsync();
}
public class Repository : IRepository
{
    private readonly Func<SqlConnection> _getSqlConnection;
    
    //leave password/ connection string out here

    public Repository(IOptions<ConnectionOptions> connectionOptions)
    {
        _getSqlConnection = () =>  new SqlConnection(connectionOptions.Value.DbConnectionString);
    }

    public async Task<List<ShoppingItem>> GetShoppingItemsAsync()
    {
        await using var connection = _getSqlConnection();
        var shoppingItems = await connection.QueryAsync<ShoppingItem>("SELECT * FROM dbo.ShoppingItems");
        return shoppingItems.ToList();
    }
    
    
    
}