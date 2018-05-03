using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class File
    {
        public int Id { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public string Name { get; set; }
        public int? TypeId { get; set; }
        public int Version { get; set; }

        public FileType Type { get; set; }
    }
}
