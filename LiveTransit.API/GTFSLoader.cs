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

        /// <summary>
        /// Get path to data
        /// </summary>
        /// <returns></returns>
        private string GetPathToData() => Directory.GetCurrentDirectory() + "\\Data";

        /// <summary>
        /// Get data from selected path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private GTFSFeed GetFeed(string path) => new GTFSReader<GTFSFeed>().Read(path);

    }
}
