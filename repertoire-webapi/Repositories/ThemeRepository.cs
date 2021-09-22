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
    public class ThemeRepository : BaseRepository, IThemeRepository
    {
        public ThemeRepository(IConfiguration configuration) : base(configuration) { }

        public void AddTheme(Theme theme)
        {
            string sql = @"INSERT INTO [Theme] ([Name], [BackgroundColorId], [AccentTextColorId],
                            [PrimaryTextColorId], [SecondaryBackgroundColorId])
                           OUTPUT INSERTED.Id
                           VALUES (@Name, @BackgroundColorId, @AccentTextColorId,
                             @PrimaryTextColorId, @SecondaryBackgroundColorId)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                theme.Id = db.QuerySingle<int>(sql, theme);
            }
        }

        public List<Theme> GetAllThemes()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandText = themeSql;
                    var reader = cmd.ExecuteReader();
                    var themes = new List<Theme>();
                    while (reader.Read())
                    {
                        var themeId = DbUtils.GetInt(reader, "Id");
                        var existingTheme = themes.FirstOrDefault(t => t.Id == themeId);
                        if (existingTheme == null)
                        {
                            existingTheme = NewThemeFromDb(reader);
                            themes.Add(existingTheme);
                        }
                    }
                    reader.Close();
                    return themes;
                }
            }
        }

        private string themeSql = @"SELECT t.[Id], t.[Name], t.[BackgroundColorId], t.[SecondaryBackgroundColorId], t.[AccentTextColorId], t.[PrimaryTextColorId],
	c1.[Hex] as c1hex, c2.[Hex] as c2hex, c3.[Hex] as c3hex, c4.[Hex] as c4hex
	FROM [Theme] t
	LEFT JOIN [Color] c1 on c1.[Id] = t.[BackgroundColorId]
	LEFT JOIN [Color] c2 on c2.[Id] = t.[SecondaryBackgroundColorId]
	LEFT JOIN [Color] c3 on c3.[Id] = t.[AccentTextColorId]
	LEFT JOIN [Color] c4 on c4.[Id] = t.[PrimaryTextColorId]";

        private Theme NewThemeFromDb(SqlDataReader reader)
        {
            return new Theme()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                Name = DbUtils.GetString(reader, "Name"),
                BackgroundColorId = DbUtils.GetInt(reader, "BackgroundColorId"),
                SecondaryBackgroundColorId = DbUtils.GetInt(reader, "SecondaryBackgroundColorId"),
                AccentTextColorId = DbUtils.GetInt(reader, "AccentTextColorId"),
                PrimaryTextColorId = DbUtils.GetInt(reader, "PrimaryTextColorId"),
                BackgroundColor = new Color()
                {
                    Id = DbUtils.GetInt(reader, "BackgroundColorId"),
                    Hex = DbUtils.GetString(reader, "c1hex")
                },
                SecondaryBackgroundColor = new Color()
                {
                    Id = DbUtils.GetInt(reader, "SecondaryBackgroundColorId"),
                    Hex = DbUtils.GetString(reader, "c2hex")
                },
                AccentTextColor = new Color()
                {
                    Id = DbUtils.GetInt(reader, "AccentTextColorId"),
                    Hex = DbUtils.GetString(reader, "c3hex")
                },
                PrimaryTextColor = new Color()
                {
                    Id = DbUtils.GetInt(reader, "PrimaryTextColorId"),
                    Hex = DbUtils.GetString(reader, "c4hex")
                }
            };
        }
    }
}
