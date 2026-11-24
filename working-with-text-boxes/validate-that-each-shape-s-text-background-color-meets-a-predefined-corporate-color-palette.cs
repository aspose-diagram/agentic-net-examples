using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio diagram file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No diagram file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Define the corporate color palette (hex strings or RGB strings as used by Visio).
        // Adjust the values to match the actual corporate palette.
        var corporatePalette = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "#FFFFFF", // White
            "#000000", // Black
            "#FF0000", // Red
            "#00FF00", // Green
            "#0000FF", // Blue
            "RGB(95,108,53)" // Example corporate RGB format
        };

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the text background color.
                    // The TextBlock section holds the background color in TextBkgnd.
                    // It can be represented either as a hex string or an RGB() string.
                    string bgColor = shape.TextBlock?.TextBkgnd?.Value;

                    // If there is no background color defined, treat it as empty.
                    if (string.IsNullOrWhiteSpace(bgColor))
                    {
                        Console.WriteLine($"Shape ID {shape.ID} on page {page.Name} has no text background color defined.");
                        continue;
                    }

                    // Validate against the corporate palette.
                    if (!corporatePalette.Contains(bgColor.Trim()))
                    {
                        Console.WriteLine($"[Violation] Shape ID {shape.ID} on page '{page.Name}' uses non‑compliant text background color: {bgColor}");
                    }
                    else
                    {
                        // Optional: report compliant shapes (can be omitted for brevity).
                        // Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' complies with palette.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}