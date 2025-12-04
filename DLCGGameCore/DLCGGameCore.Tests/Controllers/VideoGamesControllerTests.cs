using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using DLCGGameCore.Application.DTOs;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DLCGGameCore.Tests.Controllers
{
    public class VideoGamesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public VideoGamesControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetPaged_Returns_OK()
        {
            var client = _factory.CreateClient();
            var resp = await client.GetAsync("/api/videogames?page=1&pageSize=5");
            Assert.Equal(HttpStatusCode.OK, resp.StatusCode);

            var json = await resp.Content.ReadAsStringAsync();
            // parse to PagedResult<VideoGameDto> for basic validation
            dynamic parsed = JsonConvert.DeserializeObject(json)!;
            Assert.NotNull(parsed);
        }
    }
}
