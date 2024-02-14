using SoSyaGeApp;
using SoSyaGeAppTest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3Test.Controllers
{
    public class WeatherForecastControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public WeatherForecastControllerTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Test()
        {
            Debug.WriteLine("Test");

            // Arrange
            var client = _factory.CreateClient();

            // Act
            var url = "WeatherForecast";
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(responseString);

            // Act
            url = "WeatherForecast/Get2";
            response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            responseString = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(responseString);


        }
    }
}
