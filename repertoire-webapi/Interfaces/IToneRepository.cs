using repertoire_webapi.Models;
using System.Collections.Generic;

namespace repertoire_webapi.Interfaces
{
    public interface IToneRepository
    {
        void AddTone(Tone tone);
        List<Tone> GetAllTones();
    }
}