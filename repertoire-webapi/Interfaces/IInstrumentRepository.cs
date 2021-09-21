using repertoire_webapi.Models;
using System.Collections.Generic;

namespace repertoire_webapi.Interfaces
{
    public interface IInstrumentRepository
    {
        void AddInstrument(Instrument instrument);
        List<Instrument> GetAllInstruments();
    }
}