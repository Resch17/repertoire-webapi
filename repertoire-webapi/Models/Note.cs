using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace repertoire_webapi.Models
{
    public class Note
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public Song Song { get; set; }
        public User User { get; set; }
    }
}
