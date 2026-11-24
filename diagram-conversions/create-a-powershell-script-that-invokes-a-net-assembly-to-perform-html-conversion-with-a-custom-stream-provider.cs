using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: DiagramHtmlExport <inputVisioFile> <outputHtmlFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure HTML save options with a custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new CustomStreamProvider();

                // Export the diagram to HTML using the configured options.
                diagram.Save(outputPath, htmlOptions);
                Console.WriteLine($"Diagram successfully exported to HTML: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error during HTML conversion: {ex.Message}");
        }
    }
}

// Custom stream provider implementation for HTML export.
class CustomStreamProvider : IStreamProvider
{
    // Called by Aspose.Diagram when a new stream is required.
    public void InitStream(StreamProviderOptions options)
    {
        // Create a memory stream to hold the resource data.
        options.Stream = new MemoryStream();

        // Set a custom base URL/path for the exported resources.
        // This URL will be used in the generated HTML to reference images, CSS, etc.
        options.CustomPath = "custom/base/url/";
    }

    // Called by Aspose.Diagram after the stream is no longer needed.
    public void CloseStream(StreamProviderOptions options)
    {
        // Dispose the stream if it was created.
        if (options.Stream != null)
        {
            options.Stream.Dispose();
        }
    }
}