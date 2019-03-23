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

        [Fact]
        public async Task CanCreateHero()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsJsonAsync("/hero", new Hero { Id = 3, Name = "Captain America", Identity = "Steve Rogers", Hometown = "Queens", Age = 110 });

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var hero = JsonConvert.DeserializeObject<Hero>(stringResponse);
            Assert.Equal(3, hero.Id);
            Assert.Equal("Captain America", hero.Name);
        }

        [Fact]
        public async Task CanUpdateHero()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsJsonAsync("/hero/2", new Hero { Id = 2, Name = "Spiderman", Identity = "Peter Parker", Hometown = "Brooklyn", Age = 18 });

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            var httpGetResponse = await _client.GetAsync("/hero/2");

            // Must be successful.
            httpGetResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpGetResponse.Content.ReadAsStringAsync();
            var hero = JsonConvert.DeserializeObject<Hero>(stringResponse);
            Assert.Equal(2, hero.Id);
            Assert.Equal("Spiderman", hero.Name);
            Assert.Equal("Brooklyn", hero.Hometown);
            Assert.Equal(18, hero.Age);
        }

        [Fact]
        public async Task CanDeleteHero()
        {
            // Create a hero
            await this.CanCreateHero();

            var httpGetResponse = await _client.GetAsync("/hero/3");

            // Must be successful.
            httpGetResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpGetResponse.Content.ReadAsStringAsync();
            var hero = JsonConvert.DeserializeObject<Hero>(stringResponse);
            Assert.Equal(3, hero.Id);
            Assert.Equal("Captain America", hero.Name);
            Assert.Equal("Queens", hero.Hometown);
            Assert.Equal(110, hero.Age);

            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync("/hero/3");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Must be not found.
            httpGetResponse = await _client.GetAsync("/hero/3");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, httpGetResponse.StatusCode);
        }
    }
}