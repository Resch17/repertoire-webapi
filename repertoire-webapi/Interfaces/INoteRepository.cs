using repertoire_webapi.Models;
using System.Collections.Generic;

namespace repertoire_webapi.Interfaces
{
    public interface INoteRepository
    {
        void AddNote(Note note);
        List<Note> GetNotesBySongId(int songId, int userId);
    }
}