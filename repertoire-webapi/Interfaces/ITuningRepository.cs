using repertoire_webapi.Models;
using System.Collections.Generic;

namespace repertoire_webapi.Interfaces
{
    public interface ITuningRepository
    {
        void AddTuning(Tuning tuning);
        Tuning GetTuningById(int tuningId);
        List<Tuning> GetAllTunings();
    }
}