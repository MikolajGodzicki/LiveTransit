using GTFS;
using GTFS.Entities.Collections;

namespace LiveTransit.API.Services {
	public interface ILoader {
		public IUniqueEntityCollection<GTFS.Entities.Route> LoadRoutes();
	}
}

