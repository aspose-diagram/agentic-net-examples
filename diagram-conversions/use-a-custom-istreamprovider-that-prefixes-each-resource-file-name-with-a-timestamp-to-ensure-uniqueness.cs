using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExportExample
{
    // Custom stream provider that prefixes each resource file name with a timestamp
    public class TimestampedStreamProvider : IStreamProvider
    {
        public void InitStream(StreamProviderOptions options)
        {
            // Create a unique file name using the current timestamp and the original resource name
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string originalFileName = Path.GetFileName(options.DefaultPath);
            string uniqueFileName = $"{timestamp}_{originalFileName}";

            // Open a file stream for writing the resource
            var fileStream = new FileStream(uniqueFileName, FileMode.Create, FileAccess.Write);
            options.Stream = fileStream;
        }

        public void CloseStream(StreamProviderOptions options)
        {
            // Ensure the stream is properly closed after the resource is written
            options.Stream?.Close();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                var diagram = new Diagram("input.vsdx");

                // Configure HTML export options and assign the custom stream provider
                var htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new TimestampedStreamProvider();

                // Export the diagram to HTML; resources (images, CSS, etc.) will be saved with timestamped names
                diagram.Save("output.html", htmlOptions);

                Console.WriteLine("Diagram exported to HTML with timestamped resource files.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}