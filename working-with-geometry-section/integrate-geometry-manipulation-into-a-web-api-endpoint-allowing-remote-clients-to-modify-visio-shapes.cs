using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, shape ID, optional output file.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <shapeId> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string outputPath = args.Length >= 3
            ? args[2]
            : Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID (Visio shape IDs are long).
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page (index 0).
                Page page = diagram.Pages[0];

                // Retrieve the target shape by its ID.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                    return;
                }

                // Ensure the shape has at least one geometry section.
                if (shape.Geoms.Count == 0)
                {
                    Console.Error.WriteLine($"Shape ID {shapeId} has no geometry sections.");
                    return;
                }

                // Access the first geometry (type Geom).
                Geom geometry = (Geom)shape.Geoms[0];

                // Ensure there is at least one LineTo command in the geometry.
                if (geometry.CoordinateCol.LineToCol.Count == 0)
                {
                    Console.Error.WriteLine($"Geometry of shape ID {shapeId} contains no LineTo commands.");
                    return;
                }

                // Modify the X coordinate of the first LineTo command (shift by +1.0 units).
                double originalX = geometry.CoordinateCol.LineToCol[0].X.Value;
                double newX = originalX + 1.0;
                geometry.CoordinateCol.LineToCol[0].X.Value = newX;

                // Save the modified diagram to the output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}