using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Library.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "Article_id_seq");

            migrationBuilder.CreateSequence(
                name: "Author_id_seq");

            migrationBuilder.CreateSequence(
                name: "Book_id_seq");

            migrationBuilder.CreateSequence(
                name: "Book_Type_id_seq");

            migrationBuilder.CreateSequence(
                name: "City_id_seq");

            migrationBuilder.CreateSequence(
                name: "Conference_id_seq");

            migrationBuilder.CreateSequence(
                name: "Discipline_id_seq");

            migrationBuilder.CreateSequence(
                name: "Draft_id_seq");

            migrationBuilder.CreateSequence(
                name: "File_id_seq");

            migrationBuilder.CreateSequence(
                name: "Hypothesis_id_seq");

            migrationBuilder.CreateSequence(
                name: "Keyword_id_seq");

            migrationBuilder.CreateSequence(
                name: "Research_id_seq");

            migrationBuilder.CreateSequence(
                name: "Scale_id_seq");

            migrationBuilder.CreateSequence(
                name: "Theme_id_seq");

            migrationBuilder.CreateSequence(
                name: "Type_id_seq");

            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"Author_id_seq\"'::regclass)")
                        .Annotation("Npgsql:Comment", "Уникальный внутренний идентификатор."),
                    first_name = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Имя автора."),
                    last_name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Фамилимя автора."),
                    middle_name = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Отчество автора.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Таблица авторов, статей, книг, экспериментов и пр.");

            migrationBuilder.CreateTable(
                name: "book_type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"Book_Type_id_seq\"'::regclass)")
                        .Annotation("Npgsql:Comment", "Уникальный внутренний идентификатор."),
                    name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Название типа книги.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_type", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Таблица с типами книг.");

            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"City_id_seq\"'::regclass)")
                        .Annotation("Npgsql:Comment", "Уникальный внутренний идентификатор."),
                    name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Название города.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Города, в которых проводятся конференции.");

            migrationBuilder.CreateTable(
                name: "discipline",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"Discipline_id_seq\"'::regclass)")
                        .Annotation("Npgsql:Comment", "Уникальный внутренний идентификатор."),
                    name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Название дисциплины."),
                    semester = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Семестр в котором ведется дисциплина.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discipline", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Таблица со списком предметов.");

            migrationBuilder.CreateTable(
                name: "draft",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false, defaultValueSql: "nextval('\"Draft_id_seq\"'::regclass)")
                        .Annotation("Npgsql:Comment", "Уникальный внутренний идентификатор."),
                    content = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Содержимое записки."),
                    creation_date = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "now()")
                        .Annotation("Npgsql:Comment", "Дата создания записи."),
                    title = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Название, заголовок.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_draft", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Записки, мысли и черновики.");

            migrationBuilder.CreateTable(
                name: "hypothesis",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false, defaultValueSql: "nextval('\"Hypothesis_id_seq\"'::regclass)")
                        .Annotation("Npgsql:Comment", "Уникальный внутренний идентификатор."),
                    content = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Текст гипотезы."),
                    explanation = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Пояснения к гипотезе."),
                    name = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Название гипотезы.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hypothesis", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Гипотезы.");

            migrationBuilder.CreateTable(
                name: "keyword",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"Keyword_id_seq\"'::regclass)")
                        .Annotation("Npgsql:Comment", "Уникальный внутренний идентификатор."),
                    name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ключевое слово или фраза.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_keyword", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Таблица с ключевыми словами.");

            migrationBuilder.CreateTable(
                name: "research",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"Research_id_seq\"'::regclass)")
                        .Annotation("Npgsql:Comment", "Уникальный внутренний идентификатор таблицы."),
                    description = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Описание эксперимента."),
                    goal = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Название эксперимента."),
                    progress = table.Column<short>(nullable: true),
                    tasks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_research", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Исследования.");

            migrationBuilder.CreateTable(
                name: "scale",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"Scale_id_seq\"'::regclass)")
                        .Annotation("Npgsql:Comment", "Уникальный внутренний идентификатор."),
                    name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Масштаб (Городская, Международная, Региональная и т. д.).")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scale", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Таблица с перечнем масштабов конференций.");

            migrationBuilder.CreateTable(
                name: "theme",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"Theme_id_seq\"'::regclass)")
                        .Annotation("Npgsql:Comment", "Уникальный внутренний идентификатор."),
                    description = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Описание темы."),
                    name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Название темы.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_theme", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Тематики научных исследований, публикаций и т. д.");

            migrationBuilder.CreateTable(
                name: "type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"Type_id_seq\"'::regclass)")
                        .Annotation("Npgsql:Comment", "Уникальный внутренний идентификатор."),
                    description = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Описание типа."),
                    name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Название типа.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Тип содержимого тех или иных элементов.");

            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"Book_id_seq\"'::regclass)")
                        .Annotation("Npgsql:Comment", "Уникальный внутренний идентификатор."),
                    bibliography = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Название книги в виде, пригодном для списка литературы по ГОСТ 7.1-2003."),
                    book_type_id = table.Column<int>(nullable: false, defaultValueSql: "1")
                        .Annotation("Npgsql:Comment", "Ссылка на тип книги."),
                    name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Название книги."),
                    publisher = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Издатель."),
                    volume = table.Column<int>(nullable: true)
                        .Annotation("Npgsql:Comment", "Количество страниц."),
                    year = table.Column<int>(nullable: true)
                        .Annotation("Npgsql:Comment", "Год издания книги.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book", x => x.id);
                    table.ForeignKey(
                        name: "fk_book_book_type",
                        column: x => x.book_type_id,
                        principalTable: "book_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Книги.");

            migrationBuilder.CreateTable(
                name: "discipline_author",
                columns: table => new
                {
                    discipline_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id дисциплины."),
                    author_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id автора.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discipline_author", x => new { x.discipline_id, x.author_id });
                    table.ForeignKey(
                        name: "fk_discipline_author_author",
                        column: x => x.author_id,
                        principalTable: "author",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_discipline_author_discipline",
                        column: x => x.discipline_id,
                        principalTable: "discipline",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Npgsql:Comment", "Промежуточная таблица связывающая преподавателей и предметы.");

            migrationBuilder.CreateTable(
                name: "author_keyword",
                columns: table => new
                {
                    author_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id автора."),
                    keyword_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id ключевого слова.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author_keyword", x => new { x.author_id, x.keyword_id });
                    table.ForeignKey(
                        name: "fk_author_keyword_author",
                        column: x => x.author_id,
                        principalTable: "author",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_author_keyword_keyword",
                        column: x => x.keyword_id,
                        principalTable: "keyword",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Промежуточная таблица, связывающая авторов и ключевые слова.");

            migrationBuilder.CreateTable(
                name: "research_author",
                columns: table => new
                {
                    author_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id автора."),
                    research_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id исследования.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_research_author", x => new { x.author_id, x.research_id });
                    table.ForeignKey(
                        name: "fk_research_author_author",
                        column: x => x.author_id,
                        principalTable: "author",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_research_author_research",
                        column: x => x.research_id,
                        principalTable: "research",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Npgsql:Comment", "Таблица, связывающая автора и их исследования.");

            migrationBuilder.CreateTable(
                name: "conference",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"Conference_id_seq\"'::regclass)")
                        .Annotation("Npgsql:Comment", "Уникальный идентификатор."),
                    full_name = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Полное название конференции, включая место её проведения."),
                    name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Название конференции."),
                    place = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Место проведения. (университет, ВУЗ)"),
                    scale_id = table.Column<int>(nullable: true)
                        .Annotation("Npgsql:Comment", "id масштаба конференции.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conference", x => x.id);
                    table.ForeignKey(
                        name: "fk_conference_scale",
                        column: x => x.scale_id,
                        principalTable: "scale",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Конференции.");

            migrationBuilder.CreateTable(
                name: "research_theme",
                columns: table => new
                {
                    research_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id исследования."),
                    theme_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id темы.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_research_theme", x => new { x.research_id, x.theme_id });
                    table.ForeignKey(
                        name: "fk_research_theme_research",
                        column: x => x.research_id,
                        principalTable: "research",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_research_theme_theme",
                        column: x => x.theme_id,
                        principalTable: "theme",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Таблица, связывающая исследования и их тематику.");

            migrationBuilder.CreateTable(
                name: "theme_author",
                columns: table => new
                {
                    theme_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id темы."),
                    author_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id автора.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_theme_author", x => new { x.theme_id, x.author_id });
                    table.ForeignKey(
                        name: "fk_theme_author_author",
                        column: x => x.author_id,
                        principalTable: "author",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_theme_author_theme",
                        column: x => x.theme_id,
                        principalTable: "theme",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Таблица, связывающая авторов и темы их публикаций и работ.");

            migrationBuilder.CreateTable(
                name: "file",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"File_id_seq\"'::regclass)")
                        .Annotation("Npgsql:Comment", "Уникальный идентификатор файла."),
                    content_type = table.Column<string>(nullable: false, defaultValueSql: "'unknown'::character varying")
                        .Annotation("Npgsql:Comment", "Формат файла."),
                    data = table.Column<byte[]>(nullable: false)
                        .Annotation("Npgsql:Comment", "Файл."),
                    name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Название файла в ФС."),
                    type_id = table.Column<int>(nullable: true),
                    version = table.Column<int>(nullable: false, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file", x => x.id);
                    table.ForeignKey(
                        name: "fk_file_type",
                        column: x => x.type_id,
                        principalTable: "type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Таблица со всеми файлами.");

            migrationBuilder.CreateTable(
                name: "file_research",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"File_id_seq\"'::regclass)"),
                    content_type = table.Column<string>(nullable: false, defaultValueSql: "'unknown'::character varying"),
                    data = table.Column<byte[]>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    research_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ссылка на исследование."),
                    type_id = table.Column<int>(nullable: true),
                    version = table.Column<int>(nullable: false, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_research", x => x.id);
                    table.ForeignKey(
                        name: "fk_file_research_research",
                        column: x => x.research_id,
                        principalTable: "research",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_file_research_type",
                        column: x => x.type_id,
                        principalTable: "type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Файлы исследований. Таблица наследована  от общей таблицы файлов.");

            migrationBuilder.CreateTable(
                name: "author_book",
                columns: table => new
                {
                    author_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ссылка на автора."),
                    book_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ссылка на книгу.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author_book", x => new { x.author_id, x.book_id });
                    table.ForeignKey(
                        name: "fk_author_book_author",
                        column: x => x.author_id,
                        principalTable: "author",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_author_book_book",
                        column: x => x.book_id,
                        principalTable: "book",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Npgsql:Comment", "Таблица, связывающая авторов и их книги.");

            migrationBuilder.CreateTable(
                name: "file_book",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"File_id_seq\"'::regclass)"),
                    book_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ссылка на книгу."),
                    content_type = table.Column<string>(nullable: false, defaultValueSql: "'unknown'::character varying"),
                    data = table.Column<byte[]>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    type_id = table.Column<int>(nullable: true),
                    version = table.Column<int>(nullable: false, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_book", x => x.id);
                    table.ForeignKey(
                        name: "fk_file_book_book",
                        column: x => x.book_id,
                        principalTable: "book",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_file_book_type",
                        column: x => x.type_id,
                        principalTable: "type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Файлы книиг. Таблица наследована  от общей таблицы файлов.");

            migrationBuilder.CreateTable(
                name: "research_book",
                columns: table => new
                {
                    research_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id исследования."),
                    book_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id книги.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_research_book", x => new { x.research_id, x.book_id });
                    table.ForeignKey(
                        name: "fk_research_book_book",
                        column: x => x.book_id,
                        principalTable: "book",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_research_book_research",
                        column: x => x.research_id,
                        principalTable: "research",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Таблица, связывающая исследования и книги.");

            migrationBuilder.CreateTable(
                name: "theme_book",
                columns: table => new
                {
                    theme_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id темы."),
                    book_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id книги.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_theme_book", x => new { x.theme_id, x.book_id });
                    table.ForeignKey(
                        name: "fk_theme_book_book",
                        column: x => x.book_id,
                        principalTable: "book",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_theme_book_theme",
                        column: x => x.theme_id,
                        principalTable: "theme",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Таблица, связывающая книги и темы.");

            migrationBuilder.CreateTable(
                name: "article",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"Article_id_seq\"'::regclass)")
                        .Annotation("Npgsql:Comment", "Уникальный внутренний идентификатор."),
                    bibliography = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Название статьи в виде, пригодном для списка литературы по ГОСТ 7.1-2003."),
                    city_id = table.Column<int>(nullable: true)
                        .Annotation("Npgsql:Comment", "id города, где проходила конференция."),
                    conference_end_date = table.Column<DateTime>(type: "date", nullable: true)
                        .Annotation("Npgsql:Comment", "Дата окончания конференции."),
                    conference_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id конференции."),
                    conference_number = table.Column<int>(nullable: true)
                        .Annotation("Npgsql:Comment", "В который раз проводится конференция."),
                    conference_start_date = table.Column<DateTime>(type: "date", nullable: true)
                        .Annotation("Npgsql:Comment", "Дата начала конференции."),
                    name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Название статьи."),
                    page = table.Column<int>(nullable: true)
                        .Annotation("Npgsql:Comment", "Страница на которой начинается статья."),
                    pages = table.Column<int>(nullable: true)
                        .Annotation("Npgsql:Comment", "Количество страниц в статье."),
                    publication_date = table.Column<DateTime>(type: "date", nullable: true)
                        .Annotation("Npgsql:Comment", "Дата публикации."),
                    supervisor_id = table.Column<int>(nullable: true)
                        .Annotation("Npgsql:Comment", "Научный руководитель, указанный в статье."),
                    volume = table.Column<int>(nullable: true)
                        .Annotation("Npgsql:Comment", "Номер тома."),
                    year = table.Column<int>(nullable: true)
                        .Annotation("Npgsql:Comment", "Год публикации.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article", x => x.id);
                    table.ForeignKey(
                        name: "fk_article_city",
                        column: x => x.city_id,
                        principalTable: "city",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_article_conference",
                        column: x => x.conference_id,
                        principalTable: "conference",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_article_author_supervisor_id",
                        column: x => x.supervisor_id,
                        principalTable: "author",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Статьи и публикации.");

            migrationBuilder.CreateTable(
                name: "article_article",
                columns: table => new
                {
                    host_article_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Статья, которая ссылается на другую."),
                    referenced_article_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Статья, используемая в списке литературы.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article_article", x => new { x.host_article_id, x.referenced_article_id });
                    table.ForeignKey(
                        name: "fk_host_article",
                        column: x => x.host_article_id,
                        principalTable: "article",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_referenced_article",
                        column: x => x.referenced_article_id,
                        principalTable: "article",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Ссылки статей на другие статьи в списке литературы.");

            migrationBuilder.CreateTable(
                name: "article_book",
                columns: table => new
                {
                    article_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id статьи."),
                    book_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id книги.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article_book", x => new { x.article_id, x.book_id });
                    table.ForeignKey(
                        name: "fk_article_book_article",
                        column: x => x.article_id,
                        principalTable: "article",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_article_book_book",
                        column: x => x.book_id,
                        principalTable: "book",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Таблица книг, используемых в списках литературы статей.");

            migrationBuilder.CreateTable(
                name: "article_keyword",
                columns: table => new
                {
                    article_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id статьи."),
                    keyword_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id ключевого слова.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article_keyword", x => new { x.article_id, x.keyword_id });
                    table.ForeignKey(
                        name: "fk_aricle_keyword_article",
                        column: x => x.article_id,
                        principalTable: "article",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_article_keyword_keyword",
                        column: x => x.keyword_id,
                        principalTable: "keyword",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Промежуточная таблица связывающая статьи и ключевые слова.");

            migrationBuilder.CreateTable(
                name: "author_article",
                columns: table => new
                {
                    author_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id автора."),
                    article_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id статьи.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author_article", x => new { x.author_id, x.article_id });
                    table.ForeignKey(
                        name: "fk_author_article_article",
                        column: x => x.article_id,
                        principalTable: "article",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_author_article_author",
                        column: x => x.author_id,
                        principalTable: "author",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Таблица, связывающая статьи и их авторов.");

            migrationBuilder.CreateTable(
                name: "file_article",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, defaultValueSql: "nextval('\"File_id_seq\"'::regclass)"),
                    article_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ссылка на статью."),
                    content_type = table.Column<string>(nullable: false, defaultValueSql: "'unknown'::character varying"),
                    data = table.Column<byte[]>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    type_id = table.Column<int>(nullable: true),
                    version = table.Column<int>(nullable: false, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_article", x => x.id);
                    table.ForeignKey(
                        name: "fk_file_article_article",
                        column: x => x.article_id,
                        principalTable: "article",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_file_article_type",
                        column: x => x.type_id,
                        principalTable: "type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Файлы статей. Таблица наследована от общей таблицы файлов.");

            migrationBuilder.CreateTable(
                name: "research_article",
                columns: table => new
                {
                    research_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id исследования."),
                    article_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id статьи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_research_article", x => new { x.research_id, x.article_id });
                    table.ForeignKey(
                        name: "fk_research_article_article",
                        column: x => x.article_id,
                        principalTable: "article",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_research_article_research",
                        column: x => x.research_id,
                        principalTable: "research",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Таблица связывающая статьи и используемые в них исследования.");

            migrationBuilder.CreateTable(
                name: "theme_article",
                columns: table => new
                {
                    theme_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id темы."),
                    article_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "id статьи.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_theme_article", x => new { x.theme_id, x.article_id });
                    table.ForeignKey(
                        name: "fk_theme_article_article",
                        column: x => x.article_id,
                        principalTable: "article",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_theme_article_theme",
                        column: x => x.theme_id,
                        principalTable: "theme",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Таблица тем той или иной статьи.");

            migrationBuilder.CreateIndex(
                name: "IX_article_city_id",
                table: "article",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_article_conference_id",
                table: "article",
                column: "conference_id");

            migrationBuilder.CreateIndex(
                name: "fki_article_author_supervisor_id",
                table: "article",
                column: "supervisor_id");

            migrationBuilder.CreateIndex(
                name: "uk_article",
                table: "article",
                columns: new[] { "name", "city_id", "conference_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_article_article_referenced_article_id",
                table: "article_article",
                column: "referenced_article_id");

            migrationBuilder.CreateIndex(
                name: "IX_article_book_book_id",
                table: "article_book",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_article_keyword_keyword_id",
                table: "article_keyword",
                column: "keyword_id");

            migrationBuilder.CreateIndex(
                name: "IX_author_article_article_id",
                table: "author_article",
                column: "article_id");

            migrationBuilder.CreateIndex(
                name: "IX_author_book_book_id",
                table: "author_book",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_author_keyword_keyword_id",
                table: "author_keyword",
                column: "keyword_id");

            migrationBuilder.CreateIndex(
                name: "IX_book_book_type_id",
                table: "book",
                column: "book_type_id");

            migrationBuilder.CreateIndex(
                name: "uk_book_type_name",
                table: "book_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_city_name",
                table: "city",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fki_scale",
                table: "conference",
                column: "scale_id");

            migrationBuilder.CreateIndex(
                name: "uk_conference",
                table: "conference",
                columns: new[] { "name", "place" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_discipline_name",
                table: "discipline",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_discipline_author_author_id",
                table: "discipline_author",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_file_type_version",
                table: "file",
                columns: new[] { "type_id", "version" });

            migrationBuilder.CreateIndex(
                name: "IX_file_article_article_id",
                table: "file_article",
                column: "article_id");

            migrationBuilder.CreateIndex(
                name: "ix_file_article_type_article",
                table: "file_article",
                columns: new[] { "type_id", "article_id" });

            migrationBuilder.CreateIndex(
                name: "ix_file_article_type_version",
                table: "file_article",
                columns: new[] { "type_id", "version" });

            migrationBuilder.CreateIndex(
                name: "ix_file_article_type_id_version_article",
                table: "file_article",
                columns: new[] { "type_id", "version", "article_id" });

            migrationBuilder.CreateIndex(
                name: "IX_file_book_book_id",
                table: "file_book",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "ix_file_book_type_book",
                table: "file_book",
                columns: new[] { "type_id", "book_id" });

            migrationBuilder.CreateIndex(
                name: "ix_file_book_type_version",
                table: "file_book",
                columns: new[] { "type_id", "version" });

            migrationBuilder.CreateIndex(
                name: "ix_file_book_type_id_version_book",
                table: "file_book",
                columns: new[] { "type_id", "version", "book_id" });

            migrationBuilder.CreateIndex(
                name: "IX_file_research_research_id",
                table: "file_research",
                column: "research_id");

            migrationBuilder.CreateIndex(
                name: "ix_file_research_type_research",
                table: "file_research",
                columns: new[] { "type_id", "research_id" });

            migrationBuilder.CreateIndex(
                name: "ix_file_research_type_version",
                table: "file_research",
                columns: new[] { "type_id", "version" });

            migrationBuilder.CreateIndex(
                name: "ix_file_research_type_id_version_research",
                table: "file_research",
                columns: new[] { "type_id", "version", "research_id" });

            migrationBuilder.CreateIndex(
                name: "uk_keyword",
                table: "keyword",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_research_article_article_id",
                table: "research_article",
                column: "article_id");

            migrationBuilder.CreateIndex(
                name: "IX_research_author_research_id",
                table: "research_author",
                column: "research_id");

            migrationBuilder.CreateIndex(
                name: "IX_research_book_book_id",
                table: "research_book",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_research_theme_theme_id",
                table: "research_theme",
                column: "theme_id");

            migrationBuilder.CreateIndex(
                name: "uk_scale",
                table: "scale",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_theme_name",
                table: "theme",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_theme_article_article_id",
                table: "theme_article",
                column: "article_id");

            migrationBuilder.CreateIndex(
                name: "fki_theme_author_author",
                table: "theme_author",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_theme_book_book_id",
                table: "theme_book",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "uk_type",
                table: "type",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_article");

            migrationBuilder.DropTable(
                name: "article_book");

            migrationBuilder.DropTable(
                name: "article_keyword");

            migrationBuilder.DropTable(
                name: "author_article");

            migrationBuilder.DropTable(
                name: "author_book");

            migrationBuilder.DropTable(
                name: "author_keyword");

            migrationBuilder.DropTable(
                name: "discipline_author");

            migrationBuilder.DropTable(
                name: "draft");

            migrationBuilder.DropTable(
                name: "file");

            migrationBuilder.DropTable(
                name: "file_article");

            migrationBuilder.DropTable(
                name: "file_book");

            migrationBuilder.DropTable(
                name: "file_research");

            migrationBuilder.DropTable(
                name: "hypothesis");

            migrationBuilder.DropTable(
                name: "research_article");

            migrationBuilder.DropTable(
                name: "research_author");

            migrationBuilder.DropTable(
                name: "research_book");

            migrationBuilder.DropTable(
                name: "research_theme");

            migrationBuilder.DropTable(
                name: "theme_article");

            migrationBuilder.DropTable(
                name: "theme_author");

            migrationBuilder.DropTable(
                name: "theme_book");

            migrationBuilder.DropTable(
                name: "keyword");

            migrationBuilder.DropTable(
                name: "discipline");

            migrationBuilder.DropTable(
                name: "type");

            migrationBuilder.DropTable(
                name: "research");

            migrationBuilder.DropTable(
                name: "article");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "theme");

            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropTable(
                name: "conference");

            migrationBuilder.DropTable(
                name: "author");

            migrationBuilder.DropTable(
                name: "book_type");

            migrationBuilder.DropTable(
                name: "scale");

            migrationBuilder.DropSequence(
                name: "Article_id_seq");

            migrationBuilder.DropSequence(
                name: "Author_id_seq");

            migrationBuilder.DropSequence(
                name: "Book_id_seq");

            migrationBuilder.DropSequence(
                name: "Book_Type_id_seq");

            migrationBuilder.DropSequence(
                name: "City_id_seq");

            migrationBuilder.DropSequence(
                name: "Conference_id_seq");

            migrationBuilder.DropSequence(
                name: "Discipline_id_seq");

            migrationBuilder.DropSequence(
                name: "Draft_id_seq");

            migrationBuilder.DropSequence(
                name: "File_id_seq");

            migrationBuilder.DropSequence(
                name: "Hypothesis_id_seq");

            migrationBuilder.DropSequence(
                name: "Keyword_id_seq");

            migrationBuilder.DropSequence(
                name: "Research_id_seq");

            migrationBuilder.DropSequence(
                name: "Scale_id_seq");

            migrationBuilder.DropSequence(
                name: "Theme_id_seq");

            migrationBuilder.DropSequence(
                name: "Type_id_seq");
        }
    }
}
