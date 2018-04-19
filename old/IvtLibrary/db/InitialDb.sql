--13.01.2015 10:52:54
BEGIN;

COMMENT ON DATABASE "Library" IS 'База данных для хранени статей, книг, научных публикаций и экспериментов.';

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';

CREATE EXTENSION IF NOT EXISTS plv8 WITH SCHEMA pg_catalog;

COMMENT ON EXTENSION plv8 IS 'PL/JavaScript (v8) trusted procedural language';

CREATE FUNCTION copy_constraints(src text, dst text) RETURNS integer
    LANGUAGE plpgsql IMMUTABLE
    AS $$
begin
  return copy_constraints(src::regclass::oid, dst::regclass::oid);
end;
$$;

CREATE FUNCTION copy_constraints(srcoid oid, dstoid oid) RETURNS integer
    LANGUAGE plpgsql
    AS $$
declare
  i int4 := 0;
  constrs record;
  srctable text;
  dsttable text;
begin
  srctable = srcoid::regclass;
  dsttable = dstoid::regclass;
  for constrs in
  select conname as name, pg_get_constraintdef(oid) as definition
  from pg_constraint where conrelid = srcoid loop
    begin
    execute 'alter table ' || dsttable
      || ' add constraint '
      || replace(replace(constrs.name, srctable, dsttable),'.','_')
      || ' ' || constrs.definition;
    i = i + 1;
    exception
      when duplicate_table then
    end;
  end loop;
  return i;
exception when undefined_table then
  return null;
end;
$$;

CREATE FUNCTION copy_indexes(src text, dst text) RETURNS integer
    LANGUAGE plpgsql IMMUTABLE
    AS $$
begin
  return copy_indexes(src::regclass::oid, dst::regclass::oid);
end;
$$;

CREATE FUNCTION copy_indexes(srcoid oid, dstoid oid) RETURNS integer
    LANGUAGE plpgsql
    AS $$
declare
  i int4 := 0;
  indexes record;
  srctable text;
  dsttable text;
  script text;
begin
  srctable = srcoid::regclass;
  dsttable = dstoid::regclass;
  for indexes in
  select c.relname as name, pg_get_indexdef(idx.indexrelid) as definition
  from pg_index idx, pg_class c where idx.indrelid = srcoid and c.oid = idx.indexrelid loop
    script = replace (indexes.definition, ' INDEX '
      || indexes.name, ' INDEX '
      || replace(replace(indexes.name, srctable, dsttable),'.','_'));
    script = replace (script, ' ON ' || srctable, ' ON ' || dsttable);
    begin
      execute script;
      i = i + 1;
    exception
      when duplicate_table then
    end;
  end loop;
  return i;
exception when undefined_table then
  return null;
end;
$$;

CREATE FUNCTION copy_triggers(src text, dst text) RETURNS integer
    LANGUAGE plpgsql IMMUTABLE
    AS $$
begin
  return copy_triggers(src::regclass::oid, dst::regclass::oid);
end;
$$;

CREATE FUNCTION copy_triggers(srcoid oid, dstoid oid) RETURNS integer
    LANGUAGE plpgsql
    AS $$
declare
  i int4 := 0;
  triggers record;
  srctable text;
  dsttable text;
  script text = '';
begin
  srctable = srcoid::regclass;
  dsttable = dstoid::regclass;
  for triggers in
   select tgname as name, pg_get_triggerdef(oid) as definition
   from pg_trigger where tgrelid = srcoid loop
    script =
    replace (triggers.definition, ' TRIGGER '
      || triggers.name, ' TRIGGER '
      || replace(replace(triggers.name, srctable, dsttable),'.','_'));
    script = replace (script, ' ON ' || srctable, ' ON ' || dsttable);
    begin
      execute script;
      i = i + 1;
    exception
      when duplicate_table then
    end;
  end loop;
  return i;
exception when undefined_table then
  return null;
end;
$$;

CREATE FUNCTION seq_next_value(seq_name text) RETURNS bigint
    LANGUAGE plpgsql
    AS $$BEGIN
	return nextval(seq_name::regclass);
END;$$;

COMMENT ON FUNCTION seq_next_value(seq_name text) IS 'Функция возвращающая следующее значение указанной последовательности.';

CREATE FUNCTION trigger_article_conference_date_validation() RETURNS trigger
    LANGUAGE plpgsql
    AS $$BEGIN
	IF((NEW.conference_end_date IS NOT NULL)AND(NEW.conference_start_date IS NOT NULL)AND(NEW.conference_start_date > NEW.conference_end_date)) THEN
		RAISE EXCEPTION 'Дата % начала конференции не может быть позже даты окончания конференции %', NEW.conference_start_date, NEW.conference_end_date;
	END IF;
	IF((NEW.publication_date IS NOT NULL)AND(NEW.conference_start_date IS NOT NULL)AND(NEW.conference_start_date > NEW.publication_date)) THEN
		RAISE EXCEPTION 'Дата % публикации не может быть раньше даты начала конференции %', NEW.publication_date, NEW.conference_start_date;
	END IF;
	RETURN NEW;
	
