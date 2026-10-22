using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Define the directory containing the source Visio file.
        string dataDir = "Data"; // Adjust this path as needed.

        // Define the input Visio file path.
        string inputPath = Path.Combine(dataDir, "Drawing1.vsdx");

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define the output image file path.
        string outputPath = Path.Combine(dataDir, "TriangleWithShadow.png");

        try
        {
            // Load the existing Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram.
            Page page = diagram.Pages[0];

            // Define the vertices of the triangle (x1,y1, x2,y2, x3,y3, x1,y1 to close).
            double[] trianglePoints = new double[] { 1, 1, 5, 1, 3, 4, 1, 1 };

            // Draw the triangle on the page; returns the shape ID.
            long triangleShapeId = page.DrawPolyline(trianglePoints);

            // Retrieve the newly created triangle shape.
            Shape triangle = page.Shapes.GetShape(triangleShapeId);

            // Apply a simple drop shadow with default parameters.
            // Set shadow type to Simple.
            triangle.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
            // Set shadow color to black.
            triangle.Fill.ShdwForegnd.Value = "#000000";
            // Set shadow transparency to 30% (0.3). Adjust as needed.
            triangle.Fill.ShdwForegndTrans.Value = 0.3;
            // Optional: set default shadow offsets (can be omitted for defaults).
            triangle.Fill.ShapeShdwOffsetX.Value = 0.1;
            triangle.Fill.ShapeShdwOffsetY.Value = 0.1;

            // Prepare image save options for PNG format.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Save the diagram (with the shadowed triangle) as a PNG image.
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Triangle with shadow saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}