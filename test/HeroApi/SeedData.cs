using System;
using Web.Api.Core.Domain.Entities;
using Web.Api.Infrastructure.Data;

namespace Web.Api.IntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(AppDbContext context)
        {
            context.Heroes.Add(new Hero { Id = 1, Name = "Iron Man", Identity = "Tony Stark", Hometown = "L.A.", Age = 40 });
            context.Heroes.Add(new Hero { Id = 2, Name = "Spiderman", Identity = "Peter Parker", Hometown = "N.Y.", Age = 17 });
            context.SaveChanges();
        }
    }
}