END;$$;

COMMENT ON FUNCTION trigger_article_conference_date_validation() IS 'Функция, проверяющая, что дата окончания конференции позже даты начала конференции.';

SET default_tablespace = '';

SET default_with_oids = false;

CREATE TABLE "Article" (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    city_id integer,
    conference_id integer NOT NULL,
    year integer,
    bibliography character varying(255),
    supervisor_id integer,
    conference_start_date date,
    conference_end_date date,
    publication_date date,
    pages integer,
    page integer,
    volume integer,
    conference_number integer
);

COMMENT ON TABLE "Article" IS 'Статьи и публикации.';

COMMENT ON COLUMN "Article".id IS 'Уникальный внутренний идентификатор.';

COMMENT ON COLUMN "Article".name IS 'Название статьи.';

COMMENT ON COLUMN "Article".city_id IS 'id города, где проходила конференция.';

COMMENT ON COLUMN "Article".conference_id IS 'id конференции.';

COMMENT ON COLUMN "Article".year IS 'Год публикации.';

COMMENT ON COLUMN "Article".bibliography IS 'Название статьи в виде, пригодном для списка литературы по ГОСТ 7.1-2003.';

COMMENT ON COLUMN "Article".supervisor_id IS 'Научный руководитель, указанный в статье.';

COMMENT ON COLUMN "Article".conference_start_date IS 'Дата начала конференции.';

COMMENT ON COLUMN "Article".conference_end_date IS 'Дата окончания конференции.';

COMMENT ON COLUMN "Article".publication_date IS 'Дата публикации.';

COMMENT ON COLUMN "Article".pages IS 'Количество страниц в статье.';

COMMENT ON COLUMN "Article".page IS 'Страница на которой начинается статья.';

COMMENT ON COLUMN "Article".volume IS 'Номер тома.';

COMMENT ON COLUMN "Article".conference_number IS 'В который раз проводится конференция.';

CREATE TABLE "Article_Article" (
    host_article_id integer NOT NULL,
    referenced_article_id integer NOT NULL
);

COMMENT ON TABLE "Article_Article" IS 'Ссылки статей на другие статьи в списке литературы.';

COMMENT ON COLUMN "Article_Article".host_article_id IS 'Статья, которая ссылается на другую.';

COMMENT ON COLUMN "Article_Article".referenced_article_id IS 'Статья, используемая в списке литературы.';

CREATE TABLE "Article_Book" (
    article_id integer NOT NULL,
    book_id integer NOT NULL
);

COMMENT ON TABLE "Article_Book" IS 'Таблица книг, используемых в списках литературы статей.';

COMMENT ON COLUMN "Article_Book".article_id IS 'id статьи.';

COMMENT ON COLUMN "Article_Book".book_id IS 'id книги.';

CREATE TABLE "Article_Keyword" (
    article_id integer NOT NULL,
    keyword_id integer NOT NULL
);

COMMENT ON TABLE "Article_Keyword" IS 'Промежуточная таблица связывающая статьи и ключевые слова.';

COMMENT ON COLUMN "Article_Keyword".article_id IS 'id статьи.';

COMMENT ON COLUMN "Article_Keyword".keyword_id IS 'id ключевого слова.';

CREATE SEQUENCE "Article_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "Article_id_seq" OWNED BY "Article".id;

CREATE TABLE "Author" (
    id integer NOT NULL,
    first_name character varying(60),
    middle_name character varying(60),
    last_name character varying(60) NOT NULL
);

COMMENT ON TABLE "Author" IS 'Таблица авторов, статей, книг, экспериментов и пр.';

COMMENT ON COLUMN "Author".id IS 'Уникальный внутренний идентификатор.';

COMMENT ON COLUMN "Author".first_name IS 'Имя автора.';

COMMENT ON COLUMN "Author".middle_name IS 'Отчество автора.';

COMMENT ON COLUMN "Author".last_name IS 'Фамилимя автора.';

CREATE TABLE "Author_Article" (
    author_id integer NOT NULL,
    article_id integer NOT NULL
);

COMMENT ON TABLE "Author_Article" IS 'Таблица, связывающая статьи и их авторов.';

COMMENT ON COLUMN "Author_Article".author_id IS 'id автора.';

COMMENT ON COLUMN "Author_Article".article_id IS 'id статьи.';

CREATE TABLE "Author_Book" (
    author_id integer NOT NULL,
    book_id integer NOT NULL
);

COMMENT ON TABLE "Author_Book" IS 'Таблица, связывающая авторов и их книги.';

COMMENT ON COLUMN "Author_Book".author_id IS 'Ссылка на автора.';

COMMENT ON COLUMN "Author_Book".book_id IS 'Ссылка на книгу.';

