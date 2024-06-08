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
        public IEnumerable<string> GetLinesWithRoutes() {
            IEnumerable<string> route = feed.Routes.Select(e => $"{e.ShortName}|{string.Join('-', e.LongName.Split('-').SkipLast(1))}");
            return route;
        }

        [HttpGet("/Lines")] 
		public IEnumerable<string> GetLines() {
			return feed.Routes.Select(e => e.ShortName);
		}
	}
}
