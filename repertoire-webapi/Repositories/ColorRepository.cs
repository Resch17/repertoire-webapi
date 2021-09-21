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
    public class ColorRepository : BaseRepository, IColorRepository
    {
        public ColorRepository(IConfiguration configuration) : base(configuration) { }

        public void AddColor(Color color)
        {
            string sql = @"INSERT INTO [Color] ([Hex])
                           OUTPUT INSERTED.Id
                           VALUES (@Hex)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                color.Id = db.QuerySingle<int>(sql, color);
            }
        }
    }
}
