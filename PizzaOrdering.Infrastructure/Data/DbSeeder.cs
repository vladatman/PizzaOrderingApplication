using Microsoft.EntityFrameworkCore;
using PizzaOrdering.Domain.Entities;
using PizzaOrdering.Domain.Enums;

namespace PizzaOrdering.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(PizzaOrderingDbContext context)
    {
        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Check if data already exists
        if (await context.Pizzas.AnyAsync() || await context.Toppings.AnyAsync())
        {
            return; // Database has been seeded
        }

        await SeedToppingsAsync(context);
        await SeedPizzasAsync(context);
        await SeedPizzaToppingsAsync(context);
        
        await context.SaveChangesAsync();
    }

    private static async Task SeedToppingsAsync(PizzaOrderingDbContext context)
    {
        var toppings = new List<Topping>
        {
            new Topping
            {
                Name = "Mozzarella Cheese",
                Description = "Fresh mozzarella cheese",
                Price = 1.50m,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            },
            new Topping
            {
                Name = "Pepperoni",
                Description = "Spicy pepperoni slices",
                Price = 2.00m,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            },
            new Topping
            {
                Name = "Mushrooms",
                Description = "Fresh button mushrooms",
                Price = 1.25m,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            },
            new Topping
            {
                Name = "Italian Sausage",
                Description = "Seasoned Italian sausage",
                Price = 2.25m,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            },
            new Topping
            {
                Name = "Bell Peppers",
                Description = "Fresh red and green bell peppers",
                Price = 1.00m,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            },
            new Topping
            {
                Name = "Red Onions",
                Description = "Fresh red onion slices",
                Price = 0.75m,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            },
            new Topping
            {
                Name = "Black Olives",
                Description = "Mediterranean black olives",
                Price = 1.00m,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            },
            new Topping
            {
                Name = "Fresh Basil",
                Description = "Fresh basil leaves",
                Price = 1.00m,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.Toppings.AddRangeAsync(toppings);
    }

    private static async Task SeedPizzasAsync(PizzaOrderingDbContext context)
    {
        var pizzas = new List<Pizza>
        {
            new Pizza
            {
                Name = "Margherita",
                Description = "Classic pizza with tomato sauce, mozzarella, and fresh basil",
                BasePrice = 8.99m, // Base price for Small size
                CreatedAt = DateTime.UtcNow
            },
            new Pizza
            {
                Name = "Pepperoni Supreme",
                Description = "Pepperoni, mozzarella cheese, and tomato sauce",
                BasePrice = 10.99m, // Base price for Small size
                CreatedAt = DateTime.UtcNow
            },
            new Pizza
            {
                Name = "Vegetarian Delight",
                Description = "Mushrooms, bell peppers, red onions, and black olives",
                BasePrice = 11.99m, // Base price for Small size
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.Pizzas.AddRangeAsync(pizzas);
    }

    private static async Task SeedPizzaToppingsAsync(PizzaOrderingDbContext context)
    {
        // This will be called after pizzas and toppings are saved to get their IDs
        await context.SaveChangesAsync();

        var pizzas = await context.Pizzas.ToListAsync();
        var toppings = await context.Toppings.ToListAsync();

        var mozzarella = toppings.First(t => t.Name == "Mozzarella Cheese");
        var pepperoni = toppings.First(t => t.Name == "Pepperoni");
        var mushrooms = toppings.First(t => t.Name == "Mushrooms");
        var basil = toppings.First(t => t.Name == "Fresh Basil");
        var bellPeppers = toppings.First(t => t.Name == "Bell Peppers");
        var redOnions = toppings.First(t => t.Name == "Red Onions");
        var blackOlives = toppings.First(t => t.Name == "Black Olives");

        var pizzaToppings = new List<PizzaTopping>();

        // Margherita pizza default toppings
        var margherita = pizzas.First(p => p.Name == "Margherita");
        pizzaToppings.AddRange(new[]
        {
            new PizzaTopping { PizzaId = margherita.Id, ToppingId = mozzarella.Id, IsDefault = true },
            new PizzaTopping { PizzaId = margherita.Id, ToppingId = basil.Id, IsDefault = true }
        });

        // Pepperoni Supreme default toppings
        var pepperoniSupreme = pizzas.First(p => p.Name == "Pepperoni Supreme");
        pizzaToppings.AddRange(new[]
        {
            new PizzaTopping { PizzaId = pepperoniSupreme.Id, ToppingId = mozzarella.Id, IsDefault = true },
            new PizzaTopping { PizzaId = pepperoniSupreme.Id, ToppingId = pepperoni.Id, IsDefault = true }
        });

        // Vegetarian Delight default toppings
        var vegetarian = pizzas.First(p => p.Name == "Vegetarian Delight");
        pizzaToppings.AddRange(new[]
        {
            new PizzaTopping { PizzaId = vegetarian.Id, ToppingId = mozzarella.Id, IsDefault = true },
            new PizzaTopping { PizzaId = vegetarian.Id, ToppingId = mushrooms.Id, IsDefault = true },
            new PizzaTopping { PizzaId = vegetarian.Id, ToppingId = bellPeppers.Id, IsDefault = true },
            new PizzaTopping { PizzaId = vegetarian.Id, ToppingId = redOnions.Id, IsDefault = true },
            new PizzaTopping { PizzaId = vegetarian.Id, ToppingId = blackOlives.Id, IsDefault = true }
        });

        await context.PizzaToppings.AddRangeAsync(pizzaToppings);
    }
} 