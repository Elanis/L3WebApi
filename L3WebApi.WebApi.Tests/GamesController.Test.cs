using FluentAssertions;
using L3WebApi.Common.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Text.Json;

namespace L3WebApi.WebApi.Tests {
	public class GamesControllerTest {
		public HttpClient client { get; }
		public TestServer server { get; }

		private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions() {
			PropertyNameCaseInsensitive = true,
		};

		public GamesControllerTest() {
			var webApplicationFactory = new WebApplicationFactory<WebAPI.Program>();
			client = webApplicationFactory.CreateClient();
		}

		[Fact]
		public async void ShouldGet200_GET_AllGames() {
			var response = await client.GetAsync("/api/Games/");

			response.StatusCode.Should().Be(HttpStatusCode.OK);

			var data = JsonSerializer.Deserialize<IEnumerable<GameDto>>(
				await response.Content.ReadAsStringAsync(),
				jsonOptions
			);

			data.Should().NotBeEmpty();
		}

		[Theory]
		[InlineData(1, HttpStatusCode.OK)]
		[InlineData(2, HttpStatusCode.NotFound)]
		public async void ShouldGetReleventHttpCode_GET_By_Id(int id, HttpStatusCode code) {
			var response = await client.GetAsync($"/api/Games/{id}");

			response.StatusCode.Should().Be(code);

			if (code == HttpStatusCode.OK) {
				var data = JsonSerializer.Deserialize<GameDto>(
					await response.Content.ReadAsStringAsync(),
					jsonOptions
				);

				data.Should().NotBeNull();
			}
		}
	}
}
