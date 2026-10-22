using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // Initialize the stream for HTML resources.
    public void InitStream(StreamProviderOptions options)
    {
        // Set a custom base URL or path for resources referenced in the HTML.
        // This value will be used by Aspose.Diagram when generating resource links.
        options.CustomPath = "custom_resources/";
        // No additional stream handling is required for this example.
    }

    // Clean up any resources if necessary.
    public void CloseStream(StreamProviderOptions options)
    {
        // No cleanup needed for this simple implementation.
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard against missing output directory.
        if (!Directory.Exists(outputFolder))
        {
            Console.Error.WriteLine($"Output folder does not exist: {outputFolder}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create an instance of the custom stream provider.
            IStreamProvider streamProvider = new CustomStreamProvider();

            // Export each page as a separate HTML file.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure HTML save options for a single page.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.PageIndex = i;      // Zero‑based page index.
                htmlOptions.PageCount = 1;      // Export only the current page.
                htmlOptions.StreamProvider = streamProvider; // Assign custom provider.

                // Build the output file path.
                string outputPath = Path.Combine(outputFolder, $"page_{i + 1}.html");

                // Save the page to HTML.
                diagram.Save(outputPath, htmlOptions);
                Console.WriteLine($"Saved page {i + 1} to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}