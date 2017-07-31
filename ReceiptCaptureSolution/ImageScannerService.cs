using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Google.Cloud.Vision.V1;
using Newtonsoft.Json;

namespace ReceiptCapture
{
    public class ImageScannerService
    {

        /// <summary>
        /// Scan image and get description by using Google vision api.
        /// Rest Api call is used
        /// </summary>
        /// <returns></returns>
        public string ScanImageForDescription()
        {
            try
            {
                //generate request object
                var requests = GenerateImageScannerServiceRequest();

                //Scan Image
                var scannedImageResponse = ScanImage(requests);

                //return total text description.
                return scannedImageResponse.responses.FirstOrDefault()?.TextAnnotations.FirstOrDefault()?.Description;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception Block" + exception.Message);
                throw;
            }
        }

        private ImageScannerServiceResponse ScanImage(ImageScannerServiceRequest requests)
        {
            //populate json request
            var jsonRequest = JsonConvert.SerializeObject(requests, new JsonSerializerSettings
            {
                ContractResolver = new ShouldSerializeContractResolver()
            });

            //post data
            var client = new HttpClient();
            var targetUri =new Uri(ServiceSettings.GoogleVisionRestApi);

            var request = new HttpRequestMessage(HttpMethod.Post, targetUri);
            request.Headers.Add("Accept", "application/json");
            request.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var result = client.SendAsync(request).Result;
            var jsonResponseContent = result.Content.ReadAsStringAsync().Result;

            //temp fix ..for enum types inconsistency between Rest API and nuget packages.
            jsonResponseContent = jsonResponseContent.Replace("EOL_SURE_SPACE", "EOLSURESPACE");
            jsonResponseContent = jsonResponseContent.Replace("SURE_SPACE", "SURESPACE");
            jsonResponseContent = jsonResponseContent.Replace("LINE_BREAK", "LINEBREAK");

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var responsesData = JsonConvert.DeserializeObject<ImageScannerServiceResponse>(jsonResponseContent,
                settings
            );

            return responsesData;
        }

        private ImageScannerServiceRequest GenerateImageScannerServiceRequest()
        {
            //Read Image from Uri and create request obj. 
            var filePath = ServiceSettings.ImageFilePath; //Image Uri path - that need to be scanned.
            
            var requests = new ImageScannerServiceRequest();
            var requestinfo =
                new AnnotateImageRequest
                {
                    Image = Image.FromUri(new Uri(filePath))
                };

            requestinfo.Features.Add(new Feature
            {
                Type = Feature.Types.Type.TextDetection
            });

            requests.requests.Add(requestinfo);
            return requests;
        }
    }
}