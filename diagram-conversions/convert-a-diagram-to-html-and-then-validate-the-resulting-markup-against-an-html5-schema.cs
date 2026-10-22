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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlPath>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare HTML export options (default settings).
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Export the entire diagram to a single HTML file.
            diagram.Save(outputPath, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram conversion: {ex.Message}");
            return;
        }

        // Simple HTML5 validation: check for required DOCTYPE and root tags.
        try
        {
            string htmlContent = File.ReadAllText(outputPath);

            bool hasDoctype = htmlContent.IndexOf("<!DOCTYPE html>", StringComparison.OrdinalIgnoreCase) >= 0;
            bool hasHtmlOpen = htmlContent.IndexOf("<html", StringComparison.OrdinalIgnoreCase) >= 0;
            bool hasHtmlClose = htmlContent.IndexOf("</html>", StringComparison.OrdinalIgnoreCase) >= 0;

            if (hasDoctype && hasHtmlOpen && hasHtmlClose)
            {
                Console.WriteLine("HTML validation passed.");
            }
            else
            {
                Console.Error.WriteLine("HTML validation failed: missing required HTML5 elements.");
                // Optionally, you could throw an exception to indicate failure.
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML validation: {ex.Message}");
        }
    }
}