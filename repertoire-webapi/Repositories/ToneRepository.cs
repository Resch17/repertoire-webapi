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
    public class ToneRepository : BaseRepository, IToneRepository
    {
        public ToneRepository(IConfiguration configuration) : base(configuration) { }

        public List<Tone> GetAllTones()
        {
            string sql = "SELECT * FROM [Tone]";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Tone>(sql).ToList();
            }
        }

        public void AddTone(Tone tone)
        {
            string sql = @"INSERT INTO [Tone] ([Note], [Path])
                           OUTPUT INSERTED.Id
                           VALUES (@Note, @Path)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                tone.Id = db.QuerySingle<int>(sql, tone);
            }
        }
    }
}
