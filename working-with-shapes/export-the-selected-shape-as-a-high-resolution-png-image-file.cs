using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for the Visio diagram file path
        Console.Write("Enter the path to the Visio diagram file: ");
        string diagramPath = Console.ReadLine()?.Trim();

        // Validate diagram file existence
        if (string.IsNullOrEmpty(diagramPath) || !File.Exists(diagramPath))
        {
            Console.Error.WriteLine("Diagram file not found.");
            return;
        }

        // Prompt for the shape ID to export
        Console.Write("Enter the ID of the shape to export (numeric): ");
        string shapeIdInput = Console.ReadLine()?.Trim();
        if (!long.TryParse(shapeIdInput, out long shapeId))
        {
            Console.Error.WriteLine("Invalid shape ID.");
            return;
        }

        // Prompt for the output PNG file path
        Console.Write("Enter the output PNG file path: ");
        string outputPath = Console.ReadLine()?.Trim();

        // Validate output directory
        if (string.IsNullOrEmpty(outputPath))
        {
            Console.Error.WriteLine("Invalid output path.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Locate the shape with the specified ID across all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // GetShape throws if ID not present, so check existence via Shapes collection
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ID == shapeId)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null) break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found in the diagram.");
                return;
            }

            // Configure high‑resolution PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            pngOptions.Resolution = 300f; // DPI for high‑resolution output

            // Export the selected shape to PNG
            targetShape.ToImage(outputPath, pngOptions);

            Console.WriteLine($"Shape ID {shapeId} exported successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}