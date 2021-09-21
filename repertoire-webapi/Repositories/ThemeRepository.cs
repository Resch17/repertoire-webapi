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
    }
}
