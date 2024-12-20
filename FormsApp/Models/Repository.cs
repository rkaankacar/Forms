namespace FormsApp.Models
{
	public class Repository
	{
		private static readonly List<Product> _products = new();
		private static readonly List<Category> _categories=new();
		private static readonly List<Category> _courses = new();
		static Repository()
		{
			_categories.Add(new Category {CategoryId=1,Name="Telefon" });
			_categories.Add(new Category { CategoryId = 2, Name = "Bilgisayar" });
			_products.Add(new Product { ProductId = 1, Name = "iphone 12", Price = 25000, Image = "ip12.jpg", IsActive = true, CategoryId = 1 }); 
			_products.Add(new Product { ProductId = 2, Name = "iphone 14", Price = 30000, Image = "ip14.jpg", IsActive = true, CategoryId = 1 });
			_products.Add(new Product { ProductId = 3, Name = "iphone 15", Price = 35000, Image = "ip15.jpg", IsActive = true, CategoryId = 1 });
			_products.Add(new Product { ProductId = 4, Name = "iphone 16", Price = 40000, Image = "ip16.jpg", IsActive = true, CategoryId = 1 });
			_products.Add(new Product { ProductId = 5, Name = "macbook air", Price = 56000, Image = "macair.jpg", IsActive = true, CategoryId = 2 });
			_products.Add(new Product { ProductId = 6, Name = "macbook pro", Price = 89000, Image = "macpro.jpg", IsActive = true, CategoryId = 2 });

		}
		public static List<Product> Product
		{
			get
			{
				return _products;
			}
		}

		public static void Createproduct(Product entity)
		{
			_products.Add(entity);
		}
		public static void Delete (Product entity)
		{
			var x = _products.FirstOrDefault(p => p.ProductId == entity.ProductId);
			if (x != null)
			{
				_products.Remove(x);
			}
		}
		public static void editProduct(Product updadetprd)
		{
			var entity = _products.FirstOrDefault(p => p.ProductId == updadetprd.ProductId);
			if (entity != null)
			{
				if (!string.IsNullOrEmpty(entity.Name))
				{
					entity.Name = updadetprd.Name;
				}
				entity.Price = updadetprd.Price;
				entity.Image = updadetprd.Image;
				entity.CategoryId = updadetprd.CategoryId;
				entity.IsActive = updadetprd.IsActive;
			}
		}
		public static void editIsActive(Product updadetprd)
		{
			var entity = _products.FirstOrDefault(p => p.ProductId == updadetprd.ProductId);
			if (entity != null)
			{
				entity.IsActive = updadetprd.IsActive;
			}
		}
		public static List<Category> Categories
		{
			get
			{
				return _categories;
			}
		}
	}
}
