﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ProductsCatalog.Contexts;

namespace ProductsCatalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .SeedData()
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

    }
}

