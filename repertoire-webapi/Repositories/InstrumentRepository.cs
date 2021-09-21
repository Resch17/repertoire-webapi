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
    public class InstrumentRepository : BaseRepository, IInstrumentRepository
    {
        public InstrumentRepository(IConfiguration configuration) : base(configuration) { }
        public List<Instrument> GetAllInstruments()
        {
            string sql = "SELECT * FROM [Instrument]";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Instrument>(sql).ToList();
            }
        }

        public void AddInstrument(Instrument instrument)
        {
            string sql = @"INSERT INTO [Instrument] ([Name])
                           OUTPUT INSERTED.Id
                           VALUES (@Name)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                instrument.Id = db.QuerySingle<int>(sql, instrument);
            }
        }
    }
}
