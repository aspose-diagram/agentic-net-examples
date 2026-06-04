using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace HtmlConversionWithCustomStreamProvider
{
    // Custom stream provider for HTML export
    public class CustomStreamProvider : IStreamProvider
    {
        // Called when a new stream is required for a resource (e.g., image) during HTML export
        public void InitStream(StreamProviderOptions options)
        {
            // Create a file stream in the output directory using the default path as the file name
            // Ensure the directory exists
            string outputDir = Path.GetDirectoryName(options.DefaultPath);
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Open a file stream for writing the resource
            options.Stream = new FileStream(options.DefaultPath, FileMode.Create, FileAccess.Write);
        }

        // Called after the resource has been written
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream if it was created
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output HTML file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: HtmlConversionWithCustomStreamProvider <inputVisioFile> <outputHtmlFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options and assign the custom stream provider
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new CustomStreamProvider();

            // Perform the HTML conversion
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully converted to HTML: {outputPath}");
        }
    }
}