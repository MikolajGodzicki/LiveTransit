using GTFS.Entities;

namespace LiveTransit.Models {
    public class RouteModelInternal {
        public string ShortName { get; set; }
        public string Id { get; set; }
        public TimeOfDay? ArrivalTime { get; set; }
        public TimeOfDay? DepartureTime { get; set; }
        public string Name { get; set; }
    }
}
