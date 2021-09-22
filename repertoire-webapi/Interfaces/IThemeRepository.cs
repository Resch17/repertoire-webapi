using repertoire_webapi.Models;
using System.Collections.Generic;

namespace repertoire_webapi.Interfaces
{
    public interface IThemeRepository
    {
        void AddTheme(Theme theme);
        List<Theme> GetAllThemes();
    }
}