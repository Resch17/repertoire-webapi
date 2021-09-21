using repertoire_webapi.Models;
using System.Collections.Generic;

namespace repertoire_webapi.Interfaces
{
    public interface IGenreRepository
    {
        void AddGenre(Genre genre);
        List<Genre> GetAllGenres();
    }
}