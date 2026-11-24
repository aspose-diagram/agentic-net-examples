using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the existing diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define shape parameters.
                double pinX = 2.0;               // Horizontal position (in inches)
                double pinY = 2.0;               // Vertical position (in inches)
                string masterName = "Rectangle"; // Master shape name; must exist in the stencil.

                // Add the shape to the active page. The method returns the structural shape ID (long).
                // The fourth parameter (isCalculate) is a plain bool, not BOOL.
                long shapeId = diagram.ActivePage.AddShape(pinX, pinY, masterName, false);

                // Retrieve the newly added shape using the returned ID.
                // GetShape expects an int; cast the long ID accordingly.
                Shape shape = diagram.ActivePage.Shapes.GetShape((int)shapeId);

                // Output basic placement information.
                Console.WriteLine("Added shape ID: " + shapeId);
                Console.WriteLine($"PinX: {shape.XForm.PinX.Value}");
                Console.WriteLine($"PinY: {shape.XForm.PinY.Value}");
                Console.WriteLine($"Width: {shape.XForm.Width.Value}");
                Console.WriteLine($"Height: {shape.XForm.Height.Value}");

                // Retrieve geometry data.
                // A shape can have multiple geometry sections; list each section's coordinate counts.
                Console.WriteLine($"Geometry sections count: {shape.Geoms.Count}");
                for (int i = 0; i < shape.Geoms.Count; i++)
                {
                    // Explicitly type the geometry section.
                    Aspose.Diagram.Geom geom = (Aspose.Diagram.Geom)shape.Geoms[i];
                    int moveToCount = geom.CoordinateCol.MoveToCol.Count;
                    int lineToCount = geom.CoordinateCol.LineToCol.Count;
                    int arcToCount = geom.CoordinateCol.ArcToCol.Count;
                    Console.WriteLine($"Geometry[{i}] - MoveTo: {moveToCount}, LineTo: {lineToCount}, ArcTo: {arcToCount}");
                }

                // Save the modified diagram to the output path using the correct SaveFileFormat enum.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine("An error occurred: " + ex.Message);
        }
    }
}