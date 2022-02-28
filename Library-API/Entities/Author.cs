﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_API.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string name { get; set; }
        public List<Book> Books { get; set; }
    }
}
