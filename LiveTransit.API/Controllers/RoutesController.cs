using GTFS;
using GTFS.Entities;
using GTFS.Entities.Collections;
using LiveTransit.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextDepartures.Standard.Utils;

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

		[HttpGet("/Full")]
        public IEnumerable<Model> GetFullRoutes(string startCity, string endCity) { //[FromQuery] IEnumerable<TimeOfDay> times

            IEnumerable<TimeOfDay> times = [new TimeOfDay() { Hours = 17, Minutes = 52, Seconds = 0 }, new TimeOfDay() { Hours = 18, Minutes = 12, Seconds = 0}];

            //var selectedRoutes = feed.Routes.Where(e => e.LongName.Split('-').SkipLast(1).Contains(startCity));
            var result = feed.Routes
                    .Join(feed.Trips,
                          t1 => t1.Id,
                          t2 => t2.RouteId,
                          (t1, t2) => new { Table1 = t1, Table2 = t2 })
                    .Join(feed.StopTimes,
                          combined => combined.Table2.Id,
                          t3 => t3.TripId,
                          (combined, t3) => new { combined.Table1, combined.Table2, Table3 = t3 })
                    .Join(feed.Stops,
                          combined => combined.Table3.StopId,
                          t4 => t4.Id,
                          (combined, t4) => new { combined.Table1, combined.Table2, combined.Table3, Table4 = t4 })
                    .Select(x => new Model() {
                        ShortName = x.Table1.ShortName,
                        Id = x.Table2.Id,
                        ArrivalTime = x.Table3.ArrivalTime,
                        DepartureTime = x.Table3.DepartureTime,
                        Name = x.Table4.Name
                    });

            var resultGrouped = result.GroupBy(x => x.Id)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(x => new Model() {
                            Id = x.Id,
                            ShortName = x.ShortName,
                            ArrivalTime = x.ArrivalTime,
                            DepartureTime = x.DepartureTime,
                            Name = x.Name
                        }).ToList()
                    );

            List<Model> finalValues = new List<Model>();
            
            foreach ( var group in resultGrouped ) {
                var values = group.Value
                            .SkipWhile(e => !e.Name.Contains(startCity))
                            .TakeWhile(e => !e.Name.Contains(endCity))
                            .SkipWhile(e => e.ArrivalTime < times.First())
                            .TakeWhile(e => e.DepartureTime < times.Last());

                //finalValues.AddRange(values);
                if ( values.Count() > 2 ) {
                    finalValues.AddRange([values.First(), values.Last()]);
                }
            }
            
            return finalValues;
		}
    }

    public class Model { 
        public string ShortName { get; set; }
        public string Id { get; set; }
        public TimeOfDay? ArrivalTime { get; set; }
        public TimeOfDay? DepartureTime { get; set; }
        public string Name { get; set; }
    }
}
