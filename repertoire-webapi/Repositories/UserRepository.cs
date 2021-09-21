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
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public User GetUser(int userId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandText = userSql + " WHERE u.[Id] = @UserId";
                    DbUtils.AddParameter(cmd, "UserId", userId);
                    var reader = cmd.ExecuteReader();
                    User user = null;
                    while (reader.Read())
                    {
                        if (user == null)
                        {
                            user = NewUserFromDb(reader);
                        }
                    }
                    reader.Close();
                    return user;
                }
            }
        }

        public void AddUser(User user)
        {
            string sql = @"INSERT INTO [User] ([Username], [Email], [ThemeId])
                           OUTPUT INSERTED.Id
                           VALUES (@Username, @Email, @ThemeId)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                user.Id = db.QuerySingle<int>(sql, user);
            }
        }

        private string userSql = @"SELECT u.[Id], u.[Username], u.[Email], u.[ThemeId],
		t.[Name] as ThemeName, t.[BackgroundColorId], t.[SecondaryBackgroundColorId],
		t.[AccentTextColorId], t.[PrimaryTextColorId],
		c1.[Hex] as BackgroundColorHex,
		c2.[Hex] as SecondaryBackgroundColorHex,
		c3.[Hex] as AccentTextColorHex,
		c4.[Hex] as PrimaryTextColorHex
	FROM [User] u
	LEFT JOIN [Theme] t on t.[Id] = u.[ThemeId]
	LEFT JOIN [Color] c1 on t.[BackgroundColorId] = c1.[Id]
	LEFT JOIN [Color] c2 on t.[SecondaryBackgroundColorId] = c2.[Id]
	LEFT JOIN [Color] c3 on t.[AccentTextColorId] = c3.[Id]
	LEFT JOIN [Color] c4 on t.[PrimaryTextColorId] = c4.[Id]";

        private User NewUserFromDb(SqlDataReader reader)
        {
            return new User()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                Username = DbUtils.GetString(reader, "Username"),
                Email = DbUtils.GetString(reader, "Email"),
                ThemeId = DbUtils.GetInt(reader, "ThemeId"),
                Theme = new Theme()
                {
                    Id = DbUtils.GetInt(reader, "ThemeId"),
                    Name = DbUtils.GetString(reader, "ThemeName"),
                    BackgroundColorId = DbUtils.GetInt(reader, "BackgroundColorId"),
                    SecondaryBackgroundColorId = DbUtils.GetInt(reader, "SecondaryBackgroundColorId"),
                    AccentTextColorId = DbUtils.GetInt(reader, "AccentTextColorId"),
                    PrimaryTextColorId = DbUtils.GetInt(reader, "PrimaryTextColorId"),
                    BackgroundColor = new Color()
                    {
                        Id = DbUtils.GetInt(reader, "BackgroundColorId"),
                        Hex = DbUtils.GetString(reader, "BackgroundColorHex")
                    },
                    SecondaryBackgroundColor = new Color()
                    {
                        Id = DbUtils.GetInt(reader, "SecondaryBackgroundColorId"),
                        Hex = DbUtils.GetString(reader, "SecondaryBackgroundColorHex")
                    },
                    AccentTextColor = new Color()
                    {
                        Id = DbUtils.GetInt(reader, "AccentTextColorId"),
                        Hex = DbUtils.GetString(reader, "AccentTextColorHex")
                    },
                    PrimaryTextColor = new Color()
                    {
                        Id = DbUtils.GetInt(reader, "PrimaryTextColorId"),
                        Hex = DbUtils.GetString(reader, "PrimaryTextColorHex")
                    }
                }
            };
        }
    }

}
