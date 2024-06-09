using LiveTransit.Models;

namespace LiveTransit.Services
{
    public interface IRouteService
    {
        Task<string> GetRoute(string line);
        Task<IEnumerable<string>> GetLinesWithRoutes();
        Task<IEnumerable<string>> GetLines();
        Task<IEnumerable<string>> GetRoutesFrom(int startIndex, int count);
		Task<IEnumerable<RouteModelInternal>> GetFullRoutes(int startHours, int endHours, string startCity, string endCity);
        Task<IEnumerable<string>> GetStops();
	}
}
