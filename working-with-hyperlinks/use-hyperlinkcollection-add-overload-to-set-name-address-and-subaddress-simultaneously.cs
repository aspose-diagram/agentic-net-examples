using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the source Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Find the first non‑deleted shape on the page.
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No visible shape found on the first page.");
                return;
            }

            // Create a new Hyperlink instance and set its properties.
            Hyperlink hyperlink = new Hyperlink();
            // Set the display name (identifier) of the hyperlink.
            hyperlink.Name = "Visit Aspose";
            // Set the external address (URL).
            hyperlink.Address.Value = "https://www.aspose.com";
            // Set the sub‑address (e.g., bookmark or page reference).
            hyperlink.SubAddress.Value = "section1";
            // Optionally set a description (tooltip) for the hyperlink.
            hyperlink.Description.Value = "Open Aspose website";

            // Add the configured hyperlink to the shape's Hyperlinks collection.
            targetShape.Hyperlinks.Add(hyperlink);

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}