using repertoire_webapi.Models;
using System.Collections.Generic;

namespace repertoire_webapi.Interfaces
{
    public interface IArtistRepository
    {
        void AddArtist(Artist artist);
        List<Artist> GetAllArtists();
        Artist GetArtistById(int id);
    }
}