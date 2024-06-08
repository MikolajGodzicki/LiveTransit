using Microsoft.AspNetCore.Components;

namespace LiveTransit.Services.Routes {
	public class RouteService : IRouteService {
		private readonly HttpClient _httpClient;

		public RouteService(HttpClient httpClient) {
			_httpClient = httpClient;
		}
		
		public async Task<string> GetRoute() {
			string route = await _httpClient.GetStringAsync("Routes");
			return route;
		}
	}
}