CREATE TABLE "Author_Keyword" (
    author_id integer NOT NULL,
    keyword_id integer NOT NULL
);

COMMENT ON TABLE "Author_Keyword" IS 'Промежуточная таблица, связывающая авторов и ключевые слова.';

COMMENT ON COLUMN "Author_Keyword".author_id IS 'id автора.';

COMMENT ON COLUMN "Author_Keyword".keyword_id IS 'id ключевого слова.';

CREATE SEQUENCE "Author_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "Author_id_seq" OWNED BY "Author".id;

CREATE TABLE "Book" (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    year integer,
    publisher character varying(255),
    volume integer,
    bibliography character varying(255),
    book_type_id integer DEFAULT 1 NOT NULL
);

COMMENT ON TABLE "Book" IS 'Книги.';

COMMENT ON COLUMN "Book".id IS 'Уникальный внутренний идентификатор.';

COMMENT ON COLUMN "Book".name IS 'Название книги.';

COMMENT ON COLUMN "Book".year IS 'Год издания книги.';

COMMENT ON COLUMN "Book".publisher IS 'Издатель.';

COMMENT ON COLUMN "Book".volume IS 'Количество страниц.';

COMMENT ON COLUMN "Book".bibliography IS 'Название книги в виде, пригодном для списка литературы по ГОСТ 7.1-2003.';

COMMENT ON COLUMN "Book".book_type_id IS 'Ссылка на тип книги.';

CREATE TABLE "BookType" (
    id integer NOT NULL,
    name character varying(255) NOT NULL
);

COMMENT ON TABLE "BookType" IS 'Таблица с типами книг.';

COMMENT ON COLUMN "BookType".id IS 'Уникальный внутренний идентификатор.';

COMMENT ON COLUMN "BookType".name IS 'Название типа книги.';

CREATE SEQUENCE "Book_Type_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "Book_Type_id_seq" OWNED BY "BookType".id;

CREATE SEQUENCE "Book_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "Book_id_seq" OWNED BY "Book".id;

CREATE TABLE "City" (
    id integer NOT NULL,
    name character varying(100) NOT NULL
);

COMMENT ON TABLE "City" IS 'Города, в которых проводятся конференции.';

COMMENT ON COLUMN "City".id IS 'Уникальный внутренний идентификатор.';

COMMENT ON COLUMN "City".name IS 'Название города.';

CREATE SEQUENCE "City_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "City_id_seq" OWNED BY "City".id;

CREATE TABLE "Conference" (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    place character varying(255) NOT NULL,
    full_name character varying(255),
    scale_id integer
);

COMMENT ON TABLE "Conference" IS 'Конференции.';

COMMENT ON COLUMN "Conference".id IS 'Уникальный идентификатор.';

COMMENT ON COLUMN "Conference".name IS 'Название конференции.';

COMMENT ON COLUMN "Conference".place IS 'Место проведения. (университет, ВУЗ)';

COMMENT ON COLUMN "Conference".full_name IS 'Полное название конференции, включая место её проведения.';

COMMENT ON COLUMN "Conference".scale_id IS 'id масштаба конференции.';

CREATE SEQUENCE "Conference_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "Conference_id_seq" OWNED BY "Conference".id;

CREATE TABLE "Discipline" (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    semester integer NOT NULL
);

COMMENT ON TABLE "Discipline" IS 'Таблица со списком предметов.';

COMMENT ON COLUMN "Discipline".id IS 'Уникальный внутренний идентификатор.';

COMMENT ON COLUMN "Discipline".name IS 'Название дисциплины.';

COMMENT ON COLUMN "Discipline".semester IS 'Семестр в котором ведется дисциплина.';

CREATE TABLE "Discipline_Author" (
    discipline_id integer NOT NULL,
    author_id integer NOT NULL
);

COMMENT ON TABLE "Discipline_Author" IS 'Промежуточная таблица связывающая преподавателей и предметы.';

COMMENT ON COLUMN "Discipline_Author".discipline_id IS 'id дисциплины.';

COMMENT ON COLUMN "Discipline_Author".author_id IS 'id автора.';

CREATE SEQUENCE "Discipline_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "Discipline_id_seq" OWNED BY "Discipline".id;

CREATE TABLE "Draft" (
    id bigint NOT NULL,
    title character varying(255),
    content text NOT NULL,
    creation_date timestamp with time zone DEFAULT now() NOT NULL
);

COMMENT ON TABLE "Draft" IS 'Записки, мысли и черновики.';

COMMENT ON COLUMN "Draft".id IS 'Уникальный внутренний идентификатор.';

COMMENT ON COLUMN "Draft".title IS 'Название, заголовок.';

COMMENT ON COLUMN "Draft".content IS 'Содержимое записки.';

COMMENT ON COLUMN "Draft".creation_date IS 'Дата создания записи.';

CREATE SEQUENCE "Draft_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "Draft_id_seq" OWNED BY "Draft".id;

