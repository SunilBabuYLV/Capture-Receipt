using System.IO;

namespace ReceiptCapture
{
    public class ServiceSettings
    {
        static ServiceSettings()
        {
            //Read config values from local file... just to prevent accidental checkins my API key to source control.
            GoogleVisionApiKey = File.ReadAllText(@"C:\Working\Capture_Receipt_key.txt");
            ImageFilePath = File.ReadAllText(@"C:\Working\Capture_Receipt_ImageURI.txt");
            GoogleVisionRestApi =
                $"https://vision.googleapis.com/v1/images:annotate?key={GoogleVisionApiKey}";
           
            //example value:
            //ImageFilePath = "https://storage.googleapis.com/mybasket/bill.JPG";
            //GoogleVisionApiKey = "uuy3mEw7uuy3mEw7uuy3mEw7uuy3mEw7";
        }

        public static string ImageFilePath { get; }
        public static string GoogleVisionRestApi { get; }

        private static string GoogleVisionApiKey { get; }


    }
}