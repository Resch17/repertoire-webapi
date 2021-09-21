using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace repertoire_webapi.Models
{
    public class Tuning
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InstrumentId { get; set; }
        public int String1ToneId { get; set; }
        public int String2ToneId { get; set; }
        public int String3ToneId { get; set; }
        public int String4ToneId { get; set; }
        public int? String5ToneId { get; set; }
        public int? String6ToneId { get; set; }
        public Instrument Instrument { get; set; }
        public Tone String1Tone { get; set; }
        public Tone String2Tone { get; set; }
        public Tone String3Tone { get; set; }
        public Tone String4Tone { get; set; }
        public Tone? String5Tone { get; set; }
        public Tone? String6Tone { get; set; }
    }
}
