using System;
using System.ComponentModel.DataAnnotations;

namespace IvtLibrary
{
	[MetadataType(typeof (FatherDataAnnotations))]
    public partial class Father
    {
    }
	
    // ����� ����� ��� ��������� ������������
	public class FatherDataAnnotations
    {
		[Display(Name = "��������")]
        public string name { get; set; }
	}
	
	[MetadataType(typeof (AuthorDataAnnotations))]
    public partial class Author
    {
    }
	
	public class AuthorDataAnnotations
    {
        [Display(Name = "�������")]
        public string last_name { get; set; }

        [Display(Name = "���")]
        public string first_name { get; set; }
		
		[Display(Name = "��������")]
		public string middle_name {get; set; }
    
		[Display(Name = "����")]
		public string Theme {get; set; }
	}
	
	[MetadataType(typeof (ArticleDataAnnotations))]
	public partial class Article
	{
	}

    public class ArticleDataAnnotations : FatherDataAnnotations
    {
        [Display(Name = "�����")]
		public string Author { get; set; }

        [Display(Name = "����� �����������")]
        public string city_id { get; set; }
		
        [Display(Name = "�����������")]
        public string conference_id { get; set; }  
		
        [Display(Name = "��� �������")]
        [Range(1900, 2100)]
        public int year  { get; set; }
		
        [Display(Name = "������� ������������")]
        public string supervisor_id { get; set; }
		
        [Display(Name = "������ �����������")]
		[DataType(DataType.Date)]
		public DateTime conference_start_date { get; set; }
		
		[Display(Name = "��������� �����������")]
		[DataType(DataType.Date)]
		public DateTime conference_end_date { get; set; }
		
		[Display(Name = "���� ����������")]
		[DataType(DataType.Date)]
		public DateTime publication_date { get; set; }

        [Display(Name = "����")]
        public string Theme { get; set; }
    }
	
	[MetadataType(typeof (BookDataAnnotations))]
	public partial class Book
	{
	}
	
	public class BookDataAnnotations : FatherDataAnnotations
	{			
		[Display(Name = "�����")]
		public string Author { get; set; }
		
		[Display(Name = "��� �������")]
        [Range(1900, 2100)]
		public int year  { get; set; }
		
		[Display(Name = "������������")]
        public string publisher { get; set; }
		
		[Display(Name = "���������� �������")]
        [Range(0, 10000)]
		public int volume { get; set; }

        [Display(Name = "����")]
        public string Theme { get; set; }
						
	}
	
	[MetadataType(typeof (CityDataAnnotations))]
	public partial class City
	{
	}
	
	public class CityDataAnnotations : FatherDataAnnotations
	{
	}
	
	[MetadataType(typeof (ConferenceDataAnnotations))]
	public partial class Conference
	{
	}
	
	public class ConferenceDataAnnotations : FatherDataAnnotations
	{
		[Display(Name = "����� ����������")]
        public string place { get; set; }
		
		[Display(Name = "������ ��������")]
        public string full_name { get; set; }
		
		[Display(Name = "������ �����������")]
        public string scale_id { get; set; }
	}
	
	[MetadataType(typeof (DraftDataAnnotations))]
	public partial class Draft
	{
	}
	
	public class DraftDataAnnotations
	{
		[Display(Name = "�������� �������")]
        public string title { get; set; }
		
		[Display(Name = "����������")]
        public string content { get; set; }
		
		[Display(Name = "���� ��������")]
		[DataType(DataType.Date)]
		public DateTime creation_date { get; set; }
	}
	
	[MetadataType(typeof (FileDataAnnotations))]
	public partial class File
	{
	}
	
	public class FileDataAnnotations : FatherDataAnnotations
	{
		[Display(Name = "��� �����")]
        public string type_id { get; set; }
		
		[Display(Name = "������ �����")]
        [Range(0, 50)]
		public int version { get; set; }

        [Display(Name = "�������� ����")]
        public string data { get; set; }
	}
		
	[MetadataType(typeof (HypothesisDataAnnotations))]
	public partial class Hypothesis
	{
	}
	
	public class HypothesisDataAnnotations : FatherDataAnnotations
	{
		[Display(Name = "����������")]
        public string content { get; set; }
		
		[Display(Name = "����������")]
        public string explanation { get; set; }
	}
	
	[MetadataType(typeof (ResearchDataAnnotations))]
	public partial class Research
	{
	}
	
	public class ResearchDataAnnotations : FatherDataAnnotations
	{
		[Display(Name = "�����")]
		public string Author { get; set; }
		
		[Display(Name = "��������")]
        public string description { get; set; }
		
		[Display(Name = "����")]
		public string Theme {get; set; }
	}
	
	[MetadataType(typeof (ScaleDataAnnotations))]
	public partial class Scale
	{
		[Display(Name = "��������")]
        public string description { get; set; }
	}
	
	public class ScaleDataAnnotations : FatherDataAnnotations
	{
	}
	
	[MetadataType(typeof (ThemeDataAnnotations))]
	public partial class Theme
	{
	}
	
	public class ThemeDataAnnotations : FatherDataAnnotations
	{
		[Display(Name = "��������")]
        public string description { get; set; }
	}
	
	[MetadataType(typeof (TypeDataAnnotations))]
	public partial class Type
	{
	}
	
	public class TypeDataAnnotations : FatherDataAnnotations
	{
		[Display(Name = "��������")]
        public string description { get; set; }
	
	}
	
	
	
}