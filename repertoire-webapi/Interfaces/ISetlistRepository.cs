using repertoire_webapi.Models;

namespace repertoire_webapi.Interfaces
{
    public interface ISetlistRepository
    {
        void AddSetlist(Setlist setlist);
    }
}