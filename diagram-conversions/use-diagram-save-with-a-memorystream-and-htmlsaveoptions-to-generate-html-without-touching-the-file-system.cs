using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure HTML export options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // ExportHiddenPage expects a plain bool, not BOOL
                    ExportHiddenPage = false
                };

                // Assign a custom stream provider to handle embedded resources in memory
                htmlOptions.StreamProvider = new MemoryStreamProvider();

                // Save the diagram to a memory stream as HTML
                using (MemoryStream outputStream = new MemoryStream())
                {
                    try
                    {
                        // Save using the overload that accepts a stream and save options
                        diagram.Save(outputStream, htmlOptions);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error during diagram save: {ex.Message}");
                        return;
                    }

                    // Reset stream position to read the generated HTML
                    outputStream.Position = 0;
                    using (StreamReader reader = new StreamReader(outputStream))
                    {
                        string htmlContent = reader.ReadToEnd();
                        // Output the HTML content to the console (or further processing)
                        Console.WriteLine(htmlContent);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}

// Custom IStreamProvider implementation that supplies in‑memory streams for resources
class MemoryStreamProvider : IStreamProvider
{
    // Called by Aspose.Diagram when a new resource stream is needed
    public void InitStream(StreamProviderOptions options)
    {
        // Provide a fresh MemoryStream for the resource
        options.Stream = new MemoryStream();
    }

    // Called when the resource stream is no longer needed
    public void CloseStream(StreamProviderOptions options)
    {
        // Dispose the stream if it was created
        if (options.Stream != null)
        {
            options.Stream.Dispose();
        }
    }
}