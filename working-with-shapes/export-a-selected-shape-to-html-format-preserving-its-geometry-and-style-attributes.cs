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
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <ShapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdText = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Guard: parse the shape identifier.
        if (!long.TryParse(shapeIdText, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdText}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Locate the shape across all pages.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // GetShape returns null if the shape ID is not present on this page.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape != null)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found in the diagram.");
                return;
            }

            // Prepare output HTML file path.
            string outputDirectory = Path.GetDirectoryName(diagramPath) ?? Directory.GetCurrentDirectory();
            string outputPath = Path.Combine(outputDirectory, $"shape_{shapeId}.html");

            // Export the selected shape to HTML, preserving geometry and style.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            targetShape.ToHTML(outputPath, htmlOptions);

            Console.WriteLine($"Shape {shapeId} exported successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}