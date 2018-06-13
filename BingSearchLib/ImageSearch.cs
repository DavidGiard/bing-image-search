using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BingSearchLib.models;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace BingSearchLib
{
    public class ImageSearch
    {
        const string uriBase = "https://api.cognitive.microsoft.com/bing/v5.0/images/search";
        const string subscriptionKey = "392c8fa05e0243d2bd4575f04d0bcf21";

        public void FindAndDownloadMatchingImages(string searchString, string rootFolder, string prefix, int maxImagesToDownload)
        {
            // How many images>
            BingImageSearchResponse initResults = GetImages(searchString, 1, 0).Result;
            int estTotalImages = initResults.TotalEstimatedMatches;
            var msg = String.Format("Est. #Results=" + estTotalImages);

            int numResultsAtATime = 100;

            bool keepGettingImages = true;
            var offSet = 0;
            var imageFoundCount = 0;
            var downloadCount = 0;
            while (keepGettingImages)
            {
                BingImageSearchResponse searchResults = GetImages(searchString, numResultsAtATime, offSet).Result;
                offSet += numResultsAtATime;
                imageFoundCount += numResultsAtATime;

                InformationEvent(this, new InformationEventArgs(msg));

                var destinationFolder = GetDestinationFolder(rootFolder, searchString);

                var values = searchResults.Value;
                foreach (ImageInfo info in values)
                {
                    downloadCount++;
                    if (downloadCount > maxImagesToDownload)
                    {
                        keepGettingImages = false;
                        break;
                    }
                    var sourceUrl = info.ContentUrl;
                    string fileName = DestinationFileName(info, prefix, destinationFolder, downloadCount);
                    var fdleArgs = new FileDownloadingEventArgs(searchString, sourceUrl, fileName, downloadCount);
                    FileDownloading(this, fdleArgs);

                    DownloadFile(sourceUrl, fileName);
                }

                if (imageFoundCount > estTotalImages)
                {
                    keepGettingImages = false;
                }
            }

            var summaryMsg = String.Format(
                "Est. #Results={0}\n#Results returned={1}\n", 
                estTotalImages, 
                downloadCount);
            InformationEvent(this, new InformationEventArgs(summaryMsg));
        }

        private static async Task<BingImageSearchResponse> GetImages(string searchString, int numResults, int offSet)
        {
            BingImageSearchResponse results = null;
            results = await SearchImages(searchString, numResults, offSet);
            return results;

        }

        public void DownLoadFiles (ImageInfo[] imageInfos, int start, int maxDownloads, string destinationFolder, string prefix, string searchString)
        {
            var count = 0;
            foreach (ImageInfo info in imageInfos)
            {
                count++;
                var sourceUrl = info.ContentUrl;
                //var destinationFolder = @"c:\test\Cars\";
                string fileName = DestinationFileName(info, prefix, destinationFolder, count);

                var fdleArgs = new FileDownloadingEventArgs(searchString, sourceUrl, fileName, count);
                FileDownloading(this, fdleArgs);

                DownloadFile(sourceUrl, fileName);
            }
        }

        public static string GetDestinationFolder(string rootFolder, string searchTerm)
        {
            if (!rootFolder.EndsWith(@"\"))
            {
                rootFolder += @"\";
            }
            string destinationFolderName = rootFolder + searchTerm;
            Directory.CreateDirectory(destinationFolderName);
            return destinationFolderName;
        }

        public static string DestinationFileName (ImageInfo info, string prefix, string folder, int count)
        {
            if (!folder.EndsWith("/"))
            {
                folder += "/";
            }
            var fileType = info.EncodingFormat;
            var fileName = folder
                + prefix + count.ToString("0000")
                + "." + fileType;

            return fileName;

        }

        public static async Task<BingImageSearchResponse> SearchImages(string searchString, int numResults, int offSet)
        {
            BingImageSearchResponse results = null;
            //int numResults = 10;
            //int offSet = 0;
            int totalReturned = 0;
            bool lookForMore = true;
            while (lookForMore)
            {
                results = await MakeRequest(searchString, offSet, numResults);
                int resultsReturned = results.Value.Length;
                totalReturned += resultsReturned;
                lookForMore = false;
            }

            return results;
        }

        static async Task<BingImageSearchResponse> MakeRequest(string searchString, int offSet, int numResults)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameters
            //../images/search?q=Ferrari&count=10&offset=0&mkt=en-us&safeSearch=Strict
            queryString["q"] = searchString;
            queryString["count"] = numResults.ToString("###0");
            queryString["offset"] = offSet.ToString("###0");
            queryString["mkt"] = "en-us";
            queryString["safeSearch"] = "Strict";
            var uri = uriBase + "?" + queryString;

            HttpResponseMessage response;

            byte[] byteData = Encoding.UTF8.GetBytes("");
            string contentString = "";
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
                response = await client.PostAsync(uri, content);
                contentString = await response.Content.ReadAsStringAsync();
            }
            BingImageSearchResponse returnValue = JsonConvert.DeserializeObject<BingImageSearchResponse>(contentString);


            return returnValue;
        }

        public static void DownloadFile (string fileUrl, string destination)
        {
            try
            {
                var client = new WebClient();
                client.DownloadFile(new Uri(fileUrl), destination);
            }
            catch (Exception)
            {
                // Ignore errors

            }
        }

        public event EventHandler<FileDownloadingEventArgs> FileDownloading;
        public event EventHandler<InformationEventArgs> InformationEvent;

        //protected virtual void OnFileDownloading(FileDownloadingEventArgs e)
        //{
        //    EventHandler handler = FileDownloading;
        //    if (handler != null)
        //    {
        //        handler(this, e);
        //    }
        //}

    }

    public class FileDownloadingEventArgs : EventArgs
    {
        public FileDownloadingEventArgs(
            string searchString, 
            string sourceUrl, 
            string fileName,
            int imageNumber)
        {
            SearchString = searchString;
            SourceUrl = sourceUrl;
            FileName = fileName;
            ImageNumber = imageNumber;
        }
        public string SearchString { get; set; }
        public string SourceUrl { get; set; }
        public string FileName { get; set; }
        public int ImageNumber { get; set; }
    }

    public class InformationEventArgs : EventArgs
    {
        public InformationEventArgs(string msg)
        {
            Message = msg;
        }
        public string Message { get; set; }
    }


    }
