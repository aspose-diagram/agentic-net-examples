using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExportWithLogging
{
    // Custom stream provider that logs resource creation events
    public class LoggingStreamProvider : IStreamProvider
    {
        // Called when a new resource stream is required (e.g., a file for an HTML resource)
        public void InitStream(StreamProviderOptions options)
        {
            // Log the creation of the resource
            Console.WriteLine($"[Log] Creating resource: {options.DefaultPath}");

            // Create the file stream for the resource
            options.Stream = new FileStream(options.DefaultPath, FileMode.Create, FileAccess.Write);
        }

        // Called after the resource has been written
        public void CloseStream(StreamProviderOptions options)
        {
            // Ensure the stream is properly closed
            if (options.Stream != null)
            {
                options.Stream.Close();
                Console.WriteLine($"[Log] Closed resource: {options.DefaultPath}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output HTML file path
                string outputPath = "output.html";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML save options and assign the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new LoggingStreamProvider();

                // Save the diagram as HTML; the stream provider will log each resource creation
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine("Diagram exported to HTML with logging completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}