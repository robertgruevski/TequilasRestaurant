using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TequilasRestaurant.Data;
using TequilasRestaurant.Models;

namespace TequilasRestaurant.Controllers
{
	public class ProductController : Controller
	{
		private Repository<Product> products;
		public ProductController(ApplicationDbContext context)
		{
			this.products = new Repository<Product>(context);
		}

		public async Task<IActionResult> Index()
		{
			return View(await products.GetAllAsync());
		}
	}
}
