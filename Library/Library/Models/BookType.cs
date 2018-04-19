using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class BookType
    {
        public BookType()
        {
            Book = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Book { get; set; }
    }
}
