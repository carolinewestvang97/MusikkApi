using Microsoft.AspNetCore.Mvc;
using MusikkApi.Models;
using System.Collections.Generic;

namespace MusikkApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ArtistsController : ControllerBase
	{

		private readonly IArtistRepo _artistRepo;

		public ArtistsController(IArtistRepo artistRepo)
		{
			_artistRepo = artistRepo;
		}

		[HttpGet]
		public ActionResult<List<Artist>> GetAllArtists()
		{
			var artists = _artistRepo.GetAllArtists();
			return Ok(artists);
		}

		[HttpGet("{id}")]
		public ActionResult<Artist> GetArtistById(string id)
		{
			var artist = _artistRepo.GetArtistById(id);
			if (artist == null)
			{
				return NotFound();
			}

			return Ok(artist);
		}
		
		[HttpPost]
		public ActionResult AddArtist([FromBody] Artist artist)
		{
			_artistRepo.AddArtist(artist);
			return Ok(artist);
		}

		[HttpDelete("{id}")]
		public ActionResult DeleteArtist(string id)
		{
			bool deleted = _artistRepo.DeleteArtist(id);
			if (!deleted)
			{
				return NotFound();
			}

			return NoContent();
		}



	}

}
	
