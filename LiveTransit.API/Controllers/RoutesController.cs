using GTFS;
using GTFS.Entities.Collections;
using LiveTransit.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiveTransit.API.Controllers {
	[Route("[controller]")]
	[ApiController]
	public class RoutesController : ControllerBase {
		/// <summary>
		/// Data serialized from file
		/// </summary>
		IUniqueEntityCollection<GTFS.Entities.Route> routes;

		public RoutesController(ILoader gtfsLoader)
        {
			routes = gtfsLoader.LoadRoutes();
        }

		[HttpGet("")] 
		public string GetRoute() {
			return routes.First().ShortName;
		}
	}
}
