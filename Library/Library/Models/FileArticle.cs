using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class FileArticle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public int? TypeId { get; set; }
        public int Version { get; set; }
        public int ArticleId { get; set; }

        public Article Article { get; set; }
        public Type Type { get; set; }
    }
}
