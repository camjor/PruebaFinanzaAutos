using Xunit;
using Microsoft.EntityFrameworkCore;
using Asisya.Data;
using Asisya.Data.Orders;
using Asisya.Models;

public class OrderRepositoryTests
{
    private AppDbContext BuildContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"db_{Guid.NewGuid()}")
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateOrder_ShouldInsert()
    {
        var context = BuildContext();
        var repo = new OrderRepository(context);

        var order = new Order { CustomerID = "ALFKI", ShipName = "Test" };

        await repo.Create(order);
        await repo.SaveChanges();

        Assert.Equal(1, context.Orders!.Count());
    }
}
