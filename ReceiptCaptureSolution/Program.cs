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
                Console.WriteLine(resultText);

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