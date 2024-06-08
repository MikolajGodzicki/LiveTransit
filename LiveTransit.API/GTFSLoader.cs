using GTFS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveTransit.API {
    public class GTFSLoader {
        /// <summary>
        /// Data about every route etc.
        /// </summary>
        private GTFSFeed feed;

        public GTFSLoader() {
            feed = GetFeed(GetPathToData());
        }

        private string GetPathToData() => Directory.GetCurrentDirectory() + "\\Data";

        private GTFSFeed GetFeed(string path) => new GTFSReader<GTFSFeed>().Read(path);

    }
}
