using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <shapeId>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse the shape identifier.
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume we work with the first page; adjust if needed.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];

                // Retrieve the target shape by its structural ID.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                    return;
                }

                // ------------------------------------------------------------
                // Create a new Geometry (Geom) instance.
                // ------------------------------------------------------------
                Geom newGeom = new Geom();

                // ------------------------------------------------------------
                // Define custom vertices for the geometry.
                // Example: a simple rectangle defined by MoveTo and four LineTo segments.
                // ------------------------------------------------------------

                // MoveTo (0,0)
                MoveTo move = new MoveTo();
                move.X.Value = 0.0;
                move.Y.Value = 0.0;
                newGeom.CoordinateCol.Add(move);

                // LineTo (2,0)
                LineTo line1 = new LineTo();
                line1.X.Value = 2.0;
                line1.Y.Value = 0.0;
                newGeom.CoordinateCol.Add(line1);

                // LineTo (2,2)
                LineTo line2 = new LineTo();
                line2.X.Value = 2.0;
                line2.Y.Value = 2.0;
                newGeom.CoordinateCol.Add(line2);

                // LineTo (0,2)
                LineTo line3 = new LineTo();
                line3.X.Value = 0.0;
                line3.Y.Value = 2.0;
                newGeom.CoordinateCol.Add(line3);

                // LineTo (0,0) – close the rectangle
                LineTo line4 = new LineTo();
                line4.X.Value = 0.0;
                line4.Y.Value = 0.0;
                newGeom.CoordinateCol.Add(line4);

                // ------------------------------------------------------------
                // Add the new geometry to the shape's Geoms collection.
                // ------------------------------------------------------------
                shape.Geoms.Add(newGeom);

                // ------------------------------------------------------------
                // Save the modified diagram.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}