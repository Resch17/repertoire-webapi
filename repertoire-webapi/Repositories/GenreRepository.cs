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
    public class GenreRepository : BaseRepository, IGenreRepository
    {
        public GenreRepository(IConfiguration configuration) : base(configuration) { }

        public List<Genre> GetAllGenres()
        {
            string sql = "SELECT * FROM [Genre]";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Genre>(sql).ToList();
            }
        }

        public void AddGenre(Genre genre)
        {
            string sql = @"INSERT INTO [Genre] ([Name])
                           OUTPUT INSERTED.Id
                           VALUES (@Name)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                genre.Id = db.QuerySingle<int>(sql, genre);
            }
        }
    }
}
