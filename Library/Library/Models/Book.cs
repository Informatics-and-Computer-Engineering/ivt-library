using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Book
    {
        public Book()
        {
            ArticleBook = new HashSet<ArticleBook>();
            AuthorBook = new HashSet<AuthorBook>();
            FileBook = new HashSet<FileBook>();
            ResearchBook = new HashSet<ResearchBook>();
            ThemeBook = new HashSet<ThemeBook>();
        }

        public int Id { get; set; }
        public string Bibliography { get; set; }
        public int BookTypeId { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public int? Volume { get; set; }
        public int? Year { get; set; }

        public BookType BookType { get; set; }
        public ICollection<ArticleBook> ArticleBook { get; set; }
        public ICollection<AuthorBook> AuthorBook { get; set; }
        public ICollection<FileBook> FileBook { get; set; }
        public ICollection<ResearchBook> ResearchBook { get; set; }
        public ICollection<ThemeBook> ThemeBook { get; set; }
    }
}
