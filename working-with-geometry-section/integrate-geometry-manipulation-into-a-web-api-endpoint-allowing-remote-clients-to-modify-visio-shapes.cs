using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expecting five arguments:
        // 0 - input Visio file path
        // 1 - output Visio file path
        // 2 - shape ID (numeric)
        // 3 - new X coordinate for a LineTo geometry segment (double)
        // 4 - new Y coordinate for a LineTo geometry segment (double)
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: <inputPath> <outputPath> <shapeId> <newX> <newY>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];
        string newXArg = args[3];
        string newYArg = args[4];

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        // Parse new X coordinate
        if (!double.TryParse(newXArg, out double newX))
        {
            Console.Error.WriteLine($"Invalid X coordinate: {newXArg}");
            return;
        }

        // Parse new Y coordinate
        if (!double.TryParse(newYArg, out double newY))
        {
            Console.Error.WriteLine($"Invalid Y coordinate: {newYArg}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Retrieve the target shape by ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                    return;
                }

                // Ensure the shape has at least one geometry section
                if (shape.Geoms.Count == 0)
                {
                    Console.Error.WriteLine($"Shape ID {shapeId} has no geometry sections.");
                    return;
                }

                // Access the first geometry (Geom) object
                Geom geom = (Geom)shape.Geoms[0];

                // Create a new LineTo segment with the provided coordinates
                LineTo lineTo = new LineTo();
                lineTo.X.Value = newX;
                lineTo.Y.Value = newY;

                // Append the new LineTo segment to the geometry's coordinate collection
                geom.CoordinateCol.Add(lineTo);

                // Refresh shape data to apply geometry changes
                shape.RefreshData();

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}