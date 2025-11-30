using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DataSourcing;

namespace ShoppingCart.Interface.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class CartController :ControllerBase
{
    private ILogger<CartController> _logger;
    private  IMemoryCachingService _memoryCachingService;
    private IRepository _repository;

    public CartController(ILogger<CartController> logger, IMemoryCachingService memoryCachingService, IRepository repository)
    {
        _logger = logger;
        _memoryCachingService = memoryCachingService;
        _repository = repository;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts()
    {
        var products = _repository.GetShoppingItemsAsync().Result;
        return Ok(products);
    }

    [HttpPost("/AddItem")]
    public async Task<IActionResult> AddItemToCart([FromQuery] Guid itemId, [FromQuery] string userEmail)
    {
        _memoryCachingService.AddShoppingItem(itemId, userEmail);
        return Ok();
    }

    [HttpDelete("/RemoveItem")]
    public async Task<IActionResult> RemoveItemFromCart([FromQuery] Guid itemId, [FromQuery] string userEmail)
    {
        _memoryCachingService.RemoveShoppingItem(itemId, userEmail);
        return Ok();
    }

    [HttpGet("/cart")]
    public async Task<IActionResult> GetCart([FromQuery] string userEmail)
    {
        var userShoppingItems = _memoryCachingService.GetShoppingItems(userEmail);
        if (userShoppingItems == null)
        {
            return NotFound();
        }

        var shoppingItems = await _repository.GetShoppingItemsAsync();

        var filteredItems = shoppingItems
            .Where(x => userShoppingItems.Contains(x.ItemId))
            .ToList();
        return Ok(filteredItems);
    }

    
}