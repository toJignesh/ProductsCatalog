using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ProductsCatalog.Models;
using System.Linq;

namespace ProductsCatalog.Contexts
{
    public static class ContextExtensions
    {

        public static IWebHost SeedData(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                context.SeedDb();
            }
            return host;
        }

        public static void SeedDb(this ApplicationDbContext context)
        {
            if (context.Database.EnsureCreated())
            {
                if (!context.Products.Any())
                {
                    context.Products.Add(new Product()
                    {
                        Name = "iPhone",
                        BriefDescription = "Smart Phone by Apple Inc.",
                        Cost = 999.95M,
                        Price = 1195.99M,
                        Quantity = 1000
                    });

                    context.Products.Add(new Product()
                    {
                        Name = "Galaxy",
                        BriefDescription = "Smart Phone by Samsung Inc.",
                        Cost = 888.95M,
                        Price = 1095.99M,
                        Quantity = 1700
                    });

                    context.Products.Add(new Product()
                    {
                        Name = "Blackberry",
                        BriefDescription = "Smart Phone by Blackberry Inc.",
                        Cost = 777.95M,
                        Price = 995.99M,
                        Quantity = 39
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
