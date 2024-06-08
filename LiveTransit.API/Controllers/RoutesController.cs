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
		GTFSFeed feed;

		public RoutesController(ILoader gtfsLoader)
        {
			feed = gtfsLoader.Load();
        }

		[HttpGet("{line}")] 
		public string GetRoute(string line) {
			string route = feed.Routes.First(e => e.ShortName == line).LongName;

			var fullRoute = route.Split('-').SkipLast(1);

			route = string.Join('-', fullRoute);

			return route;
		}

		[HttpGet("")] 
		public IEnumerable<string> GetRoutes() {
			return feed.Routes.Select(e => e.ShortName);
		}
	}
}
