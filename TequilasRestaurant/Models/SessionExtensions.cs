using System.Text.Json;

namespace TequilasRestaurant.Models
{
	public static class SessionExtensions
	{
		public static void Set<T>(this ISession session, string key, T value) // Set objects in session
		{
			session.SetString(key, JsonSerializer.Serialize(value));
		}

		public static T Get<T>(this ISession session, string key) // Read objects out of session
		{
			var json = session.GetString(key);

			if (string.IsNullOrEmpty(json))
			{
				return default(T);
			}
			else
			{
				return JsonSerializer.Deserialize<T>(json);
			}
		}
	}
}
