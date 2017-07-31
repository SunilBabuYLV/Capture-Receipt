using System.Collections.Generic;
using Google.Cloud.Vision.V1;
using Newtonsoft.Json;

namespace ReceiptCapture
{
    public class ImageScannerServiceResponse
    {
        public ImageScannerServiceResponse()
        {
            responses = new List<AnnotateImageResponse>();
        }

        public List<AnnotateImageResponse> responses { get; set; }
    }
}