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
    public class SongRepository : BaseRepository, ISongRepository
    {
        public SongRepository(IConfiguration configuration) : base(configuration) { }

        public void AddSong(Song song)
        {
            string sql = @"INSERT INTO [Song] ([Title], [ArtistId], [GenreId],
                            [InstrumentId], [UserId], [TuningId], [Url], [Youtube])
                           OUTPUT INSERTED.Id
                           VALUES (@Title, @ArtistId, @GenreId, @InstrumentId, @UserId,
                            @TuningId, @Url, @Youtube)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                song.Id = db.QuerySingle<int>(sql, song);
            }
        }

        public void UpdateSong(Song song)
        {
            string sql = @"UPDATE [Song]
                            SET [Title] = @Title,
                                [ArtistId] = @ArtistId,
                                [GenreId] = @GenreId,
                                [InstrumentId] = @InstrumentId,
                                [TuningId] = @TuningId,
                                [Url] = @Url,
                                [Youtube] = @Youtube
                            WHERE [Id] = @Id";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(sql, song);
            }
        }
        
        public void DeleteSong(int songId)
        {
            string sql = @"DELETE [Song] WHERE [Id] = @Id";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(sql, new { Id = songId });
            }
        }

        public List<Song> GetAllSongs(int userId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandText = songsSql + " WHERE s.[UserId] = @UserId";
                    DbUtils.AddParameter(cmd, "UserId", userId);
                    var reader = cmd.ExecuteReader();
                    var songs = new List<Song>();
                    while (reader.Read())
                    {
                        songs.Add(NewSongFromDb(reader));
                    }
                    reader.Close();
                    return songs;
                }
            }
        }

        public Song GetSongById(int songId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandText = songsSql + " WHERE s.[Id] = @SongId";
                    DbUtils.AddParameter(cmd, "SongId", songId);
                    var reader = cmd.ExecuteReader();
                    Song song = null;
                    while (reader.Read())
                    {
                        if (song == null)
                        {
                            song = NewSongFromDb(reader);
                        }
                    }
                    reader.Close();
                    return song;
                }
            }
        }

        private string songsSql = @"SELECT s.[Id], s.[Title], s.[ArtistId], s.[GenreId], 
                                        s.[InstrumentId], s.[UserId], 
	                                    s.[TuningId], s.[Url], s.[Youtube], 
                                        a.[Name] as ArtistName, 
                                        g.[Name] as GenreName,
	                                    i.[Name] as InstrumentName, 
                                        u.[Username], u.[Email], u.[ThemeId], 
                                        t.[Name] as TuningName
	                               FROM [Song] s
	                                    LEFT JOIN [Artist] a on a.[Id] = s.[ArtistId]
	                                    LEFT JOIN [Genre] g on g.[Id] = s.[GenreId]
	                                    LEFT JOIN [User] u on u.[Id] = s.[UserId]
	                                    LEFT JOIN [Instrument] i on i.[Id] = s.[InstrumentId]
	                                    LEFT JOIN [Tuning] t on t.[Id] = s.[TuningId]";
        private Song NewSongFromDb(SqlDataReader reader)
        {
            return new Song()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                Title = DbUtils.GetString(reader, "Title"),
                ArtistId = DbUtils.GetInt(reader, "ArtistId"),
                GenreId = DbUtils.GetInt(reader, "GenreId"),
                InstrumentId = DbUtils.GetInt(reader, "InstrumentId"),
                UserId = DbUtils.GetInt(reader, "UserId"),
                TuningId = DbUtils.GetInt(reader, "TuningId"),
                Url = DbUtils.GetString(reader, "Url"),
                Youtube = DbUtils.GetString(reader, "Youtube"),
                Artist = new Artist()
                {
                    Id = DbUtils.GetInt(reader, "ArtistId"),
                    Name = DbUtils.GetString(reader, "ArtistName")
                },
                Genre = new Genre()
                {
                    Id = DbUtils.GetInt(reader, "GenreId"),
                    Name = DbUtils.GetString(reader, "GenreName")
                },
                Instrument = new Instrument()
                {
                    Id = DbUtils.GetInt(reader, "InstrumentId"),
                    Name = DbUtils.GetString(reader, "InstrumentName")
                },
                User = new User()
                {
                    Id = DbUtils.GetInt(reader, "UserId"),
                    Username = DbUtils.GetString(reader, "Username"),
                    Email = DbUtils.GetString(reader, "Email"),
                    ThemeId = DbUtils.GetInt(reader, "ThemeId"),
                },
                Tuning = new Tuning()
                {
                    Id = DbUtils.GetInt(reader, "TuningId"),
                    Name = DbUtils.GetString(reader, "TuningName")
                }
            };
        }
    }
}
