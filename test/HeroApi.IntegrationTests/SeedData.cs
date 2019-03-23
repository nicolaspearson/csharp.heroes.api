using System;
using HeroApi.Models;

namespace HeroApi.IntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(HeroContext context)
        {
            context.Heroes.Add(new Hero { Id = 1, Name = "Iron Man", Identity = "Tony Stark", Hometown = "L.A.", Age = 40 });
            context.Heroes.Add(new Hero { Id = 2, Name = "Spiderman", Identity = "Peter Parker", Hometown = "N.Y.", Age = 17 });
            context.SaveChanges();
        }
    }
}