namespace LiveTransit.Services
{
    public interface IRouteService
    {
        Task<string> GetRoute(string line);
        Task<IEnumerable<string>> GetRoutes();
    }
}