CREATE TABLE "File" (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    content_type character varying(100) DEFAULT 'unknown'::character varying NOT NULL,
    data bytea NOT NULL,
    type_id integer,
    version integer DEFAULT 1 NOT NULL
);

COMMENT ON TABLE "File" IS 'Таблица со всеми файлами.';

COMMENT ON COLUMN "File".id IS 'Уникальный идентификатор файла.';

COMMENT ON COLUMN "File".name IS 'Название файла в ФС.';

COMMENT ON COLUMN "File".content_type IS 'Формат файла.';

COMMENT ON COLUMN "File".data IS 'Файл.';

CREATE TABLE "FileArticle" (
    article_id integer NOT NULL
)
INHERITS ("File");

COMMENT ON TABLE "FileArticle" IS 'Файлы статей. Таблица наследована от общей таблицы файлов.';

COMMENT ON COLUMN "FileArticle".article_id IS 'Ссылка на статью.';

CREATE TABLE "FileBook" (
    book_id integer NOT NULL
)
INHERITS ("File");

COMMENT ON TABLE "FileBook" IS 'Файлы книиг. Таблица наследована  от общей таблицы файлов.';

COMMENT ON COLUMN "FileBook".book_id IS 'Ссылка на книгу.';

CREATE TABLE "FileResearch" (
    research_id integer NOT NULL
)
INHERITS ("File");

COMMENT ON TABLE "FileResearch" IS 'Файлы исследований. Таблица наследована  от общей таблицы файлов.';

COMMENT ON COLUMN "FileResearch".research_id IS 'Ссылка на исследование.';

CREATE SEQUENCE "File_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "File_id_seq" OWNED BY "File".id;

CREATE TABLE "Hypothesis" (
    id bigint NOT NULL,
    name character varying(255),
    content text NOT NULL,
    explanation text
);

COMMENT ON TABLE "Hypothesis" IS 'Гипотезы.';

COMMENT ON COLUMN "Hypothesis".id IS 'Уникальный внутренний идентификатор.';

COMMENT ON COLUMN "Hypothesis".name IS 'Название гипотезы.';

COMMENT ON COLUMN "Hypothesis".content IS 'Текст гипотезы.';

COMMENT ON COLUMN "Hypothesis".explanation IS 'Пояснения к гипотезе.';

CREATE SEQUENCE "Hypothesis_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "Hypothesis_id_seq" OWNED BY "Hypothesis".id;

CREATE TABLE "Keyword" (
    id integer NOT NULL,
    name character varying(255) NOT NULL
);

COMMENT ON TABLE "Keyword" IS 'Таблица с ключевыми словами.';

COMMENT ON COLUMN "Keyword".id IS 'Уникальный внутренний идентификатор.';

COMMENT ON COLUMN "Keyword".name IS 'Ключевое слово или фраза.';

CREATE SEQUENCE "Keyword_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "Keyword_id_seq" OWNED BY "Keyword".id;

CREATE TABLE "Research" (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    description character varying(255),
    goal text,
    tasks text,
    progress smallint
);

COMMENT ON TABLE "Research" IS 'Исследования.';

COMMENT ON COLUMN "Research".id IS 'Уникальный внутренний идентификатор таблицы.';

COMMENT ON COLUMN "Research".name IS 'Название эксперимента.';

COMMENT ON COLUMN "Research".description IS 'Описание эксперимента.';

CREATE TABLE "Research_Article" (
    research_id integer NOT NULL,
    article_id integer NOT NULL
);

COMMENT ON TABLE "Research_Article" IS 'Таблица связывающая статьи и используемые в них исследования.';

COMMENT ON COLUMN "Research_Article".research_id IS 'id исследования.';

COMMENT ON COLUMN "Research_Article".article_id IS 'id статьи';

CREATE TABLE "Research_Author" (
    author_id integer NOT NULL,
    research_id integer NOT NULL
);

COMMENT ON TABLE "Research_Author" IS 'Таблица, связывающая автора и их исследования.';

COMMENT ON COLUMN "Research_Author".author_id IS 'id автора.';

COMMENT ON COLUMN "Research_Author".research_id IS 'id исследования.';

CREATE TABLE "Research_Book" (
    research_id integer NOT NULL,
    book_id integer NOT NULL
);

COMMENT ON TABLE "Research_Book" IS 'Таблица, связывающая исследования и книги.';

COMMENT ON COLUMN "Research_Book".research_id IS 'id исследования.';

COMMENT ON COLUMN "Research_Book".book_id IS 'id книги.';

CREATE TABLE "Research_Theme" (
    research_id integer NOT NULL,
    theme_id integer NOT NULL
);

COMMENT ON TABLE "Research_Theme" IS 'Таблица, связывающая исследования и их тематику.';

COMMENT ON COLUMN "Research_Theme".research_id IS 'id исследования.';

COMMENT ON COLUMN "Research_Theme".theme_id IS 'id темы.';

