using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace repertoire_webapi.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        public int InstrumentId { get; set; }
        public int UserId { get; set; }
        public int TuningId { get; set; }
        public string? Url { get; set; }
        public string? Youtube { get; set; }
        public Artist Artist { get; set; }
        public Genre Genre { get; set; }
        public Instrument Instrument { get; set; }
        public User User { get; set; }
        public Tuning Tuning { get; set; }
    }
}
