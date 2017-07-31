using System.Collections.Generic;
using Google.Cloud.Vision.V1;

namespace ReceiptCapture
{
    public class ImageScannerServiceRequest
    {
        public ImageScannerServiceRequest()
        {
            requests = new List<AnnotateImageRequest>();
        }

        public List<AnnotateImageRequest> requests { get; set; }
    }
}