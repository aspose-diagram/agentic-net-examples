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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Access the first page where the diamond will be added.
            Page page = diagram.Pages[0];

            // Define the points of a diamond shape.
            // The array is a flat list of X,Y coordinates.
            double[] diamondPoints = new double[]
            {
                1, 2,   // left middle
                2, 0,   // top middle
                3, 2,   // right middle
                2, 4,   // bottom middle
                1, 2    // close the polygon
            };

            // Draw the diamond polyline on the page.
            // The method returns the shape ID of the newly created shape.
            long shapeId = page.DrawPolyline(diamondPoints);

            // Retrieve the shape object using the returned ID.
            Shape diamondShape = page.Shapes.GetShape(shapeId);

            // Set the desired size: 2 inches width and 2 inches height.
            diamondShape.XForm.Width.Value = 2.0;
            diamondShape.XForm.Height.Value = 2.0;

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}