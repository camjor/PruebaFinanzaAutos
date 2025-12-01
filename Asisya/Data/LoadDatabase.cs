using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Asisya.Models;

namespace Asisya.Data;

public class LoadDatabase
{
    public static async Task InsertarData(AppDbContext context, UserManager<Usuario> usuarioManager)
    {
        // -----------------------------------------
        // USUARIO DE PRUEBA (Identity)
        // -----------------------------------------
        if (!usuarioManager.Users.Any())
        {
            var usuario = new Usuario
            {
                Nombre = "Jorge",
                Apellido = "Fonseca",
                Email = "jorgeesteban013@gmail.com",
                UserName = "Jorge",
                Telefono = "98142545"
            };

            await usuarioManager.CreateAsync(usuario, "Jorge2025*");
        }

        // -----------------------------------------
        // CARGA Northwind
        // -----------------------------------------

        // Asegura migraciones
        await context.Database.EnsureCreatedAsync();

        // SHIPPERS
        if (!context.Shippers!.Any())
        {
            context.Shippers!.AddRange(
                new Shipper { CompanyName = "Speedy Express", Phone = "503-555-9831" },
                new Shipper { CompanyName = "United Package", Phone = "503-555-3199" },
                new Shipper { CompanyName = "Federal Shipping", Phone = "503-555-9931" }
            );
        }

        await context.SaveChangesAsync();


        // CATEGORIES
        if (!context.Categories!.Any())
        {
            context.Categories.AddRange(
                new Category { CategoryName = "SERVIDORES", Description = "Soft drinks, coffees, teas" },
                new Category { CategoryName = "CLOUD", Description = "Sweet and savory sauces" },
                new Category { CategoryName = "Produce", Description = "Dried fruit and bean curd" }
            );
        }

        await context.SaveChangesAsync();


        // SUPPLIERS
        if (!context.Suppliers!.Any())
        {
            context.Suppliers.AddRange(
                new Supplier
                {
                    CompanyName = "Exotic Liquids",
                    ContactName = "Charlotte Cooper",
                    Country = "UK",
                    Phone = "171-555-2222"
                },
                new Supplier
                {
                    CompanyName = "New Orleans Cajun",
                    ContactName = "Shelley Burke",
                    Country = "USA",
                    Phone = "504-555-0191"
                }
            );
        }

        await context.SaveChangesAsync();

        // Get FK needed
        var cat1 = context.Categories!.First().CategoryID;
        var sup1 = context.Suppliers!.First().SupplierID;


        // PRODUCTS
        if (!context.Products!.Any())
        {
            context.Products.AddRange(
                new Product
                {
                    ProductName = "Chai",
                    SupplierID = sup1,
                    CategoryID = cat1,
                    UnitPrice = 18,
                    UnitsInStock = 39,
                    Discontinued = false
                },
                new Product
                {
                    ProductName = "Chang",
                    SupplierID = sup1,
                    CategoryID = cat1,
                    UnitPrice = 19,
                    UnitsInStock = 17,
                    Discontinued = false
                }
            );
        }

        await context.SaveChangesAsync();


        // CUSTOMERS
        if (!context.Customers!.Any())
        {
            context.Customers.AddRange(
                new Customer
                {
                    CustomerID = "ALFKI",
                    CompanyName = "Alfreds Futterkiste",
                    ContactName = "Maria Anders",
                    Country = "Germany",
                    Phone = "030-0074321"
                },
                new Customer
                {
                    CustomerID = "ANATR",
                    CompanyName = "Ana Trujillo Emparedados",
                    ContactName = "Ana Trujillo",
                    Country = "Mexico",
                    Phone = "5-555-4729"
                }
            );
        }

        await context.SaveChangesAsync();


        // EMPLOYEES (self–reference)
        if (!context.Employees!.Any())
        {
            var emp1 = new Employee
            {
                FirstName = "Nancy",
                LastName = "Davolio",
                Title = "Sales Representative",
                Country = "USA"
            };

            var emp2 = new Employee
            {
                FirstName = "Andrew",
                LastName = "Fuller",
                Title = "Vice President",
                Country = "USA",
                Subordinates = new List<Employee>() // Nancy dependerá de él
            };

            // Save Andrew first
            context.Employees.Add(emp2);
            await context.SaveChangesAsync();

            // Nancy reports to Andrew
            emp1.ReportsTo = emp2.EmployeeID;

            context.Employees.Add(emp1);
        }

        await context.SaveChangesAsync();


        // ORDERS
        if (!context.Orders!.Any())
        {
            var customer = context.Customers!.First();
            var employee = context.Employees!.First();
            var shipper = context.Shippers!.First();

            var order = new Order
            {
                CustomerID = customer.CustomerID,
                EmployeeID = employee.EmployeeID,
                ShipVia = shipper.ShipperID,
                OrderDate = DateTime.Now,
                ShipName = "Alfreds Futterkiste",
                ShipCountry = "Germany"
            };

            context.Orders.Add(order);
        }

        await context.SaveChangesAsync();


        // ORDER DETAILS
        if (!context.OrderDetails!.Any())
        {
            var order = context.Orders!.First();
            var product = context.Products!.First();

            context.OrderDetails.Add(
                new OrderDetail
                {
                    OrderID = order.OrderID,
                    ProductID = product.ProductID,
                    UnitPrice = product.UnitPrice ?? 18,
                    Quantity = 10,
                    Discount = 0
                }
            );
        }

        await context.SaveChangesAsync();
    }
}
