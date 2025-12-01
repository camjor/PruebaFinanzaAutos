using Xunit;
using Microsoft.EntityFrameworkCore;
using Asisya.Data;
using Asisya.Data.Categories;
using Asisya.Models;

public class CategoryRepositoryTests
{
    private AppDbContext BuildContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: $"db_{Guid.NewGuid()}")
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateCategory_ShouldInsert()
    {
        var context = BuildContext();
        var repo = new CategoryRepository(context);

        var category = new Category { CategoryName = "Beverages" };

        await repo.Create(category);
        await repo.SaveChanges();

        Assert.Equal(1, context.Categories!.Count());
    }

    [Fact]
    public async Task GetAll_ShouldReturnList()
    {
        var context = BuildContext();
        var repo = new CategoryRepository(context);

        await repo.Create(new Category { CategoryName = "Food" });
        await repo.SaveChanges();

        var result = await repo.GetAll();

        Assert.Single(result);
    }
}
