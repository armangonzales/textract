using System;
using System.Text;
using Amazon;
using Amazon.Textract;
using Amazon.Textract.Model;

namespace PdfTextractExtractor
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            // Specify the S3 bucket name and URL of the PDF document
            string bucketName = "textract-console-ap-southeast-1-a5010ed1-938c-43df-b21f-ed271d9";
            string documentUrl = "https://textract-console-ap-southeast-1-a5010ed1-938c-43df-b21f-ed271d9.s3.ap-southeast-1.amazonaws.com/91fe2f30_95c3_436f_bd4b_55f2a28f5228_sample.pdf";

            var region = RegionEndpoint.APSoutheast1;

            try
            {
                StringBuilder extractedText = new StringBuilder();

                // Initialize the Textract client with the default credential chain
                var textractClient = new AmazonTextractClient(region);

                // Specify the request to analyze the document
                var request = new AnalyzeDocumentRequest
                {
                    Document = new Document
                    {
                        S3Object = new S3Object
                        {
                            Bucket = bucketName,
                            Name = documentUrl
                        }
                    },
                    FeatureTypes = new List<string> { "TABLES", "FORMS" }
                };

                // Call Amazon Textract to analyze the document
                var response = await textractClient.AnalyzeDocumentAsync(request);

                // Extracted tables
                List<Block> tables = response.Blocks.FindAll(b => b.BlockType == "TABLE");

                // Extracted form fields
                List<Block> forms = response.Blocks.FindAll(b => b.BlockType == "KEY_VALUE_SET");

                // Display or process the extracted tables and forms
                Console.WriteLine("Extracted Tables:");
                foreach (var table in tables)
                {
                    Console.WriteLine(table.BlockType + ": " + table.Text);
                }

                Console.WriteLine("\nExtracted Forms:");
                foreach (var form in forms)
                {
                    Console.WriteLine(form.BlockType + ": " + form.Text);
                }
            }
            catch (AmazonTextractException e)
            {
                Console.WriteLine("Error analyzing document:");
                Console.WriteLine(e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
