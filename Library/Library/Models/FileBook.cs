using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class FileBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public int? TypeId { get; set; }
        public int Version { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
        public FileType Type { get; set; }
    }
}
