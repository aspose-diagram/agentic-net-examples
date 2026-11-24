using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <shapeId> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string outputPath = args.Length > 2
            ? args[2]
            : Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_updated.vsdx");

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse the shape ID (Visio shape IDs are long)
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (mandatory disposal pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool shapeFound = false;

                // Search each page for the shape with the specified ID
                foreach (Page page in diagram.Pages)
                {
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                    {
                        // Remove the shape from the page's shape collection
                        // Remove returns void in the current API version
                        page.Shapes.Remove(shape);
                        Console.WriteLine($"Shape with ID {shapeId} removed from page \"{page.Name}\".");
                        shapeFound = true;
                        break; // Shape IDs are unique across the diagram
                    }
                }

                if (!shapeFound)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found in the diagram.");
                    return;
                }

                // Save the modified diagram to the output path using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}