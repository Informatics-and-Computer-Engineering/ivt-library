using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    public class LibraryContext : DbContext
    {
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleArticle> ArticleArticle { get; set; }
        public virtual DbSet<ArticleBook> ArticleBook { get; set; }
        public virtual DbSet<ArticleKeyword> ArticleKeyword { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<AuthorArticle> AuthorArticle { get; set; }
        public virtual DbSet<AuthorBook> AuthorBook { get; set; }
        public virtual DbSet<AuthorKeyword> AuthorKeyword { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookType> BookType { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Conference> Conference { get; set; }
        public virtual DbSet<Discipline> Discipline { get; set; }
        public virtual DbSet<DisciplineAuthor> DisciplineAuthor { get; set; }
        public virtual DbSet<Draft> Draft { get; set; }
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<FileArticle> FileArticle { get; set; }
        public virtual DbSet<FileBook> FileBook { get; set; }
        public virtual DbSet<FileResearch> FileResearch { get; set; }
        public virtual DbSet<Hypothesis> Hypothesis { get; set; }
        public virtual DbSet<Keyword> Keyword { get; set; }
        public virtual DbSet<Research> Research { get; set; }
        public virtual DbSet<ResearchArticle> ResearchArticle { get; set; }
        public virtual DbSet<ResearchAuthor> ResearchAuthor { get; set; }
        public virtual DbSet<ResearchBook> ResearchBook { get; set; }
        public virtual DbSet<ResearchTheme> ResearchTheme { get; set; }
        public virtual DbSet<Scale> Scale { get; set; }
        public virtual DbSet<Theme> Theme { get; set; }
        public virtual DbSet<ThemeArticle> ThemeArticle { get; set; }
        public virtual DbSet<ThemeAuthor> ThemeAuthor { get; set; }
        public virtual DbSet<ThemeBook> ThemeBook { get; set; }
        public virtual DbSet<FileType> FileType { get; set; }

        public LibraryContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("article");

                entity.ForNpgsqlHasComment("Статьи и публикации.");

                entity.HasIndex(e => e.SupervisorId)
                    .HasName("fki_article_author_supervisor_id");

                entity.HasIndex(e => new { e.Name, e.CityId, e.ConferenceId })
                    .HasName("uk_article")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Article_id_seq\"'::regclass)")
                    .ForNpgsqlHasComment("Уникальный внутренний идентификатор.");

                entity.Property(e => e.Bibliography)
                    .HasColumnName("bibliography")
                    .ForNpgsqlHasComment("Название статьи в виде, пригодном для списка литературы по ГОСТ 7.1-2003.");

                entity.Property(e => e.CityId)
                    .HasColumnName("city_id")
                    .ForNpgsqlHasComment("id города, где проходила конференция.");

                entity.Property(e => e.ConferenceEndDate)
                    .HasColumnName("conference_end_date")
                    .HasColumnType("date")
                    .ForNpgsqlHasComment("Дата окончания конференции.");

                entity.Property(e => e.ConferenceId)
                    .HasColumnName("conference_id")
                    .ForNpgsqlHasComment("id конференции.");

                entity.Property(e => e.ConferenceNumber)
                    .HasColumnName("conference_number")
                    .ForNpgsqlHasComment("В который раз проводится конференция.");

                entity.Property(e => e.ConferenceStartDate)
                    .HasColumnName("conference_start_date")
                    .HasColumnType("date")
                    .ForNpgsqlHasComment("Дата начала конференции.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .ForNpgsqlHasComment("Название статьи.");

                entity.Property(e => e.Page)
                    .HasColumnName("page")
                    .ForNpgsqlHasComment("Страница на которой начинается статья.");

                entity.Property(e => e.Pages)
                    .HasColumnName("pages")
                    .ForNpgsqlHasComment("Количество страниц в статье.");

                entity.Property(e => e.PublicationDate)
                    .HasColumnName("publication_date")
                    .HasColumnType("date")
                    .ForNpgsqlHasComment("Дата публикации.");

                entity.Property(e => e.SupervisorId)
                    .HasColumnName("supervisor_id")
                    .ForNpgsqlHasComment("Научный руководитель, указанный в статье.");

                entity.Property(e => e.Volume)
                    .HasColumnName("volume")
                    .ForNpgsqlHasComment("Номер тома.");

                entity.Property(e => e.Year)
                    .HasColumnName("year")
                    .ForNpgsqlHasComment("Год публикации.");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("fk_article_city");

                entity.HasOne(d => d.Conference)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.ConferenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_article_conference");

                entity.HasOne(d => d.Supervisor)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.SupervisorId)
                    .HasConstraintName("fk_article_author_supervisor_id");
            });

            modelBuilder.Entity<ArticleArticle>(entity =>
            {
                entity.HasKey(e => new { e.HostArticleId, e.ReferencedArticleId });

                entity.ToTable("article_article");

                entity.ForNpgsqlHasComment("Ссылки статей на другие статьи в списке литературы.");

                entity.Property(e => e.HostArticleId)
                    .HasColumnName("host_article_id")
                    .ForNpgsqlHasComment("Статья, которая ссылается на другую.");

                entity.Property(e => e.ReferencedArticleId)
                    .HasColumnName("referenced_article_id")
                    .ForNpgsqlHasComment("Статья, используемая в списке литературы.");

                entity.HasOne(d => d.HostArticle)
                    .WithMany(p => p.ArticleArticleHostArticle)
                    .HasForeignKey(d => d.HostArticleId)
                    .HasConstraintName("fk_host_article");

                entity.HasOne(d => d.ReferencedArticle)
                    .WithMany(p => p.ArticleArticleReferencedArticle)
                    .HasForeignKey(d => d.ReferencedArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_referenced_article");
            });

            modelBuilder.Entity<ArticleBook>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.BookId });

                entity.ToTable("article_book");

                entity.ForNpgsqlHasComment("Таблица книг, используемых в списках литературы статей.");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("article_id")
                    .ForNpgsqlHasComment("id статьи.");

                entity.Property(e => e.BookId)
                    .HasColumnName("book_id")
                    .ForNpgsqlHasComment("id книги.");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleBook)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("fk_article_book_article");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.ArticleBook)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_article_book_book");
            });

            modelBuilder.Entity<ArticleKeyword>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.KeywordId });

                entity.ToTable("article_keyword");

                entity.ForNpgsqlHasComment("Промежуточная таблица связывающая статьи и ключевые слова.");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("article_id")
                    .ForNpgsqlHasComment("id статьи.");

                entity.Property(e => e.KeywordId)
                    .HasColumnName("keyword_id")
                    .ForNpgsqlHasComment("id ключевого слова.");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleKeyword)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("fk_aricle_keyword_article");

                entity.HasOne(d => d.Keyword)
                    .WithMany(p => p.ArticleKeyword)
                    .HasForeignKey(d => d.KeywordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_article_keyword_keyword");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");

                entity.ForNpgsqlHasComment("Таблица авторов, статей, книг, экспериментов и пр.");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Author_id_seq\"'::regclass)")
                    .ForNpgsqlHasComment("Уникальный внутренний идентификатор.");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .ForNpgsqlHasComment("Имя автора.");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .ForNpgsqlHasComment("Фамилимя автора.");

                entity.Property(e => e.MiddleName)
                    .HasColumnName("middle_name")
                    .ForNpgsqlHasComment("Отчество автора.");
            });

            modelBuilder.Entity<AuthorArticle>(entity =>
            {
                entity.HasKey(e => new { e.AuthorId, e.ArticleId });

                entity.ToTable("author_article");

                entity.ForNpgsqlHasComment("Таблица, связывающая статьи и их авторов.");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("author_id")
                    .ForNpgsqlHasComment("id автора.");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("article_id")
                    .ForNpgsqlHasComment("id статьи.");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.AuthorArticle)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("fk_author_article_article");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AuthorArticle)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_author_article_author");
            });

            modelBuilder.Entity<AuthorBook>(entity =>
            {
                entity.HasKey(e => new { e.AuthorId, e.BookId });

                entity.ToTable("author_book");

                entity.ForNpgsqlHasComment("Таблица, связывающая авторов и их книги.");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("author_id")
                    .ForNpgsqlHasComment("Ссылка на автора.");

                entity.Property(e => e.BookId)
                    .HasColumnName("book_id")
                    .ForNpgsqlHasComment("Ссылка на книгу.");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AuthorBook)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_author_book_author");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.AuthorBook)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("fk_author_book_book");
            });

            modelBuilder.Entity<AuthorKeyword>(entity =>
            {
                entity.HasKey(e => new { e.AuthorId, e.KeywordId });

                entity.ToTable("author_keyword");

                entity.ForNpgsqlHasComment("Промежуточная таблица, связывающая авторов и ключевые слова.");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("author_id")
                    .ForNpgsqlHasComment("id автора.");

                entity.Property(e => e.KeywordId)
                    .HasColumnName("keyword_id")
                    .ForNpgsqlHasComment("id ключевого слова.");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AuthorKeyword)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("fk_author_keyword_author");

                entity.HasOne(d => d.Keyword)
                    .WithMany(p => p.AuthorKeyword)
                    .HasForeignKey(d => d.KeywordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_author_keyword_keyword");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.ForNpgsqlHasComment("Книги.");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Book_id_seq\"'::regclass)")
                    .ForNpgsqlHasComment("Уникальный внутренний идентификатор.");

                entity.Property(e => e.Bibliography)
                    .HasColumnName("bibliography")
                    .ForNpgsqlHasComment("Название книги в виде, пригодном для списка литературы по ГОСТ 7.1-2003.");

                entity.Property(e => e.BookTypeId)
                    .HasColumnName("book_type_id")
                    .HasDefaultValueSql("1")
                    .ForNpgsqlHasComment("Ссылка на тип книги.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .ForNpgsqlHasComment("Название книги.");

                entity.Property(e => e.Publisher)
                    .HasColumnName("publisher")
                    .ForNpgsqlHasComment("Издатель.");

                entity.Property(e => e.Volume)
                    .HasColumnName("volume")
                    .ForNpgsqlHasComment("Количество страниц.");

                entity.Property(e => e.Year)
                    .HasColumnName("year")
                    .ForNpgsqlHasComment("Год издания книги.");

                entity.HasOne(d => d.BookType)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.BookTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_book_book_type");
            });

            modelBuilder.Entity<BookType>(entity =>
            {
                entity.ToTable("book_type");

                entity.ForNpgsqlHasComment("Таблица с типами книг.");

                entity.HasIndex(e => e.Name)
                    .HasName("uk_book_type_name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Book_Type_id_seq\"'::regclass)")
                    .ForNpgsqlHasComment("Уникальный внутренний идентификатор.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .ForNpgsqlHasComment("Название типа книги.");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.ForNpgsqlHasComment("Города, в которых проводятся конференции.");

                entity.HasIndex(e => e.Name)
                    .HasName("uk_city_name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"City_id_seq\"'::regclass)")
                    .ForNpgsqlHasComment("Уникальный внутренний идентификатор.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .ForNpgsqlHasComment("Название города.");
            });

            modelBuilder.Entity<Conference>(entity =>
            {
                entity.ToTable("conference");

                entity.ForNpgsqlHasComment("Конференции.");

                entity.HasIndex(e => e.ScaleId)
                    .HasName("fki_scale");

                entity.HasIndex(e => new { e.Name, e.Place })
                    .HasName("uk_conference")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Conference_id_seq\"'::regclass)")
                    .ForNpgsqlHasComment("Уникальный идентификатор.");

                entity.Property(e => e.FullName)
                    .HasColumnName("full_name")
                    .ForNpgsqlHasComment("Полное название конференции, включая место её проведения.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .ForNpgsqlHasComment("Название конференции.");

                entity.Property(e => e.Place)
                    .IsRequired()
                    .HasColumnName("place")
                    .ForNpgsqlHasComment("Место проведения. (университет, ВУЗ)");

                entity.Property(e => e.ScaleId)
                    .HasColumnName("scale_id")
                    .ForNpgsqlHasComment("id масштаба конференции.");

                entity.HasOne(d => d.Scale)
                    .WithMany(p => p.Conference)
                    .HasForeignKey(d => d.ScaleId)
                    .HasConstraintName("fk_conference_scale");
            });

            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.ToTable("discipline");

                entity.ForNpgsqlHasComment("Таблица со списком предметов.");

                entity.HasIndex(e => e.Name)
                    .HasName("uk_discipline_name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Discipline_id_seq\"'::regclass)")
                    .ForNpgsqlHasComment("Уникальный внутренний идентификатор.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .ForNpgsqlHasComment("Название дисциплины.");

                entity.Property(e => e.Semester)
                    .HasColumnName("semester")
                    .ForNpgsqlHasComment("Семестр в котором ведется дисциплина.");
            });

            modelBuilder.Entity<DisciplineAuthor>(entity =>
            {
                entity.HasKey(e => new { e.DisciplineId, e.AuthorId });

                entity.ToTable("discipline_author");

                entity.ForNpgsqlHasComment("Промежуточная таблица связывающая преподавателей и предметы.");

                entity.Property(e => e.DisciplineId)
                    .HasColumnName("discipline_id")
                    .ForNpgsqlHasComment("id дисциплины.");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("author_id")
                    .ForNpgsqlHasComment("id автора.");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.DisciplineAuthor)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_discipline_author_author");

                entity.HasOne(d => d.Discipline)
                    .WithMany(p => p.DisciplineAuthor)
                    .HasForeignKey(d => d.DisciplineId)
                    .HasConstraintName("fk_discipline_author_discipline");
            });

            modelBuilder.Entity<Draft>(entity =>
            {
                entity.ToTable("draft");

                entity.ForNpgsqlHasComment("Записки, мысли и черновики.");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Draft_id_seq\"'::regclass)")
                    .ForNpgsqlHasComment("Уникальный внутренний идентификатор.");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .ForNpgsqlHasComment("Содержимое записки.");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creation_date")
                    .HasDefaultValueSql("now()")
                    .ForNpgsqlHasComment("Дата создания записи.");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .ForNpgsqlHasComment("Название, заголовок.");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.ToTable("file");

                entity.ForNpgsqlHasComment("Таблица со всеми файлами.");

                entity.HasIndex(e => new { e.TypeId, e.Version })
                    .HasName("ix_file_type_version");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"File_id_seq\"'::regclass)")
                    .ForNpgsqlHasComment("Уникальный идентификатор файла.");

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasColumnName("content_type")
                    .HasDefaultValueSql("'unknown'::character varying")
                    .ForNpgsqlHasComment("Формат файла.");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data")
                    .ForNpgsqlHasComment("Файл.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .ForNpgsqlHasComment("Название файла в ФС.");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.File)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("fk_file_type");
            });

            modelBuilder.Entity<FileArticle>(entity =>
            {
                entity.ToTable("file_article");

                entity.ForNpgsqlHasComment("Файлы статей. Таблица наследована от общей таблицы файлов.");

                entity.HasIndex(e => new { e.TypeId, e.ArticleId })
                    .HasName("ix_file_article_type_article");

                entity.HasIndex(e => new { e.TypeId, e.Version })
                    .HasName("ix_file_article_type_version");

                entity.HasIndex(e => new { e.TypeId, e.Version, e.ArticleId })
                    .HasName("ix_file_article_type_id_version_article");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"File_id_seq\"'::regclass)");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("article_id")
                    .ForNpgsqlHasComment("Ссылка на статью.");

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasColumnName("content_type")
                    .HasDefaultValueSql("'unknown'::character varying");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.FileArticle)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("fk_file_article_article");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.FileArticle)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("fk_file_article_type");
            });

            modelBuilder.Entity<FileBook>(entity =>
            {
                entity.ToTable("file_book");

                entity.ForNpgsqlHasComment("Файлы книиг. Таблица наследована  от общей таблицы файлов.");

                entity.HasIndex(e => new { e.TypeId, e.BookId })
                    .HasName("ix_file_book_type_book");

                entity.HasIndex(e => new { e.TypeId, e.Version })
                    .HasName("ix_file_book_type_version");

                entity.HasIndex(e => new { e.TypeId, e.Version, e.BookId })
                    .HasName("ix_file_book_type_id_version_book");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"File_id_seq\"'::regclass)");

                entity.Property(e => e.BookId)
                    .HasColumnName("book_id")
                    .ForNpgsqlHasComment("Ссылка на книгу.");

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasColumnName("content_type")
                    .HasDefaultValueSql("'unknown'::character varying");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.FileBook)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("fk_file_book_book");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.FileBook)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("fk_file_book_type");
            });

            modelBuilder.Entity<FileResearch>(entity =>
            {
                entity.ToTable("file_research");

                entity.ForNpgsqlHasComment("Файлы исследований. Таблица наследована  от общей таблицы файлов.");

                entity.HasIndex(e => new { e.TypeId, e.ResearchId })
                    .HasName("ix_file_research_type_research");

                entity.HasIndex(e => new { e.TypeId, e.Version })
                    .HasName("ix_file_research_type_version");

                entity.HasIndex(e => new { e.TypeId, e.Version, e.ResearchId })
                    .HasName("ix_file_research_type_id_version_research");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"File_id_seq\"'::regclass)");

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasColumnName("content_type")
                    .HasDefaultValueSql("'unknown'::character varying");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.ResearchId)
                    .HasColumnName("research_id")
                    .ForNpgsqlHasComment("Ссылка на исследование.");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.Research)
                    .WithMany(p => p.FileResearch)
                    .HasForeignKey(d => d.ResearchId)
                    .HasConstraintName("fk_file_research_research");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.FileResearch)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("fk_file_research_type");
            });

            modelBuilder.Entity<Hypothesis>(entity =>
            {
                entity.ToTable("hypothesis");

                entity.ForNpgsqlHasComment("Гипотезы.");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Hypothesis_id_seq\"'::regclass)")
                    .ForNpgsqlHasComment("Уникальный внутренний идентификатор.");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .ForNpgsqlHasComment("Текст гипотезы.");

                entity.Property(e => e.Explanation)
                    .HasColumnName("explanation")
                    .ForNpgsqlHasComment("Пояснения к гипотезе.");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .ForNpgsqlHasComment("Название гипотезы.");
            });

            modelBuilder.Entity<Keyword>(entity =>
            {
                entity.ToTable("keyword");

                entity.ForNpgsqlHasComment("Таблица с ключевыми словами.");

                entity.HasIndex(e => e.Name)
                    .HasName("uk_keyword")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Keyword_id_seq\"'::regclass)")
                    .ForNpgsqlHasComment("Уникальный внутренний идентификатор.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .ForNpgsqlHasComment("Ключевое слово или фраза.");
            });

            modelBuilder.Entity<Research>(entity =>
            {
                entity.ToTable("research");

                entity.ForNpgsqlHasComment("Исследования.");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Research_id_seq\"'::regclass)")
                    .ForNpgsqlHasComment("Уникальный внутренний идентификатор таблицы.");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .ForNpgsqlHasComment("Описание эксперимента.");

                entity.Property(e => e.Goal).HasColumnName("goal");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .ForNpgsqlHasComment("Название эксперимента.");

                entity.Property(e => e.Progress).HasColumnName("progress");

                entity.Property(e => e.Tasks).HasColumnName("tasks");
            });

            modelBuilder.Entity<ResearchArticle>(entity =>
            {
                entity.HasKey(e => new { e.ResearchId, e.ArticleId });

                entity.ToTable("research_article");

                entity.ForNpgsqlHasComment("Таблица связывающая статьи и используемые в них исследования.");

                entity.Property(e => e.ResearchId)
                    .HasColumnName("research_id")
                    .ForNpgsqlHasComment("id исследования.");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("article_id")
                    .ForNpgsqlHasComment("id статьи");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ResearchArticle)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("fk_research_article_article");

                entity.HasOne(d => d.Research)
                    .WithMany(p => p.ResearchArticle)
                    .HasForeignKey(d => d.ResearchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_research_article_research");
            });

            modelBuilder.Entity<ResearchAuthor>(entity =>
            {
                entity.HasKey(e => new { e.AuthorId, e.ResearchId });

                entity.ToTable("research_author");

                entity.ForNpgsqlHasComment("Таблица, связывающая автора и их исследования.");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("author_id")
                    .ForNpgsqlHasComment("id автора.");

                entity.Property(e => e.ResearchId)
                    .HasColumnName("research_id")
                    .ForNpgsqlHasComment("id исследования.");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.ResearchAuthor)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("fk_research_author_author");

                entity.HasOne(d => d.Research)
                    .WithMany(p => p.ResearchAuthor)
                    .HasForeignKey(d => d.ResearchId)
                    .HasConstraintName("fk_research_author_research");
            });

            modelBuilder.Entity<ResearchBook>(entity =>
            {
                entity.HasKey(e => new { e.ResearchId, e.BookId });

                entity.ToTable("research_book");

                entity.ForNpgsqlHasComment("Таблица, связывающая исследования и книги.");

                entity.Property(e => e.ResearchId)
                    .HasColumnName("research_id")
                    .ForNpgsqlHasComment("id исследования.");

                entity.Property(e => e.BookId)
                    .HasColumnName("book_id")
                    .ForNpgsqlHasComment("id книги.");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.ResearchBook)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("fk_research_book_book");

                entity.HasOne(d => d.Research)
                    .WithMany(p => p.ResearchBook)
                    .HasForeignKey(d => d.ResearchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_research_book_research");
            });

            modelBuilder.Entity<ResearchTheme>(entity =>
            {
                entity.HasKey(e => new { e.ResearchId, e.ThemeId });

                entity.ToTable("research_theme");

                entity.ForNpgsqlHasComment("Таблица, связывающая исследования и их тематику.");

                entity.Property(e => e.ResearchId)
                    .HasColumnName("research_id")
                    .ForNpgsqlHasComment("id исследования.");

                entity.Property(e => e.ThemeId)
                    .HasColumnName("theme_id")
                    .ForNpgsqlHasComment("id темы.");

                entity.HasOne(d => d.Research)
                    .WithMany(p => p.ResearchTheme)
                    .HasForeignKey(d => d.ResearchId)
                    .HasConstraintName("fk_research_theme_research");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.ResearchTheme)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_research_theme_theme");
            });

            modelBuilder.Entity<Scale>(entity =>
            {
                entity.ToTable("scale");

                entity.ForNpgsqlHasComment("Таблица с перечнем масштабов конференций.");

                entity.HasIndex(e => e.Name)
                    .HasName("uk_scale")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Scale_id_seq\"'::regclass)")
                    .ForNpgsqlHasComment("Уникальный внутренний идентификатор.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .ForNpgsqlHasComment("Масштаб (Городская, Международная, Региональная и т. д.).");
            });

            modelBuilder.Entity<Theme>(entity =>
            {
                entity.ToTable("theme");

                entity.ForNpgsqlHasComment("Тематики научных исследований, публикаций и т. д.");

                entity.HasIndex(e => e.Name)
                    .HasName("uk_theme_name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Theme_id_seq\"'::regclass)")
                    .ForNpgsqlHasComment("Уникальный внутренний идентификатор.");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .ForNpgsqlHasComment("Описание темы.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .ForNpgsqlHasComment("Название темы.");
            });

            modelBuilder.Entity<ThemeArticle>(entity =>
            {
                entity.HasKey(e => new { e.ThemeId, e.ArticleId });

                entity.ToTable("theme_article");

                entity.ForNpgsqlHasComment("Таблица тем той или иной статьи.");

                entity.Property(e => e.ThemeId)
                    .HasColumnName("theme_id")
                    .ForNpgsqlHasComment("id темы.");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("article_id")
                    .ForNpgsqlHasComment("id статьи.");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ThemeArticle)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("fk_theme_article_article");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.ThemeArticle)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_theme_article_theme");
            });

            modelBuilder.Entity<ThemeAuthor>(entity =>
            {
                entity.HasKey(e => new { e.ThemeId, e.AuthorId });

                entity.ToTable("theme_author");

                entity.ForNpgsqlHasComment("Таблица, связывающая авторов и темы их публикаций и работ.");

                entity.HasIndex(e => e.AuthorId)
                    .HasName("fki_theme_author_author");

                entity.Property(e => e.ThemeId)
                    .HasColumnName("theme_id")
                    .ForNpgsqlHasComment("id темы.");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("author_id")
                    .ForNpgsqlHasComment("id автора.");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.ThemeAuthor)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("fk_theme_author_author");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.ThemeAuthor)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_theme_author_theme");
            });

            modelBuilder.Entity<ThemeBook>(entity =>
            {
                entity.HasKey(e => new { e.ThemeId, e.BookId });

                entity.ToTable("theme_book");

                entity.ForNpgsqlHasComment("Таблица, связывающая книги и темы.");

                entity.Property(e => e.ThemeId)
                    .HasColumnName("theme_id")
                    .ForNpgsqlHasComment("id темы.");

                entity.Property(e => e.BookId)
                    .HasColumnName("book_id")
                    .ForNpgsqlHasComment("id книги.");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.ThemeBook)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("fk_theme_book_book");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.ThemeBook)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_theme_book_theme");
            });

            modelBuilder.Entity<FileType>(entity =>
            {
                entity.ToTable("file_type");

                entity.ForNpgsqlHasComment("Тип содержимого тех или иных элементов.");

                entity.HasIndex(e => e.Name)
                    .HasName("uk_type")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Type_id_seq\"'::regclass)")
                    .ForNpgsqlHasComment("Уникальный внутренний идентификатор.");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .ForNpgsqlHasComment("Описание типа.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .ForNpgsqlHasComment("Название типа.");
            });

            modelBuilder.HasSequence("Article_id_seq");

            modelBuilder.HasSequence("Author_id_seq");

            modelBuilder.HasSequence("Book_id_seq");

            modelBuilder.HasSequence("Book_Type_id_seq");

            modelBuilder.HasSequence("City_id_seq");

            modelBuilder.HasSequence("Conference_id_seq");

            modelBuilder.HasSequence("Discipline_id_seq");

            modelBuilder.HasSequence("Draft_id_seq");

            modelBuilder.HasSequence("File_id_seq");

            modelBuilder.HasSequence("Hypothesis_id_seq");

            modelBuilder.HasSequence("Keyword_id_seq");

            modelBuilder.HasSequence("Research_id_seq");

            modelBuilder.HasSequence("Scale_id_seq");

            modelBuilder.HasSequence("Theme_id_seq");

            modelBuilder.HasSequence("Type_id_seq");
        }
    }
}
