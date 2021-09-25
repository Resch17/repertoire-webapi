﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace repertoire_webapi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirebaseId { get; set; }
        public int ThemeId { get; set; }
        public Theme Theme { get; set; }
    }
}