CREATE SEQUENCE "Research_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "Research_id_seq" OWNED BY "Research".id;

CREATE TABLE "Scale" (
    id integer NOT NULL,
    name character varying(100) NOT NULL
);

COMMENT ON TABLE "Scale" IS 'Таблица с перечнем масштабов конференций.';

COMMENT ON COLUMN "Scale".id IS 'Уникальный внутренний идентификатор.';

COMMENT ON COLUMN "Scale".name IS 'Масштаб (Городская, Международная, Региональная и т. д.).';

CREATE SEQUENCE "Scale_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "Scale_id_seq" OWNED BY "Scale".id;

CREATE TABLE "Theme" (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    description character varying(255)
);

COMMENT ON TABLE "Theme" IS 'Тематики научных исследований, публикаций и т. д.';

COMMENT ON COLUMN "Theme".id IS 'Уникальный внутренний идентификатор.';

COMMENT ON COLUMN "Theme".name IS 'Название темы.';

COMMENT ON COLUMN "Theme".description IS 'Описание темы.';

CREATE TABLE "Theme_Article" (
    theme_id integer NOT NULL,
    article_id integer NOT NULL
);

COMMENT ON TABLE "Theme_Article" IS 'Таблица тем той или иной статьи.';

COMMENT ON COLUMN "Theme_Article".theme_id IS 'id темы.';

COMMENT ON COLUMN "Theme_Article".article_id IS 'id статьи.';

CREATE TABLE "Theme_Author" (
    theme_id integer NOT NULL,
    author_id integer NOT NULL
);

COMMENT ON TABLE "Theme_Author" IS 'Таблица, связывающая авторов и темы их публикаций и работ.';

COMMENT ON COLUMN "Theme_Author".theme_id IS 'id темы.';

COMMENT ON COLUMN "Theme_Author".author_id IS 'id автора.';

CREATE TABLE "Theme_Book" (
    theme_id integer NOT NULL,
    book_id integer NOT NULL
);

COMMENT ON TABLE "Theme_Book" IS 'Таблица, связывающая книги и темы.';

COMMENT ON COLUMN "Theme_Book".theme_id IS 'id темы.';

COMMENT ON COLUMN "Theme_Book".book_id IS 'id книги.';

CREATE SEQUENCE "Theme_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "Theme_id_seq" OWNED BY "Theme".id;

CREATE TABLE "Type" (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    description character varying(512)
);

COMMENT ON TABLE "Type" IS 'Тип содержимого тех или иных элементов.';

COMMENT ON COLUMN "Type".id IS 'Уникальный внутренний идентификатор.';

COMMENT ON COLUMN "Type".name IS 'Название типа.';

COMMENT ON COLUMN "Type".description IS 'Описание типа.';

CREATE SEQUENCE "Type_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "Type_id_seq" OWNED BY "Type".id;

ALTER TABLE ONLY "Article" ALTER COLUMN id SET DEFAULT nextval('"Article_id_seq"'::regclass);

ALTER TABLE ONLY "Author" ALTER COLUMN id SET DEFAULT nextval('"Author_id_seq"'::regclass);

ALTER TABLE ONLY "Book" ALTER COLUMN id SET DEFAULT nextval('"Book_id_seq"'::regclass);

ALTER TABLE ONLY "BookType" ALTER COLUMN id SET DEFAULT nextval('"Book_Type_id_seq"'::regclass);

ALTER TABLE ONLY "City" ALTER COLUMN id SET DEFAULT nextval('"City_id_seq"'::regclass);

ALTER TABLE ONLY "Conference" ALTER COLUMN id SET DEFAULT nextval('"Conference_id_seq"'::regclass);

ALTER TABLE ONLY "Discipline" ALTER COLUMN id SET DEFAULT nextval('"Discipline_id_seq"'::regclass);

ALTER TABLE ONLY "Draft" ALTER COLUMN id SET DEFAULT nextval('"Draft_id_seq"'::regclass);

ALTER TABLE ONLY "File" ALTER COLUMN id SET DEFAULT nextval('"File_id_seq"'::regclass);

ALTER TABLE ONLY "FileArticle" ALTER COLUMN id SET DEFAULT nextval('"File_id_seq"'::regclass);

ALTER TABLE ONLY "FileArticle" ALTER COLUMN content_type SET DEFAULT 'unknown'::character varying;

ALTER TABLE ONLY "FileArticle" ALTER COLUMN version SET DEFAULT 1;

ALTER TABLE ONLY "FileBook" ALTER COLUMN id SET DEFAULT nextval('"File_id_seq"'::regclass);

ALTER TABLE ONLY "FileBook" ALTER COLUMN content_type SET DEFAULT 'unknown'::character varying;

ALTER TABLE ONLY "FileBook" ALTER COLUMN version SET DEFAULT 1;

ALTER TABLE ONLY "FileResearch" ALTER COLUMN id SET DEFAULT nextval('"File_id_seq"'::regclass);

