using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Prepare a memory stream that will receive the HTML output.
            using (MemoryStream htmlStream = new MemoryStream())
            {
                // Configure HTML export options.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Do not export hidden pages.
                    ExportHiddenPage = false,
                    // Do not export comments.
                    IsExportComments = false,
                    // Provide a custom stream provider for embedded resources.
                    StreamProvider = new MemoryStreamProvider()
                };

                // Save the diagram as HTML into the memory stream.
                diagram.Save(htmlStream, htmlOptions);

                // Reset the stream position to read the generated HTML.
                htmlStream.Position = 0;
                using (StreamReader reader = new StreamReader(htmlStream))
                {
                    string htmlContent = reader.ReadToEnd();
                    // Output the HTML content to the console (no file system usage).
                    Console.WriteLine(htmlContent);
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

// Custom implementation of IStreamProvider that supplies a MemoryStream for each resource.
class MemoryStreamProvider : IStreamProvider
{
    // Called by Aspose.Diagram when a new resource stream is required.
    public void InitStream(StreamProviderOptions options)
    {
        // Provide a fresh MemoryStream for the resource.
        options.Stream = new MemoryStream();
    }

    // Called by Aspose.Diagram after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // Dispose the stream to release resources.
        options.Stream?.Dispose();
    }
}