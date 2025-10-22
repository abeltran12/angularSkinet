using System.Text.Json;
using angularSkinet.Core.Entities;

namespace angularSkinet.Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.Products.Any())
        {
            var productsData = await File.ReadAllTextAsync("../angularSkinet.Infrastructure/Data/SeedData/products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (products is null) return;

            context.Products.AddRange(products);

            await context.SaveChangesAsync();
        }
    }
}
