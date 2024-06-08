using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace LiveTransit.Services
{
    public class RouteService : IRouteService
    {
        private readonly HttpClient _httpClient;

        public RouteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<string>> GetLines() {
            HttpContent content = _httpClient.GetAsync("Routes/Lines").Result.Content;

            var route = await content.ReadAsStringAsync();
            var routes = JsonSerializer.Deserialize<IEnumerable<string>>(route);


            if (routes is null) {
                return Enumerable.Empty<string>();
            }

            return routes;
        }

        public async Task<IEnumerable<string>> GetLinesWithRoutes() {
            HttpContent content = _httpClient.GetAsync("Routes").Result.Content;

            var route = await content.ReadAsStringAsync();
            var routes = JsonSerializer.Deserialize<IEnumerable<string>>(route);


            if (routes is null) {
                return Enumerable.Empty<string>();
            }

            return routes;
        }

        public async Task<string> GetRoute(string line)
        {
            string route = await _httpClient.GetStringAsync($"Routes/{line}");
            return route;
        }

        public async Task<IEnumerable<string>> GetRoutesFrom(int startIndex, int count) {
            var items = await GetLinesWithRoutes();

            items = items.Skip(startIndex).Take(count);

            return items;
        }
    }
}
