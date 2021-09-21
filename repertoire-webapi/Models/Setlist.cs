using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace repertoire_webapi.Models
{
    public class Setlist
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SongId { get; set; }
        public int Ordinal { get; set; }
    }
}
