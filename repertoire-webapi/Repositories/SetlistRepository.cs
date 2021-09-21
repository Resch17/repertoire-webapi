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
    public class SetlistRepository : BaseRepository, ISetlistRepository
    {
        public SetlistRepository(IConfiguration configuration) : base(configuration) { }


        public void AddSetlist(Setlist setlist)
        {
            string sql = @"INSERT INTO [Setlist] ([UserId], [SongId], [Ordinal]
                            OUTPUT INSERTED.Id
                            VALUES (@UserId, @SongId, @Ordinal)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                setlist.Id = db.QuerySingle<int>(sql, setlist);
            }
        }
    }
}
