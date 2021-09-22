using Dapper;
using Microsoft.Extensions.Configuration;
using repertoire_webapi.Interfaces;
using repertoire_webapi.Models;
using repertoire_webapi.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace repertoire_webapi.Repositories
{
    public class TuningRepository : BaseRepository, ITuningRepository
    {
        public TuningRepository(IConfiguration configuration) : base(configuration) { }

        public void AddTuning(Tuning tuning)
        {
            string sql = @"INSERT INTO [Tuning] ([Name], [InstrumentId], [String1ToneId],
                            [String2ToneId], [String3ToneId], [String4ToneId], [String5ToneId],
                            [String6ToneId])
                           OUTPUT INSERTED.Id
                           VALUES (@Name, @InstrumentId, @String1ToneId, @String2ToneId, @String3ToneId,
                            @String4ToneId, @String5ToneId, @String6ToneId)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                tuning.Id = db.QuerySingle<int>(sql, tuning);
            }
        }

        public List<Tuning> GetAllTunings()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandText = tuningSql;
                    var reader = cmd.ExecuteReader();
                    var tunings = new List<Tuning>();
                    while (reader.Read())
                    {
                        var tuningId = DbUtils.GetInt(reader, "Id");
                        var existingTuning = tunings.FirstOrDefault(t => t.Id == tuningId);
                        if (existingTuning == null)
                        {
                            existingTuning = NewTuningFromDb(reader);
                            tunings.Add(existingTuning);
                        }
                    }
                    reader.Close();
                    return tunings;
                }
            }
        }

        public Tuning GetTuningById(int tuningId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandText = tuningSql + " WHERE t.[Id] = @TuningId";
                    DbUtils.AddParameter(cmd, "TuningId", tuningId);
                    var reader = cmd.ExecuteReader();
                    Tuning tuning = null;
                    while (reader.Read())
                    {
                        if (tuning == null)
                        {
                            tuning = NewTuningFromDb(reader);
                        }
                    }
                    reader.Close();
                    return tuning;
                }
            }
        }

        private string tuningSql = @"SELECT t.[Id], t.[Name], t.[InstrumentId], 
        t.[String1ToneId], t.[String2ToneId], t.[String3ToneId],
		t.[String4ToneId], t.[String5ToneId], t.[String6ToneId],
        i.[Name] as InstrumentName,
		s1.[Note] as s1note, s1.[Path] as s1path,
		s2.[Note] as s2note, s2.[Path] as s2path,
		s3.[Note] as s3note, s3.[Path] as s3path,
		s4.[Note] as s4note, s4.[Path] as s4path,
		s5.[Note] as s5note, s5.[Path] as s5path,
		s6.[Note] as s6note, s6.[Path] as s6path
	FROM [Tuning] t
    LEFT JOIN [Instrument] i on i.[Id] = t.[InstrumentId]
	LEFT JOIN [Tone] s1 on s1.[Id] = t.[String1ToneId]
	LEFT JOIN [Tone] s2 on s2.[Id] = t.[String2ToneId]
	LEFT JOIN [Tone] s3 on s3.[Id] = t.[String3ToneId]
	LEFT JOIN [Tone] s4 on s4.[Id] = t.[String4ToneId]
	LEFT JOIN [Tone] s5 on s5.[Id] = t.[String5ToneId]
	LEFT JOIN [Tone] s6 on s6.[Id] = t.[String6ToneId]";

        private Tuning NewTuningFromDb(SqlDataReader reader)
        {
            return new Tuning()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                Name = DbUtils.GetString(reader, "Name"),
                InstrumentId = DbUtils.GetInt(reader, "InstrumentId"),
                Instrument = new Instrument()
                {
                    Id = DbUtils.GetInt(reader, "InstrumentId"),
                    Name = DbUtils.GetString(reader, "InstrumentName")
                },
                String1ToneId = DbUtils.GetInt(reader, "String1ToneId"),
                String2ToneId = DbUtils.GetInt(reader, "String2ToneId"),
                String3ToneId = DbUtils.GetInt(reader, "String3ToneId"),
                String4ToneId = DbUtils.GetInt(reader, "String4ToneId"),
                String5ToneId = DbUtils.GetNullableInt(reader, "String5ToneId"),
                String6ToneId = DbUtils.GetNullableInt(reader, "String6ToneId"),
                String1Tone = new Tone() { 
                    Id = DbUtils.GetInt(reader, "String1ToneId"),
                    Note = DbUtils.GetString(reader, "s1note"),
                    Path = DbUtils.GetString(reader, "s1Path")
                },
                String2Tone = new Tone()
                {
                    Id = DbUtils.GetInt(reader, "String2ToneId"),
                    Note = DbUtils.GetString(reader, "s2note"),
                    Path = DbUtils.GetString(reader, "s2Path")
                },
                String3Tone = new Tone()
                {
                    Id = DbUtils.GetInt(reader, "String3ToneId"),
                    Note = DbUtils.GetString(reader, "s3note"),
                    Path = DbUtils.GetString(reader, "s3Path")
                },
                String4Tone = new Tone()
                {
                    Id = DbUtils.GetInt(reader, "String4ToneId"),
                    Note = DbUtils.GetString(reader, "s4note"),
                    Path = DbUtils.GetString(reader, "s4Path")
                },
                String5Tone = new Tone()
                {
                    Id = DbUtils.GetNullableInt(reader, "String5ToneId"),
                    Note = DbUtils.GetString(reader, "s5note"),
                    Path = DbUtils.GetString(reader, "s5Path")
                },
                String6Tone = new Tone()
                {
                    Id = DbUtils.GetNullableInt(reader, "String6ToneId"),
                    Note = DbUtils.GetString(reader, "s6note"),
                    Path = DbUtils.GetString(reader, "s6Path")
                }
            };
        }
    }
}
