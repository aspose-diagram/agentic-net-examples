using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExport
{
    // Custom stream provider that prefixes each resource file name with a timestamp.
    public class TimestampedStreamProvider : IStreamProvider
    {
        // Called when a new stream is required for a resource.
        public void InitStream(StreamProviderOptions options)
        {
            // Generate a unique file name using the current timestamp and the original default path.
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string fileName = $"{timestamp}_{Path.GetFileName(options.DefaultPath)}";

            // Combine with the system temporary folder to avoid cluttering the working directory.
            string fullPath = Path.Combine(Path.GetTempPath(), fileName);

            // Create a writable file stream and assign it to the options.
            options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        }

        // Called when the stream is no longer needed.
        public void CloseStream(StreamProviderOptions options)
        {
            // Ensure the stream is properly closed and disposed.
            if (options.Stream != null)
            {
                options.Stream.Dispose();
                options.Stream = null;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio diagram.
                string inputPath = "sample.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML save options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new TimestampedStreamProvider()
                };

                // Export the diagram to HTML. Resources (images, CSS, etc.) will be saved with timestamped names.
                diagram.Save("output.html", htmlOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}