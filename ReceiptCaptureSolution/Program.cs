using System;

namespace ReceiptCapture
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var obj = new ImageScannerService();
                var resultText = obj.ScanImageForDescription();
                var analyzer=new TextAnalyzer();
                var result=analyzer.Analize(resultText);
                Console.WriteLine(result.IsValid);
                Console.WriteLine($"Date : {result.TransactionDate}");
                Console.WriteLine($"Time :{result.Amount}");

                Console.WriteLine("Task Completed......");
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception - {e}");
            }

            Console.ReadKey();
        }
    }
}