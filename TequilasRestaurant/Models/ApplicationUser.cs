using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TequilasRestaurant.Models
{
	public class ApplicationUser : IdentityUser
	{
		public ICollection<Order>? Orders { get; set; }

		[NotMapped]
		public IList<string> RoleNames { get; set; } = null!;
	}
}
