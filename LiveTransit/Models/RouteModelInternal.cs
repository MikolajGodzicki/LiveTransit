using GTFS.Entities;

namespace LiveTransit.Models {
    [Serializable]
    public class RouteModelInternal {
        public string shortName { get; set; }
        public string id { get; set; }
        public TimeOfDay arrivalTime { get; set; }
        public TimeOfDay departureTime { get; set; }
        public string name { get; set; }
    }
}
