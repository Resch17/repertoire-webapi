using Dapper;
using Microsoft.Extensions.Configuration;
using repertoire_webapi.Interfaces;
using repertoire_webapi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace repertoire_webapi.Repositories
{
    public class NoteRepository : BaseRepository, INoteRepository
    {
        public NoteRepository(IConfiguration configuration) : base(configuration) { }

        public List<Note> GetNotesBySongId(int songId, int userId)
        {
            List<Note> notes = new List<Note>();
            string sql = @"SELECT * FROM [Note] 
                            WHERE [SongId] = @SongId
                            AND [UserId] = @UserId";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                notes = db.Query<Note>(sql, new { SongId = songId, UserId = userId }).ToList();
            }
            return notes;
        }

        public void AddNote(Note note)
        {
            string sql = @"INSERT INTO [Note] ([UserId], [SongId], [Text])
                           OUTPUT INSERTED.Id
                           VALUES (@UserId, @SongId, @Text)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                note.Id = db.QuerySingle<int>(sql, note);
            }
        }
    }
}
