using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using HeroApi.Models;
using Xunit;

namespace HeroApi.IntegrationTests.Controllers
{
    public class HeroControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public HeroControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetHeroes()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/hero");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var heroes = JsonConvert.DeserializeObject<IEnumerable<Hero>>(stringResponse);
            Assert.Contains(heroes, h => h.Name == "Iron Man");
            Assert.Contains(heroes, h => h.Name == "Spiderman");
        }


        [Fact]
        public async Task CanGetHeroById()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/hero/1");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var hero = JsonConvert.DeserializeObject<Hero>(stringResponse);
            Assert.Equal(1, hero.Id);
            Assert.Equal("Iron Man", hero.Name);
        }
    }
}