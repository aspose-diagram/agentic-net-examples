using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output HTML file path
        string outputPath = "output.html";

        try
        {
            // Load the Visio diagram within a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure HTML save options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Optional: do not export hidden pages
                htmlOptions.ExportHiddenPage = false; // BOOL.False is not compatible; use native bool

                // Save the diagram as HTML with the configured options
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine($"Diagram successfully saved to HTML: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
