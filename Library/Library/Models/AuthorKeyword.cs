using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class AuthorKeyword
    {
        public int AuthorId { get; set; }
        public int KeywordId { get; set; }

        public Author Author { get; set; }
        public Keyword Keyword { get; set; }
    }
}
