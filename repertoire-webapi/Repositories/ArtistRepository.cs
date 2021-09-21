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
    public class ArtistRepository : BaseRepository, IArtistRepository
    {
        public ArtistRepository(IConfiguration configuration) : base(configuration) { }
        public List<Artist> GetAllArtists()
        {
            string sql = "SELECT * FROM [Artist]";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Artist>(sql).ToList();
            }
        }

        public Artist GetArtistById(int id)
        {
            string sql = "SELECT * FROM [Artist] WHERE [Id] = @Id";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QueryFirstOrDefault<Artist>(sql, new { id });
            }
        }

        public void AddArtist(Artist artist)
        {
            string sql = @"INSERT INTO [Artist] ([Name])
                           OUTPUT INSERTED.Id
                           VALUES (@Name)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                artist.Id = db.QuerySingle<int>(sql, artist);
            }
        }
    }
}
