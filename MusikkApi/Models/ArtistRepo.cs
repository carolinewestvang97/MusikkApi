namespace MusikkApi.Models
{
	public class ArtistRepo : IArtistRepo
	{
		public List<Artist> _artists = new List<Artist>() {
			{ new Artist("711MCceyCBcFnzjGY4Q7Un", "AC/DC") },
			{ new Artist("3fMbdgg4jU18AjLCKBhRSm", "Michael Jackson") },
			{ new Artist("1OTNNdgU6qLUTCwvJxcObX", "Knutsen & Ludvigsen") },
			{ new Artist("12Chz98pHFMPJEknJQMWvI", "Muse") },
			{ new Artist("345hz98pHFMPJEknJQMWvI", "Justin Bieber") },
			{ new Artist("4TrraAsitQKl821DQY42cZ", "Sigrid") },
		};
		public List<Artist> GetAllArtists()
		{ return _artists; }

		public Artist? GetArtistById(string id)
		{
			return _artists.FirstOrDefault(artist => artist.Id == id);
		}

		public void AddArtist(Artist artist)
		{
			_artists.Add(artist);
		}

		public bool DeleteArtist(string id)
		{
			var artist = GetArtistById(id);
			if (artist != null)
			{
				_artists.Remove(artist);
				return true;
			}
			return false;
		}
	}

}
