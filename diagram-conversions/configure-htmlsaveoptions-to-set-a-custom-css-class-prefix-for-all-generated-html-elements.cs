using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.html";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure HTML save options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Export hidden pages are disabled (default behavior)
                    ExportHiddenPage = false // Changed from BOOL.False to bool
                };

                // NOTE: Aspose.Diagram does not provide a property to set a custom CSS class prefix.
                // This feature is not supported, so the default CSS class naming is used.

                // Assign a custom stream provider if you need to control resource URLs.
                // The implementation below demonstrates how to attach the provider.
                htmlOptions.StreamProvider = new CustomStreamProvider();

                // Save the diagram as HTML using the configured options
                diagram.Save(outputPath, htmlOptions);
                Console.WriteLine($"Diagram successfully saved to HTML at: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

// Implementation of IStreamProvider for HTML export scenarios.
// This example does not modify streams but shows the required method signatures.
class CustomStreamProvider : IStreamProvider
{
    // Called before a resource stream is created.
    public void InitStream(StreamProviderOptions options)
    {
        // No custom stream handling is required for this example.
        // If needed, you could assign a custom Stream to options.Stream here.
    }

    // Called after the resource stream is closed.
    public void CloseStream(StreamProviderOptions options)
    {
        // No cleanup is required for this example.
    }
}
