namespace LiveTransit.Services.Routes {
	public interface IRouteService {
		Task<string> GetRoute();
        Task<IEnumerable<string>> GetRoutes();
    }
}
