using System.IO;

namespace ReceiptCapture
{
    public class ServiceSettings
    {
        static ServiceSettings()
        {

            ReadKeys();

            GoogleVisionRestApi = $"https://vision.googleapis.com/v1/images:annotate?key={_googleVisionApiKey}";
            


            //example value:
            //ImageFilePath = "https://storage.googleapis.com/mybasket/bill.JPG";
            //GoogleVisionApiKey = "uuy3mEw7uuy3mEw7uuy3mEw7uuy3mEw7";
        }

        public static string ImageFilePath => _imageFilePath;
        public static string TopicExtractionRestApiKey => _topicExtractionCloudApiKey;
        public static string TopicExtractionRestApi => "http://api.meaningcloud.com/topics-2.0";
        public static string GoogleVisionRestApi { get; }

       

        private static string _googleVisionApiKey;
        private static string _topicExtractionCloudApiKey;
        private static string _imageFilePath;


        private static void ReadKeys()
        {
            //Read config values from local file... just to prevent accidental checkins my API key to source control.
            _googleVisionApiKey = File.ReadAllText(@"C:\MySubscriptions\GoogleApikey.txt");
            _topicExtractionCloudApiKey = File.ReadAllText(@"C:\MySubscriptions\TopicExtractionCloudApikey.txt");
            _imageFilePath = File.ReadAllText(@"C:\MySubscriptions\Capture_Receipt_ImageURI.txt");
        }

    }
}