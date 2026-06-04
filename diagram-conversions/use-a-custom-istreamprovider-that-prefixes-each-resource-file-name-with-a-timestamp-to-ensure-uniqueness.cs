using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExport
{
    // Custom stream provider that prefixes each resource file name with a timestamp
    public class TimestampStreamProvider : IStreamProvider
    {
        // Called when Aspose.Diagram needs a stream for a resource (e.g., images, CSS)
        public void InitStream(StreamProviderOptions options)
        {
            // Get the original resource name (e.g., "image1.png")
            string originalName = Path.GetFileName(options.DefaultPath);

            // Create a timestamp prefix (yyyyMMdd_HHmmssfff)
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");

            // Build a unique file name
            string uniqueName = $"{timestamp}_{originalName}";

            // Determine a folder to store the resources (same folder as the output HTML)
            string outputFolder = Path.GetDirectoryName(options.DefaultPath) ?? Directory.GetCurrentDirectory();

            // Ensure the folder exists
            Directory.CreateDirectory(outputFolder);

            // Full path for the resource file
            string fullPath = Path.Combine(outputFolder, uniqueName);

            // Assign a writable file stream to the options
            options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        }

        // Called after Aspose.Diagram finishes writing to the stream
        public void CloseStream(StreamProviderOptions options)
        {
            // Safely close the stream if it was created
            options.Stream?.Close();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML export options and assign the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new TimestampStreamProvider()
                };

                // Export the diagram to HTML; resources will be saved with timestamped names
                string outputHtml = "output.html";
                diagram.Save(outputHtml, htmlOptions);

                Console.WriteLine($"Diagram exported to {outputHtml} with timestamped resources.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}