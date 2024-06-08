using GTFS;
using GTFS.Entities.Collections;

namespace LiveTransit.API.Services {
	public class GTFSLoaderService : ILoader {
		GTFSReader<GTFSFeed> gTFSReader;

		GTFSFeed gTFSFeed;
		public GTFSLoaderService() {
			gTFSReader = new GTFSReader<GTFSFeed>();
			gTFSFeed = gTFSReader.Read(Directory.GetCurrentDirectory() + "\\Data");
		}

		public IUniqueEntityCollection<GTFS.Entities.Route> LoadRoutes() {
			return gTFSFeed.Routes;
		}
	}
}
