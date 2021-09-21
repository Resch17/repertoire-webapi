using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace repertoire_webapi.Models
{
    public class Theme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BackgroundColorId { get; set; }
        public int SecondaryBackgroundColorId { get; set; }
        public int AccentTextColorId { get; set; }
        public int PrimaryTextColorId { get; set; }
        public Color BackgroundColor { get; set; }
        public Color SecondaryBackgroundColor { get; set; }
        public Color AccentTextColor { get; set; }
        public Color PrimaryTextColor { get; set; }
    }
}
