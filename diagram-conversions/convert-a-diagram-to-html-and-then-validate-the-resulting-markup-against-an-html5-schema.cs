using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output HTML file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Convert the Visio diagram to HTML.
        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create HTML save options (default PNG images for shapes).
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Save the diagram as HTML.
                diagram.Save(outputPath, htmlOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram conversion: {ex.Message}");
            return;
        }

        // Simple validation of the generated HTML against basic HTML5 expectations.
        try
        {
            if (!File.Exists(outputPath))
            {
                Console.Error.WriteLine($"Error: Expected HTML output file \"{outputPath}\" was not created.");
                return;
            }

            string htmlContent = File.ReadAllText(outputPath);

            // Basic checks: presence of DOCTYPE and root html tags.
            bool hasDoctype = htmlContent.IndexOf("<!DOCTYPE html>", StringComparison.OrdinalIgnoreCase) >= 0;
            bool hasHtmlTag = htmlContent.IndexOf("<html", StringComparison.OrdinalIgnoreCase) >= 0 &&
                              htmlContent.IndexOf("</html>", StringComparison.OrdinalIgnoreCase) >= 0;

            if (!hasDoctype || !hasHtmlTag)
            {
                Console.Error.WriteLine("HTML validation failed: Missing required HTML5 DOCTYPE or root <html> element.");
                return;
            }

            // If needed, additional validation logic could be added here.

            Console.WriteLine("HTML conversion succeeded and basic validation passed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML validation: {ex.Message}");
        }
    }
}