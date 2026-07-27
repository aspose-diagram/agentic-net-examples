using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom stream provider that logs each resource creation event
    public class LoggingStreamProvider : IStreamProvider
    {
        // Called when a new resource stream is required
        public void InitStream(StreamProviderOptions options)
        {
            // Log the resource being created (e.g., image, CSS, etc.)
            Console.WriteLine($"[Log] Creating resource: {options.DefaultPath}");

            // Create a file stream for the resource and assign it to the options
            // Ensure the directory exists
            string directory = Path.GetDirectoryName(options.DefaultPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            options.Stream = new FileStream(options.DefaultPath, FileMode.Create, FileAccess.Write);
        }

        // Called when the resource stream is closed
        public void CloseStream(StreamProviderOptions options)
        {
            // Log the closing of the resource
            Console.WriteLine($"[Log] Closing resource: {options.DefaultPath}");

            // Dispose the stream if it was created
            options.Stream?.Dispose();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // Input Visio file (adjust the path as needed)
                string inputPath = "input.vsdx";

                // Output HTML file
                string outputPath = "output.html";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML save options and assign the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new LoggingStreamProvider()
                };

                // Save the diagram as HTML; the stream provider will log each resource creation
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine("HTML export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}