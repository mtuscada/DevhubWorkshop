using FluentAssertions;
using ShoppingCart.DataSourcing;
using Xunit;

namespace ShoppingCart.Tests.UnitTests;

public class CachingTests
{
    [Fact]
    public void AddShoppingItem_ThenGetShoppingItems_ReturnsItemForUser()
    {
        // Arrange
        var service = new MemoryCachingService();
        var userEmail = "user@example.com";
        var itemId = Guid.NewGuid();

        // Act
        service.AddShoppingItem(itemId, userEmail);
        var result = service.GetShoppingItems(userEmail);
         
        result.Should().NotBeNull();
        result.Count.Should().Be(1);
        result.Should().Contain(itemId);
    }
}