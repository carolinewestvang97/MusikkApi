using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MusikkApi.Models;
using MusikkApi.Controllers;
using System.Net;


namespace ArtistsControllerTests
{
	public class ArtistsControllerTests
	{

		private WebApplicationFactory<Program> _factory;
		private readonly IArtistRepo _artistRepo;
		
		public ArtistsControllerTests()
		{
				var artistRepo = new ArtistRepo();
				_factory = new WebApplicationFactory<Program>()
			   .WithWebHostBuilder(builder =>
			   {
				   IServiceCollection services = new ServiceCollection();
				   builder.ConfigureServices(services =>
				   {
					   services.AddSingleton<IArtistRepo>(artistRepo);
				   });
			   });
			var httpClient = _factory.CreateClient();


		}

		[Fact]
		public async Task TestGetArtists_ReturnsANumberOfArtists()
		{
			// Arrange
			var client = _factory.CreateClient();

			// Act
			var response = await client.GetAsync("/artists");
			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadFromJsonAsync<List<Artist>>();

			// Assert
			Assert.NotNull(content);
			Assert.True(content.Count > 0);
			Console.WriteLine("number of artists: " + content.Count);
		}



		[Theory]
		[InlineData("711MCceyCBcFnzjGY4Q7Un", "AC/DC")]
		[InlineData("3fMbdgg4jU18AjLCKBhRSm", "Michael Jackson")]

		public async Task GetArtistDetails(string id, string artistname)
		{
			// Arrange
			var client = _factory.CreateClient();

			// Act
			var response = await client.GetAsync($"/artists/{id}");

			// Assert
			response.EnsureSuccessStatusCode(); 

			var artist = await response.Content.ReadFromJsonAsync<Artist>();
			artist.Should().NotBeNull();
			artist?.ArtistName.Should().Be(artistname);
		}

		[Fact]
		public async Task DeleteArtist_ValidId_ReturnsNoContent()
		{
			var client = _factory.CreateClient();
			var validArtistId = "711MCceyCBcFnzjGY4Q7Un"; 

			var response = await client.DeleteAsync($"/artists/{validArtistId}");

			response.StatusCode.Should().Be(HttpStatusCode.NoContent);

			var getResponse = await client.GetAsync($"/artists/{validArtistId}");
			getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}
		
		[Fact]
		public async Task AddArtist_ValidArtist_ReturnsOk()
		{
			var client = _factory.CreateClient();
			var newArtist = new Artist("4Ui2kfOqGujY81UcPrb5KE", "HAIM");

			var response = await client.PostAsJsonAsync("/artists", newArtist);

			response.StatusCode.Should().Be(HttpStatusCode.OK);

			var getResponse = await client.GetAsync($"/artists/{newArtist.Id}");
			getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

			var addedArtist = await getResponse.Content.ReadFromJsonAsync<Artist>();
			addedArtist.Should().NotBeNull();
			addedArtist?.ArtistName.Should().Be(newArtist.ArtistName);
		}
	}
}
	
