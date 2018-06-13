using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingSearchLib.models
{
    public class BingImageSearchResponse
    {
        public string _Type { get; set; }
        public Instrumentation Instrumentation { get; set; }
        public string ReadLink { get; set; }
        public string WebSearchUrl { get; set; }
        public int TotalEstimatedMatches { get; set; }

        public ImageInfo[] Value { get; set; }

    }
}
