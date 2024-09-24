namespace MusikkApi.Models
{
	public interface IArtistRepo
	{ public List<Artist> GetAllArtists();
		public Artist GetArtistById(string id);
		void AddArtist(Artist artist);
		bool DeleteArtist(string id);
	}

}
