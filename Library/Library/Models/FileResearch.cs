using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class FileResearch
    {
        public int Id { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public string Name { get; set; }
        public int ResearchId { get; set; }
        public int? TypeId { get; set; }
        public int Version { get; set; }

        public Research Research { get; set; }
        public FileType Type { get; set; }
    }
}
