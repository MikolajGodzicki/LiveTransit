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
			/*
			feed.CalendarDates.Join(
					  feed.Routes, 
					  calendar => calendar.ServiceId, 
					  route => route.Id, 
					  (student, standard) => new 
					  {
						  StudentName = student.StudentName,
						  StandardName = standard.StandardName
					  });*/
			string route = feed.Routes.First().LongName;

			return route.Substring(0, route.Length - 4);
		}
	}
}
