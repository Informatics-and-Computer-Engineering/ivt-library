using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class FileType
    {
        public FileType()
        {
            File = new HashSet<File>();
            FileArticle = new HashSet<FileArticle>();
            FileBook = new HashSet<FileBook>();
            FileResearch = new HashSet<FileResearch>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<File> File { get; set; }
        public ICollection<FileArticle> FileArticle { get; set; }
        public ICollection<FileBook> FileBook { get; set; }
        public ICollection<FileResearch> FileResearch { get; set; }
    }
}
