
namespace ShoppingCart.DataSourcing;

public interface IMemoryCachingService
{
    void AddShoppingItem(Guid itemId, string userEmail);
    void RemoveShoppingItem(Guid itemId, string userEmail);

    List<Guid>? GetShoppingItems(string userEmail);
}

public class MemoryCachingService : IMemoryCachingService
{
    private Dictionary<string, List<Guid>> _shoppingItems = new();

    public void AddShoppingItem(Guid itemId, string userEmail)
    {
        if (!_shoppingItems.ContainsKey(userEmail))
        {
            _shoppingItems.Add(userEmail, new List<Guid> {itemId});
        }
        else
        {
            _shoppingItems[userEmail].Add(itemId);
        }
    }
    
    public void RemoveShoppingItem(Guid itemId, string userEmail)
    {
        if (!_shoppingItems.ContainsKey(userEmail))
        {
            throw new KeyNotFoundException($"User {userEmail} not found");
        }
        _shoppingItems[userEmail].Remove(itemId);

    }

    public List<Guid>? GetShoppingItems(string userEmail)
    {
        return _shoppingItems.GetValueOrDefault(userEmail);
    }
    
}