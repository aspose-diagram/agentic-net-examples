using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram using a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Ensure the page contains at least one shape
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Get the first shape on the page
                Shape shape = page.Shapes[0];

                // TODO: Set a custom theme color on the shape.
                // The Aspose.Diagram API version used here does not expose a ThemeColor property on Shape.
                // Use the appropriate method or property provided by the library to apply a theme color,
                // such as setting fill or line colors, or applying a preset theme style if available.
                // Example (if ThemeColor were available):
                // shape.ThemeColor = "#FF5733";

                // Save the modified diagram to the output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}