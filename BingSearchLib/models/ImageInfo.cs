using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingSearchLib.models
{
    public class ImageInfo
    {
        public string Name { get; set; }
        public string WebSearchUrl { get; set; }
        public string ThumnbnailUrl { get; set; }
        public string DatePublished { get; set; }
        public string ContentUrl { get; set; }
        public string HostPageUrl { get; set; }
        public string ContentSize { get; set; }
        public string EncodingFormat { get; set; }
        public string HostPageDisplayUrl { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Thumbnail Thumbnail { get; set; }
        public string ImageInsightsToken { get; set; }
        public InsightsSourcesSummary InsightsSourcesSummary { get; set; }

        public string ImageId { get; set; }
        public string AccentColor { get; set; }
        


    }
}

/*
     {
      "name": "Cat’s Tale: Meowing for the right kind of food - Zee Lifestyle",
      "webSearchUrl": "https://www.bing.com/cr?IG=40C2A0E2DC3A4C52B8E1F06481D7687B&CID=2C45643CDD5B640426C46EF0DC046577&rd=1&h=FdQa6rSXoHOzpfbQzBBJFkCfYS5QwCg3qeCnB1NP_Ak&v=1&r=https%3a%2f%2fwww.bing.com%2fimages%2fsearch%3fview%3ddetailv2%26FORM%3dOIIRPO%26q%3dcats%26id%3dF42DAAA598B5E0E1383CED35C292387266EB9A9F%26simid%3d608030898169185114&p=DevEx,5006.1",
      "thumbnailUrl": "https://tse1.mm.bing.net/th?id=OIP.qtkOFD3PTouGYLHxi3xypwEsDh&pid=Api",
      "datePublished": "2017-06-04T23:55:00",
      "contentUrl": "http://www.bing.com/cr?IG=40C2A0E2DC3A4C52B8E1F06481D7687B&CID=2C45643CDD5B640426C46EF0DC046577&rd=1&h=EzJNc8H7B65x5t1izT-GX8GaFKvOzLm1SvPiSgfm-eY&v=1&r=http%3a%2f%2fzeelifestylecebu.com%2fwp-content%2fuploads%2f2015%2f03%2fcat3.jpg&p=DevEx,5008.1",
      "hostPageUrl": "http://www.bing.com/cr?IG=40C2A0E2DC3A4C52B8E1F06481D7687B&CID=2C45643CDD5B640426C46EF0DC046577&rd=1&h=lGvbJmr1LTu2Hr8ZWEK7YFUWqmWUkcUY3SoiVLRAUvw&v=1&r=http%3a%2f%2fzeelifestylecebu.com%2fa-cats-tale-meowing-for-the-right-kind-of-food%2f&p=DevEx,5007.1",
      "contentSize": "414235 B",
      "encodingFormat": "jpeg",
      "hostPageDisplayUrl": "zeelifestylecebu.com/a-cats-tale-meowing-for-the-right-kind-of-food",
      "width": 1600,
      "height": 1200,
      "thumbnail": 
	  {
        "width": 300,
        "height": 225
      },
      "imageInsightsToken": "ccid_qtkOFD3P*mid_F42DAAA598B5E0E1383CED35C292387266EB9A9F*simid_608030898169185114",
      "insightsSourcesSummary": 
	  {
        "shoppingSourcesCount": 0,
        "recipeSourcesCount": 0
      },
      "imageId": "F42DAAA598B5E0E1383CED35C292387266EB9A9F",
      "accentColor": "8E553D"
    },

*/
