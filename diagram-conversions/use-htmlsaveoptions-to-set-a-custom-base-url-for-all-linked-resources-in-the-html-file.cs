using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // Initialize the stream provider with a custom base URL for linked resources.
    public void InitStream(StreamProviderOptions options)
    {
        // Set the custom base URL that will be prefixed to all resource links in the generated HTML.
        options.CustomPath = "https://example.com/resources/";
        // No stream is assigned because resources are referenced via the custom URL.
    }

    // CloseStream is required by the interface but no action is needed for this scenario.
    public void CloseStream(StreamProviderOptions options)
    {
        // No resources to release.
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputHtmlFile>");
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
                // Configure HTML save options.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Do not export hidden pages.
                    ExportHiddenPage = false,
                    // Assign the custom stream provider to control resource URLs.
                    StreamProvider = new CustomStreamProvider()
                };

                // Save the diagram to HTML using the configured options.
                diagram.Save(outputPath, htmlOptions);
                Console.WriteLine($"Diagram successfully saved to HTML: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}