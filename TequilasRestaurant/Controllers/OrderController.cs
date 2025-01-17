using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TequilasRestaurant.Data;
using TequilasRestaurant.Models;

namespace TequilasRestaurant.Controllers
{
	public class OrderController : Controller
	{
		private readonly ApplicationDbContext _context;
		private Repository<Product> _products;
		private Repository<Order> _orders;
		private readonly UserManager<ApplicationUser> _userManager;

		public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
			_products = new Repository<Product>(context);
			_orders = new Repository<Order>(context);
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Create()
		{

			//ViewBag.Products = await _products.GetAllAsync();

			//Retrieve or create an OrderViewModel from session or other state management
			var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
			{
				OrderItems = new List<OrderItemViewModel>(),
				Products = await _products.GetAllAsync()
			};

			return View(model);
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
