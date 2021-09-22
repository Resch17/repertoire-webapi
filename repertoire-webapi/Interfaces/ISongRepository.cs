using repertoire_webapi.Models;
using System.Collections.Generic;

namespace repertoire_webapi.Interfaces
{
    public interface ISongRepository
    {
        void AddSong(Song song);
        List<Song> GetAllSongs();
        List<Song> GetSongsByUser(int userId);
        Song GetSongById(int songId);
        void UpdateSong(Song song);
        void DeleteSong(int songId);
    }
}