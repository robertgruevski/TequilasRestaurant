using Microsoft.AspNetCore.Identity;

namespace TequilasRestaurant.Models
{
	public class IdentityConfig
	{
		public static async Task CreateAdminUserAsync(IServiceProvider provider)
		{
			var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();

			string userName = "admin@tequilas.com";
			string password = "Admin1!";
			string roleName = "Admin";

			// If role does not exist, create it
			if (await roleManager.FindByNameAsync(userName) == null)
			{
				await roleManager.CreateAsync(new IdentityRole(roleName));
			}

			// If user name does not exist, create it and add a role
			if (await roleManager.FindByNameAsync(userName) == null)
			{
				ApplicationUser user = new ApplicationUser { UserName = userName, Email = userName, EmailConfirmed = true };
				var result = await userManager.CreateAsync(user, password);
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, roleName);
				}
			}
		}
	}
}
