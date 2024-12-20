using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
	public class Product
	{
		[Display(Name="ID")]
        public int ProductId { get; set; }
		[Required(ErrorMessage = "Gerekli Alan!!!")]
		[Display(Name = "İsim")]
		public string? Name { get; set; }
		[Required]
		[Range(0,100000)]
		[Display(Name = "Fiyat")]
		public decimal? Price { get; set; }
		[Display(Name = "Resim")]
		public string? Image {  get; set; }
		public bool IsActive {  get; set; }
		[Required]
		[Display(Name="kategori")]
		public int? CategoryId {  get; set; }
    }
}
