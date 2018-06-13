using System;
using BingSearchLib;

namespace BingImageSearch
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] carModelsList = CarModels.GetPopularCarModels(4, 16);
            var rootFolder = @"c:\test\Cars\";
            var prefix = "car";
            int maxImagesToDownload = 25;

            var imgSearch = new ImageSearch();
            imgSearch.FileDownloading += FileDownloadingEventHandler;
            imgSearch.InformationEvent += InformationEventHandler;

            foreach (string carModel in carModelsList)
            {
                var searchString = carModel;
                Console.WriteLine("Searching for images matching {0}...", carModel);
                Console.WriteLine();

                imgSearch.FindAndDownloadMatchingImages(searchString, rootFolder, prefix, maxImagesToDownload);
            }

            //if (args.Length > 0 && args[0] == "?")
            //{
            //    ShowUsage();
            //    return;
            //}

            // Default args
            //var searchString = "Escalade";
            //var rootFolder = @"c:\test\Cars\";
            //var prefix = "car";

            //GetArguments(args, ref searchString, ref rootFolder, ref prefix);

            //FindAndDownloadMatchingImages(searchString, rootFolder, prefix);

            Console.WriteLine("Press ENTER to end program.");
            Console.ReadLine();
        }


        private static void GetArguments(string[] args, ref string searchString, ref string rootFolder, ref string prefix)
        {
            if (args.Length > 0)
            {
                searchString = args[0];
            }
            if (args.Length > 1)
            {
                rootFolder = args[1];
            }
            if (args.Length > 2)
            {
                prefix = args[2];
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("BingImageSearch <SearchTerm> <RootFolder> <Prefix>");
            Console.WriteLine("SearchTerm=Term to search for.");
            Console.WriteLine("RootFolder=Where to store downloaded files.");
            Console.WriteLine("Prefix=The prefix of every downloaded file.");
        }

        static void FileDownloadingEventHandler(object sender, FileDownloadingEventArgs e)
        {
            Console.WriteLine("{0}, Image #{1}:", e.SearchString, e.ImageNumber);
            Console.WriteLine("Downloading from");
            Console.WriteLine(" {0}", e.SourceUrl);
            Console.WriteLine(" to");
            Console.WriteLine(" {0}...", e.FileName);
            Console.WriteLine();
        }

        static void InformationEventHandler(object sender, InformationEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }

}
