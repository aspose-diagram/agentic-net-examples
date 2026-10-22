using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output Visio file path
        string outputPath = "output.vsdx";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new blank diagram
            using (Diagram diagram = new Diagram())
            {
                // Get the active page where the shape will be added
                Page page = diagram.ActivePage;

                // Add a rectangle shape at position (2.0, 3.0) inches.
                // The fourth parameter (isCalculate) must be a plain bool.
                long shapeId = page.AddShape(2.0, 3.0, "Rectangle", false);

                // Retrieve the newly added shape using its ID (cast to int as required)
                Shape shape = page.Shapes.GetShape((int)shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine("Failed to retrieve the newly added shape.");
                    return;
                }

                // Output basic placement information
                Console.WriteLine($"Shape ID: {shape.ID}");
                Console.WriteLine($"PinX: {shape.XForm.PinX.Value}");
                Console.WriteLine($"PinY: {shape.XForm.PinY.Value}");
                Console.WriteLine($"Width: {shape.XForm.Width.Value}");
                Console.WriteLine($"Height: {shape.XForm.Height.Value}");

                // Retrieve geometry data to confirm correct placement
                if (shape.Geoms.Count > 0)
                {
                    // The geometry objects are of type Geom; cast the first entry
                    Geom geom = (Geom)shape.Geoms[0];
                    var coordCol = geom.CoordinateCol;

                    // Output the first MoveTo coordinate if present
                    if (coordCol.MoveToCol.Count > 0)
                    {
                        MoveTo moveTo = coordCol.MoveToCol[0];
                        Console.WriteLine("First MoveTo coordinate:");
                        Console.WriteLine($"  X: {moveTo.X.Value}");
                        Console.WriteLine($"  Y: {moveTo.Y.Value}");
                    }
                    else
                    {
                        Console.WriteLine("No MoveTo coordinates found in geometry.");
                    }

                    // Output the first LineTo coordinate if present
                    if (coordCol.LineToCol.Count > 0)
                    {
                        LineTo lineTo = coordCol.LineToCol[0];
                        Console.WriteLine("First LineTo coordinate:");
                        Console.WriteLine($"  X: {lineTo.X.Value}");
                        Console.WriteLine($"  Y: {lineTo.Y.Value}");
                    }
                    else
                    {
                        Console.WriteLine("No LineTo coordinates found in geometry.");
                    }
                }
                else
                {
                    Console.WriteLine("Shape has no geometry sections.");
                }

                // Save the diagram to verify that the shape was added correctly
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}