using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace ReceiptCapture
{
   public class TextAnalyzer
    {

        public TextAnalyzerResult Analize(string txtInput)
        {
            //txtInput =
            //    @"Nando's\nCbicken Restaurants\nwww.nandos.co.uk\nVAT No 662 8275 13\nIlford\nTelephone: 02085146012\nYour Order Number Is -\n28\n01/08/2017\n12:50 PM\n10002\nHost: GABRIELLA\n28\nOrder Type: Eat In\n1/2 Chicker\n10.70\nExtra Hot M\nRegular Peri Chips\nRegular Garlic Bread\n1/4 Chickern\n3.95\nExtra HotS\nNo sides\n0.00\n***Nando's Card Swiped***\nSubtotal\n12.21\nTax\nEat In TotaT\nInt CCard\n2.44\n14.65\n $ 14.65 \n20% : 12.21 VAT: 2.44\n";


            var request = (HttpWebRequest)WebRequest.Create("http://api.meaningcloud.com/topics-2.0");

            var postData = $"key=45ba55963b7817fc8fbc9a6bbc114d97&lang=en&txt={txtInput}&tt=mt";

            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return ParseJsonString(responseString);
        }





        public TextAnalyzerResult ParseJsonString(string inputString)
        {
            TextAnalyzerResult returnValue=new TextAnalyzerResult();
            dynamic result = JsonConvert.DeserializeObject(inputString);

            if (result.status.code == 0)
            {
                returnValue.IsValid = true;
                var timeExpressionList = result.time_expression_list;
                foreach (dynamic i in timeExpressionList)
                {
                    if (i.precision == "day")
                    {
                        returnValue.TransactionDate = i.actual_time;
                    }
                    else if (i.precision == "minutesAMPM")
                    {
                        returnValue.TransactionTime = i.form;
                    }
                }

                var moneyExpressionList = result.money_expression_list;
                foreach (dynamic i in moneyExpressionList)
                {
                    returnValue.Amount = i.numeric_value;
                    returnValue.AmountForm = i.amount_form;
                    returnValue.Currency = i.currency;
                }
            }
            else
            {
                returnValue.IsValid = false;
                returnValue.Error = inputString;
            }

            return returnValue;
        }

    }


    public class TextAnalyzerResult
    {
        public string Amount { get; set; }
        public string AmountForm { get; set; }
        public string Currency { get; set; }

        public string TransactionDate { get; set; }

        public string TransactionTime { get; set; }

        public bool IsValid { get; set; }

        public string Error { get; set; }
    }



   
    
}
