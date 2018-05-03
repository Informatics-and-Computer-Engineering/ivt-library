using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Article
    {
        public Article()
        {
            ArticleArticleHostArticle = new HashSet<ArticleArticle>();
            ArticleArticleReferencedArticle = new HashSet<ArticleArticle>();
            ArticleBook = new HashSet<ArticleBook>();
            ArticleKeyword = new HashSet<ArticleKeyword>();
            AuthorArticle = new HashSet<AuthorArticle>();
            FileArticle = new HashSet<FileArticle>();
            ResearchArticle = new HashSet<ResearchArticle>();
            ThemeArticle = new HashSet<ThemeArticle>();
        }

        public int Id { get; set; }
        public string Bibliography { get; set; }
        public int? CityId { get; set; }
        public DateTime? ConferenceEndDate { get; set; }
        public int ConferenceId { get; set; }
        public int? ConferenceNumber { get; set; }
        public DateTime? ConferenceStartDate { get; set; }
        public string Name { get; set; }
        public int? Page { get; set; }
        public int? Pages { get; set; }
        public DateTime? PublicationDate { get; set; }
        public int? SupervisorId { get; set; }
        public int? Volume { get; set; }
        public int? Year { get; set; }

        public City City { get; set; }
        public Conference Conference { get; set; }
        public Author Supervisor { get; set; }
        public ICollection<ArticleArticle> ArticleArticleHostArticle { get; set; }
        public ICollection<ArticleArticle> ArticleArticleReferencedArticle { get; set; }
        public ICollection<ArticleBook> ArticleBook { get; set; }
        public ICollection<ArticleKeyword> ArticleKeyword { get; set; }
        public ICollection<AuthorArticle> AuthorArticle { get; set; }
        public ICollection<FileArticle> FileArticle { get; set; }
        public ICollection<ResearchArticle> ResearchArticle { get; set; }
        public ICollection<ThemeArticle> ThemeArticle { get; set; }
    }
}
