namespace LiveTransit.Services
{
    public interface IRouteService
    {
        Task<string> GetRoute(string line);
        Task<IEnumerable<string>> GetLinesWithRoutes();
        Task<IEnumerable<string>> GetLines();
        Task<IEnumerable<string>> GetRoutesFrom(int startIndex, int count);
    }
}
