using repertoire_webapi.Models;

namespace repertoire_webapi.Interfaces
{
    public interface ITuningRepository
    {
        void AddTuning(Tuning tuning);
        Tuning GetTuningById(int tuningId);
    }
}