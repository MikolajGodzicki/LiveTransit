using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace LiveTransit.Services.Routes {
	public class RouteService : IRouteService {
		private readonly HttpClient _httpClient;

		public RouteService(HttpClient httpClient) {
			_httpClient = httpClient;
		}
		
		public async Task<string> GetRoute(string line) {
			string route = await _httpClient.GetStringAsync($"Routes/{line}");
			return route;
		}

        public async Task<IEnumerable<string>> GetRoutes() {
			HttpContent content = _httpClient.GetAsync("Routes").Result.Content;

			var route = await content.ReadAsStringAsync();
            var routes = JsonSerializer.Deserialize<IEnumerable<string>>(route);


			if (routes is null) {
				return Enumerable.Empty<string>();
			}

			return routes;
		}
	}
}
