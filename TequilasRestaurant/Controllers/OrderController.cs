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

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddItem(int prodId, int prodQty)
		{
			var product = await _context.Products.FindAsync(prodId);

			if (product == null)
			{
				return NotFound();
			}

			// Retrieve or create an OrderViewModel from session or other state management
			var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
			{
				OrderItems = new List<OrderItemViewModel>(),
				Products = await _products.GetAllAsync()
			};

			// Check if the product is already in the order
			var existingItem = model.OrderItems.FirstOrDefault(oi => oi.ProductId == prodId);

			// If the product is already in the order, update the quantity
			if (existingItem != null)
			{
				existingItem.Quantity += prodQty;
			}
			else
			{
				// If the product is not in the order, add a new OrderItemViewModel
				model.OrderItems.Add(new OrderItemViewModel
				{
					ProductId = prodId,
					Price = product.Price,
					Quantity = prodQty,
					ProductName = product.Name
				});
			}

			model.TotalAmount = model.OrderItems.Sum(oi => oi.Price * oi.Quantity);

			// Store the updated OrderViewModel in the session
			HttpContext.Session.Set("OrderViewModel", model);

			return RedirectToAction("Create", model);
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
