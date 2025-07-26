using Microsoft.EntityFrameworkCore;
using PizzaOrdering.Domain.Entities;

namespace PizzaOrdering.Infrastructure.Data;

public class PizzaOrderingDbContext : DbContext
{
    public PizzaOrderingDbContext(DbContextOptions<PizzaOrderingDbContext> options) : base(options)
    {
    }

    public DbSet<Pizza> Pizzas { get; set; }
    public DbSet<Topping> Toppings { get; set; }
    public DbSet<PizzaTopping> PizzaToppings { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderItemTopping> OrderItemToppings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Pizza entity
        modelBuilder.Entity<Pizza>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Description).HasMaxLength(500);
            entity.Property(p => p.BasePrice)
                .HasConversion(
                    v => (int)(v * 100),  // Convert decimal to cents for storage
                    v => v / 100m);       // Convert cents back to decimal
            entity.Property(p => p.CreatedAt).IsRequired();
        });

        // Configure Topping entity
        modelBuilder.Entity<Topping>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
            entity.Property(t => t.Description).HasMaxLength(300);
            entity.Property(t => t.Price)
                .HasConversion(
                    v => (int)(v * 100),
                    v => v / 100m);
            entity.Property(t => t.CreatedAt).IsRequired();
        });

        // Configure PizzaTopping junction entity
        modelBuilder.Entity<PizzaTopping>(entity =>
        {
            entity.HasKey(pt => new { pt.PizzaId, pt.ToppingId });
            
            entity.HasOne(pt => pt.Pizza)
                .WithMany(p => p.DefaultToppings)
                .HasForeignKey(pt => pt.PizzaId);
                
            entity.HasOne(pt => pt.Topping)
                .WithMany(t => t.PizzaToppings)
                .HasForeignKey(pt => pt.ToppingId);
        });

        // Configure Order entity
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.CustomerName).IsRequired().HasMaxLength(100);
            entity.Property(o => o.CustomerPhone).IsRequired().HasMaxLength(20);
            entity.Property(o => o.TotalPrice)
                .HasConversion(
                    v => (int)(v * 100),
                    v => v / 100m);
            entity.Property(o => o.CreatedAt).IsRequired();
        });

        // Configure OrderItem entity
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(oi => oi.Id);
            entity.Property(oi => oi.Size).IsRequired(); // Size is stored as integer enum
            entity.Property(oi => oi.UnitPrice)
                .HasConversion(
                    v => (int)(v * 100),
                    v => v / 100m);
            entity.Property(oi => oi.TotalPrice)
                .HasConversion(
                    v => (int)(v * 100),
                    v => v / 100m);
                    
            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId);
                
            entity.HasOne(oi => oi.Pizza)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.PizzaId);
        });

        // Configure OrderItemTopping junction entity
        modelBuilder.Entity<OrderItemTopping>(entity =>
        {
            entity.HasKey(oit => new { oit.OrderItemId, oit.ToppingId });
            
            entity.HasOne(oit => oit.OrderItem)
                .WithMany(oi => oi.ExtraToppings)
                .HasForeignKey(oit => oit.OrderItemId);
                
            entity.HasOne(oit => oit.Topping)
                .WithMany(t => t.OrderItemToppings)
                .HasForeignKey(oit => oit.ToppingId);
        });
    }
} 