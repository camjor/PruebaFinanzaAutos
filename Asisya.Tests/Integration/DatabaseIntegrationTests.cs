using Xunit;
using Testcontainers.MsSql;
using Microsoft.EntityFrameworkCore;
using Asisya.Data;
using Asisya.Data.Categories;
using Asisya.Models;

public class DatabaseIntegrationTests : IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer;

    public DatabaseIntegrationTests()
    {
        _dbContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("Str0ng_Password123")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task InsertCategory_RealDatabase()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(_dbContainer.GetConnectionString())
            .Options;

        using var context = new AppDbContext(options);
        await context.Database.EnsureCreatedAsync();

        var repo = new CategoryRepository(context);

        await repo.Create(new Category { CategoryName = "IntegrationTest" });
        await repo.SaveChanges();

        Assert.Equal(1, context.Categories!.Count());
    }
}
