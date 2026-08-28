using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExport
{
    // Custom stream provider that prefixes each resource file name with a timestamp.
    public class TimestampStreamProvider : IStreamProvider
    {
        // Called when Aspose.Diagram needs a stream for a resource.
        public void InitStream(StreamProviderOptions options)
        {
            // Build a unique file name using the current timestamp.
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string originalFileName = Path.GetFileName(options.DefaultPath);
            string directory = Path.GetDirectoryName(options.DefaultPath) ?? string.Empty;
            string newFileName = $"{timestamp}_{originalFileName}";
            string fullPath = Path.Combine(directory, newFileName);

            // Create a writable file stream for the resource.
            options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        }

        // Called after Aspose.Diagram finishes writing the resource.
        public void CloseStream(StreamProviderOptions options)
        {
            if (options.Stream != null)
            {
                options.Stream.Dispose();
                options.Stream = null;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML export options with the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new TimestampStreamProvider();

                // Export the diagram to HTML. Resources will be saved with timestamped names.
                string outputPath = "output.html";
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine("Diagram exported to HTML with timestamped resource files.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}