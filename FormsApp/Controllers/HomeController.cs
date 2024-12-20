using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(string searchString, string category)
    {
        var products = Repository.Product;
        ViewBag.SearchString = searchString;
        if (!string.IsNullOrEmpty(searchString))
        {
            products = products.Where(p => p.Name!.ToLower().Contains(searchString)).ToList();
        }

        if (!string.IsNullOrEmpty(category) && category !="0")
        {
            products=products.Where(p=> p.CategoryId==int.Parse(category)).ToList();
        }

        //ViewBag.Categoriess = new SelectList(Repository.Categories, "CategoryId", "Name",category);
        var model = new ProductViewModel
        {
            Products = products,
            Categories = Repository.Categories,
            SelectedCategory = category
        };
		return View(model);
    }
    [HttpGet]
    public IActionResult Create()
    {
		ViewBag.ktgr= new SelectList(Repository.Categories, "CategoryId", "Name");
		return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Product model, IFormFile imageFile)
    {
        var extension = "";
		if (imageFile!=null)
		{
			var extensionsallowed = new[] { "jpg", "jpeg", "png" };
		    extension = Path.GetExtension(imageFile.FileName);// dosya ad�n� al
			if (!extensionsallowed.Contains(extension))
            {
                ModelState.AddModelError("", "ge�erli bir resim ekleyin");
            }
        }
		if (ModelState.IsValid)
        {
            if (imageFile != null)
            {
				var randomname = string.Format($"{Guid.NewGuid().ToString()}{extension}");// random dosya ad� olu�turdu
				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img", randomname);//mevcut dosyan�n uzant�s�n� getir, wwwroot i�ine yeni dosyay� ata
				using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
				model.Image = randomname;
				model.ProductId = Repository.Product.Count() + 1;
				Repository.Createproduct(model);
				return RedirectToAction("Index");
			}     
		}
		ViewBag.ktgr = new SelectList(Repository.Categories, "CategoryId", "Name");
		return View(model);
    }
    public IActionResult Edit(int? id)
    {
        if(id==null)
        {
            return NotFound();
        }
        var x=Repository.Product.FirstOrDefault(p=> p.ProductId==id);
        if(x==null)
        { return NotFound(); }
		ViewBag.ktgr = new SelectList(Repository.Categories, "CategoryId", "Name");
		return View(x);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id,Product model, IFormFile imageFile)
    {
        if(id!=model.ProductId)
        { return NotFound(); }
        if(ModelState.IsValid)
        {
			var extension = Path.GetExtension(imageFile.FileName);// dosya ad�n� al
			var randomname = string.Format($"{Guid.NewGuid().ToString()}{extension}");// random dosya ad� olu�turdu
			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img", randomname);
			if (imageFile != null)
			{
				using (var stream = new FileStream(path, FileMode.Create))
				{
					await imageFile.CopyToAsync(stream);
				}
				model.Image = randomname;
			}
            Repository.editProduct(model);
            
		}
		ViewBag.ktgr = new SelectList(Repository.Categories, "CategoryId", "Name");
		return View(model);
	}
    public IActionResult Delete(int? id) 
    { 
        if(id==null)
        {
            return NotFound();
        }
		var x = Repository.Product.FirstOrDefault(p => p.ProductId == id);
        if(x==null)
        {
            return NotFound();
        }
        return View("DeletePage", x);
	}
    [HttpPost]
    public IActionResult Delete(int id , int ProductId)
    {
        if(id!=ProductId) { return NotFound(); }
        var x = Repository.Product.FirstOrDefault(x => x.ProductId == ProductId);
        if(x==null)
        {
            return NotFound();
        }
        Repository.Product.Remove(x);
        return RedirectToAction("Index");
    }
    public IActionResult EditProducts(List<Product> Products)
    {
        foreach(var t in Products)
        {
            Repository.editIsActive(t);
        }
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
