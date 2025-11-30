namespace ShoppingCart.Models;

public class ShoppingItem
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    public double Price { get; set; }
    public Guid ItemId { get; set; }
}