ALTER TABLE ONLY "FileResearch" ALTER COLUMN content_type SET DEFAULT 'unknown'::character varying;

ALTER TABLE ONLY "FileResearch" ALTER COLUMN version SET DEFAULT 1;

ALTER TABLE ONLY "Hypothesis" ALTER COLUMN id SET DEFAULT nextval('"Hypothesis_id_seq"'::regclass);

ALTER TABLE ONLY "Keyword" ALTER COLUMN id SET DEFAULT nextval('"Keyword_id_seq"'::regclass);

ALTER TABLE ONLY "Research" ALTER COLUMN id SET DEFAULT nextval('"Research_id_seq"'::regclass);

ALTER TABLE ONLY "Scale" ALTER COLUMN id SET DEFAULT nextval('"Scale_id_seq"'::regclass);

ALTER TABLE ONLY "Theme" ALTER COLUMN id SET DEFAULT nextval('"Theme_id_seq"'::regclass);

ALTER TABLE ONLY "Type" ALTER COLUMN id SET DEFAULT nextval('"Type_id_seq"'::regclass);

ALTER TABLE ONLY "Article"
    ADD CONSTRAINT pk_article PRIMARY KEY (id);

ALTER TABLE ONLY "Article_Article"
    ADD CONSTRAINT pk_article_article PRIMARY KEY (host_article_id, referenced_article_id);

ALTER TABLE ONLY "Article_Book"
    ADD CONSTRAINT pk_article_book PRIMARY KEY (article_id, book_id);

ALTER TABLE ONLY "Article_Keyword"
    ADD CONSTRAINT pk_article_keyword PRIMARY KEY (article_id, keyword_id);

ALTER TABLE ONLY "Author"
    ADD CONSTRAINT pk_author PRIMARY KEY (id);

ALTER TABLE ONLY "Author_Article"
    ADD CONSTRAINT pk_author_article PRIMARY KEY (author_id, article_id);

ALTER TABLE ONLY "Author_Book"
    ADD CONSTRAINT pk_author_book PRIMARY KEY (author_id, book_id);

ALTER TABLE ONLY "Author_Keyword"
    ADD CONSTRAINT pk_author_keyword PRIMARY KEY (author_id, keyword_id);

ALTER TABLE ONLY "Book"
    ADD CONSTRAINT pk_book PRIMARY KEY (id);

ALTER TABLE ONLY "BookType"
    ADD CONSTRAINT pk_book_type PRIMARY KEY (id);

ALTER TABLE ONLY "City"
    ADD CONSTRAINT pk_city PRIMARY KEY (id);

ALTER TABLE ONLY "Conference"
    ADD CONSTRAINT pk_conference PRIMARY KEY (id);

ALTER TABLE ONLY "Discipline"
    ADD CONSTRAINT pk_discipline PRIMARY KEY (id);

ALTER TABLE ONLY "Discipline_Author"
    ADD CONSTRAINT pk_discipline_author PRIMARY KEY (discipline_id, author_id);

ALTER TABLE ONLY "Draft"
    ADD CONSTRAINT pk_draft PRIMARY KEY (id);

ALTER TABLE ONLY "File"
    ADD CONSTRAINT pk_file PRIMARY KEY (id);

ALTER TABLE ONLY "FileArticle"
    ADD CONSTRAINT pk_file_article PRIMARY KEY (id);

ALTER TABLE ONLY "FileBook"
    ADD CONSTRAINT pk_file_book PRIMARY KEY (id);

ALTER TABLE ONLY "FileResearch"
    ADD CONSTRAINT pk_file_research PRIMARY KEY (id);

ALTER TABLE ONLY "Hypothesis"
    ADD CONSTRAINT pk_hypothesis PRIMARY KEY (id);

ALTER TABLE ONLY "Keyword"
    ADD CONSTRAINT pk_keyword PRIMARY KEY (id);

ALTER TABLE ONLY "Research"
    ADD CONSTRAINT pk_research PRIMARY KEY (id);

ALTER TABLE ONLY "Research_Article"
    ADD CONSTRAINT pk_research_article PRIMARY KEY (research_id, article_id);

ALTER TABLE ONLY "Research_Author"
    ADD CONSTRAINT pk_research_author PRIMARY KEY (author_id, research_id);

ALTER TABLE ONLY "Research_Book"
    ADD CONSTRAINT pk_research_book PRIMARY KEY (research_id, book_id);

ALTER TABLE ONLY "Research_Theme"
    ADD CONSTRAINT pk_research_theme PRIMARY KEY (research_id, theme_id);

ALTER TABLE ONLY "Scale"
    ADD CONSTRAINT pk_scale PRIMARY KEY (id);

ALTER TABLE ONLY "Theme"
    ADD CONSTRAINT pk_theme PRIMARY KEY (id);

ALTER TABLE ONLY "Theme_Article"
    ADD CONSTRAINT pk_theme_article PRIMARY KEY (theme_id, article_id);

