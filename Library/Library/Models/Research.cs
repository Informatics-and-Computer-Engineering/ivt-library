using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Research
    {
        public Research()
        {
            FileResearch = new HashSet<FileResearch>();
            ResearchArticle = new HashSet<ResearchArticle>();
            ResearchAuthor = new HashSet<ResearchAuthor>();
            ResearchBook = new HashSet<ResearchBook>();
            ResearchTheme = new HashSet<ResearchTheme>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Goal { get; set; }
        public string Tasks { get; set; }
        public short? Progress { get; set; }

        public ICollection<FileResearch> FileResearch { get; set; }
        public ICollection<ResearchArticle> ResearchArticle { get; set; }
        public ICollection<ResearchAuthor> ResearchAuthor { get; set; }
        public ICollection<ResearchBook> ResearchBook { get; set; }
        public ICollection<ResearchTheme> ResearchTheme { get; set; }
    }
}
