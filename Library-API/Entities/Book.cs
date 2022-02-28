using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library_API.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string title { get; set; }
        public int authorId { get; set; }
        public string cover { get; set; }
        public Author author { get; set; }
    }
}