ALTER TABLE ONLY "Theme_Author"
    ADD CONSTRAINT pk_theme_author PRIMARY KEY (theme_id, author_id);

ALTER TABLE ONLY "Theme_Book"
    ADD CONSTRAINT pk_theme_book PRIMARY KEY (theme_id, book_id);

ALTER TABLE ONLY "Type"
    ADD CONSTRAINT pk_type PRIMARY KEY (id);

ALTER TABLE ONLY "Article"
    ADD CONSTRAINT uk_article UNIQUE (name, city_id, conference_id);

ALTER TABLE ONLY "BookType"
    ADD CONSTRAINT uk_book_type_name UNIQUE (name);

ALTER TABLE ONLY "City"
    ADD CONSTRAINT uk_city_name UNIQUE (name);

COMMENT ON CONSTRAINT uk_city_name ON "City" IS 'Название города должно быть уникальным.';

ALTER TABLE ONLY "Conference"
    ADD CONSTRAINT uk_conference UNIQUE (name, place);

ALTER TABLE ONLY "Discipline"
    ADD CONSTRAINT uk_discipline_name UNIQUE (name);

ALTER TABLE ONLY "Keyword"
    ADD CONSTRAINT uk_keyword UNIQUE (name);

ALTER TABLE ONLY "Scale"
    ADD CONSTRAINT uk_scale UNIQUE (name);

ALTER TABLE ONLY "Theme"
    ADD CONSTRAINT uk_theme_name UNIQUE (name);

ALTER TABLE ONLY "Type"
    ADD CONSTRAINT uk_type UNIQUE (name);

CREATE INDEX fki_article_author_supervisor_id ON "Article" USING btree (supervisor_id);

CREATE INDEX fki_scale ON "Conference" USING btree (scale_id);

CREATE INDEX fki_theme_author_author ON "Theme_Author" USING btree (author_id);

CREATE INDEX ix_file_article_type_article ON "FileArticle" USING btree (type_id, article_id);

CREATE INDEX ix_file_article_type_id_version_article ON "FileArticle" USING btree (type_id, version, article_id);

CREATE INDEX ix_file_article_type_version ON "FileArticle" USING btree (type_id, version);

CREATE INDEX ix_file_book_type_book ON "FileBook" USING btree (type_id, book_id);

CREATE INDEX ix_file_book_type_id_version_book ON "FileBook" USING btree (type_id, version, book_id);

CREATE INDEX ix_file_book_type_version ON "FileBook" USING btree (type_id, version);

CREATE INDEX ix_file_research_type_id_version_research ON "FileResearch" USING btree (type_id, version, research_id);

CREATE INDEX ix_file_research_type_research ON "FileResearch" USING btree (type_id, research_id);

CREATE INDEX ix_file_research_type_version ON "FileResearch" USING btree (type_id, version);

CREATE INDEX ix_file_type_version ON "File" USING btree (type_id, version);

CREATE TRIGGER tgiu_article_date_validation BEFORE INSERT OR UPDATE ON "Article" FOR EACH ROW EXECUTE PROCEDURE trigger_article_conference_date_validation();

COMMENT ON TRIGGER tgiu_article_date_validation ON "Article" IS 'Триггер, который проверяет валидность дат проведения конференции.';

ALTER TABLE ONLY "Article_Keyword"
    ADD CONSTRAINT fk_aricle_keyword_article FOREIGN KEY (article_id) REFERENCES "Article"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "Article"
    ADD CONSTRAINT fk_article_author_supervisor_id FOREIGN KEY (supervisor_id) REFERENCES "Author"(id);

COMMENT ON CONSTRAINT fk_article_author_supervisor_id ON "Article" IS 'Ссылка на научного руководителя.';

ALTER TABLE ONLY "Article_Book"
    ADD CONSTRAINT fk_article_book_article FOREIGN KEY (article_id) REFERENCES "Article"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "Article_Book"
    ADD CONSTRAINT fk_article_book_book FOREIGN KEY (book_id) REFERENCES "Book"(id);

ALTER TABLE ONLY "Article"
    ADD CONSTRAINT fk_article_city FOREIGN KEY (city_id) REFERENCES "City"(id);

ALTER TABLE ONLY "Article"
    ADD CONSTRAINT fk_article_conference FOREIGN KEY (conference_id) REFERENCES "Conference"(id);

ALTER TABLE ONLY "Article_Keyword"
    ADD CONSTRAINT fk_article_keyword_keyword FOREIGN KEY (keyword_id) REFERENCES "Keyword"(id);

ALTER TABLE ONLY "Author_Article"
    ADD CONSTRAINT fk_author_article_article FOREIGN KEY (article_id) REFERENCES "Article"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "Author_Article"
    ADD CONSTRAINT fk_author_article_author FOREIGN KEY (author_id) REFERENCES "Author"(id);

