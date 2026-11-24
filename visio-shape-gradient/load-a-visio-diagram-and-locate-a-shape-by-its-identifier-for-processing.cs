using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and shape identifier (long).
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <shapeId>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];

        // Guard: verify the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Parse the shape identifier.
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape identifier '{shapeIdArg}'. Must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }

            // For simplicity, search all pages for the shape with the given ID.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // Attempt to retrieve the shape by ID from the current page.
                // GetShape returns the shape or throws if not found; we catch and continue.
                try
                {
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                catch
                {
                    // Shape not found on this page; continue searching.
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found in any page.");
                return;
            }

            // Check if the shape is logically deleted.
            if (targetShape.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Warning: Shape with ID {shapeId} is marked as deleted.");
            }

            // Output some basic information about the located shape.
            Console.WriteLine($"Shape ID: {targetShape.ID}");
            Console.WriteLine($"NameU: {targetShape.NameU}");
            Console.WriteLine($"Name: {targetShape.Name}");
            Console.WriteLine($"Master Name: {targetShape.Master?.Name ?? "N/A"}");
            Console.WriteLine($"Deleted flag: {targetShape.Del}");

            // Example processing: change the shape's width and height if not deleted.
            if (targetShape.Del == BOOL.False)
            {
                // Set new dimensions (example values).
                targetShape.XForm.Width.Value = 2.0;
                targetShape.XForm.Height.Value = 1.0;
                Console.WriteLine("Shape dimensions updated.");
            }

            // Optionally, save the modified diagram to a new file.
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", 
                                             Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}