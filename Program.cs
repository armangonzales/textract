using System;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace PdfTextTableExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Specify the path to the PDF file
            string pdfFilePath = @"C:\Users\Public\sample.pdf";

            // Create a StringBuilder to hold the extracted text
            StringBuilder extractedText = new StringBuilder();

            try
            {
                // Open the PDF file
                using (PdfReader pdfReader = new PdfReader(pdfFilePath))
                {
                    using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
                    {
                        // Iterate through each page
                        for (int pageNumber = 1; pageNumber <= pdfDocument.GetNumberOfPages(); pageNumber++)
                        {
                            // Extract text from the page
                            ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                            string pageText = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(pageNumber), strategy);

                            // Append the extracted text to the StringBuilder
                            extractedText.AppendLine(pageText);
                        }
                    }
                }

                // Display the extracted text
                Console.WriteLine("Extracted Text:");
                Console.WriteLine(extractedText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