ALTER TABLE ONLY "Author_Book"
    ADD CONSTRAINT fk_author_book_author FOREIGN KEY (author_id) REFERENCES "Author"(id);

ALTER TABLE ONLY "Author_Book"
    ADD CONSTRAINT fk_author_book_book FOREIGN KEY (book_id) REFERENCES "Book"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "Author_Keyword"
    ADD CONSTRAINT fk_author_keyword_author FOREIGN KEY (author_id) REFERENCES "Author"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "Author_Keyword"
    ADD CONSTRAINT fk_author_keyword_keyword FOREIGN KEY (keyword_id) REFERENCES "Keyword"(id);

ALTER TABLE ONLY "Book"
    ADD CONSTRAINT fk_book_book_type FOREIGN KEY (book_type_id) REFERENCES "BookType"(id);

ALTER TABLE ONLY "Conference"
    ADD CONSTRAINT fk_conference_scale FOREIGN KEY (scale_id) REFERENCES "Scale"(id);

ALTER TABLE ONLY "Discipline_Author"
    ADD CONSTRAINT fk_discipline_author_author FOREIGN KEY (author_id) REFERENCES "Author"(id);

ALTER TABLE ONLY "Discipline_Author"
    ADD CONSTRAINT fk_discipline_author_discipline FOREIGN KEY (discipline_id) REFERENCES "Discipline"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "FileArticle"
    ADD CONSTRAINT fk_file_article_article FOREIGN KEY (article_id) REFERENCES "Article"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "FileArticle"
    ADD CONSTRAINT fk_file_article_type FOREIGN KEY (type_id) REFERENCES "Type"(id);

ALTER TABLE ONLY "FileBook"
    ADD CONSTRAINT fk_file_book_book FOREIGN KEY (book_id) REFERENCES "Book"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "FileBook"
    ADD CONSTRAINT fk_file_book_type FOREIGN KEY (type_id) REFERENCES "Type"(id);

ALTER TABLE ONLY "FileResearch"
    ADD CONSTRAINT fk_file_research_research FOREIGN KEY (research_id) REFERENCES "Research"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "FileResearch"
    ADD CONSTRAINT fk_file_research_type FOREIGN KEY (type_id) REFERENCES "Type"(id);

ALTER TABLE ONLY "File"
    ADD CONSTRAINT fk_file_type FOREIGN KEY (type_id) REFERENCES "Type"(id);

ALTER TABLE ONLY "Article_Article"
    ADD CONSTRAINT fk_host_article FOREIGN KEY (host_article_id) REFERENCES "Article"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "Article_Article"
    ADD CONSTRAINT fk_referenced_article FOREIGN KEY (referenced_article_id) REFERENCES "Article"(id);

ALTER TABLE ONLY "Research_Article"
    ADD CONSTRAINT fk_research_article_article FOREIGN KEY (article_id) REFERENCES "Article"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "Research_Article"
    ADD CONSTRAINT fk_research_article_research FOREIGN KEY (research_id) REFERENCES "Research"(id);

ALTER TABLE ONLY "Research_Author"
    ADD CONSTRAINT fk_research_author_author FOREIGN KEY (author_id) REFERENCES "Author"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "Research_Author"
    ADD CONSTRAINT fk_research_author_research FOREIGN KEY (research_id) REFERENCES "Research"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "Research_Book"
    ADD CONSTRAINT fk_research_book_book FOREIGN KEY (book_id) REFERENCES "Book"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "Research_Book"
    ADD CONSTRAINT fk_research_book_research FOREIGN KEY (research_id) REFERENCES "Research"(id);

ALTER TABLE ONLY "Research_Theme"
    ADD CONSTRAINT fk_research_theme_research FOREIGN KEY (research_id) REFERENCES "Research"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "Research_Theme"
    ADD CONSTRAINT fk_research_theme_theme FOREIGN KEY (theme_id) REFERENCES "Theme"(id);

ALTER TABLE ONLY "Theme_Article"
    ADD CONSTRAINT fk_theme_article_article FOREIGN KEY (article_id) REFERENCES "Article"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "Theme_Article"
    ADD CONSTRAINT fk_theme_article_theme FOREIGN KEY (theme_id) REFERENCES "Theme"(id);

ALTER TABLE ONLY "Theme_Author"
    ADD CONSTRAINT fk_theme_author_author FOREIGN KEY (author_id) REFERENCES "Author"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "Theme_Author"
    ADD CONSTRAINT fk_theme_author_theme FOREIGN KEY (theme_id) REFERENCES "Theme"(id);

ALTER TABLE ONLY "Theme_Book"
    ADD CONSTRAINT fk_theme_book_book FOREIGN KEY (book_id) REFERENCES "Book"(id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "Theme_Book"
    ADD CONSTRAINT fk_theme_book_theme FOREIGN KEY (theme_id) REFERENCES "Theme"(id);

COMMIT;
--13.01.2015 10:52:54
