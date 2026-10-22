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

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the existing diagram.
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0).
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page.
            // Parameters: pinX, pinY, master name, isCalculate (false to skip auto‑layout).
            long rectId = page.AddShape(2.0, 2.0, "Rectangle", false);

            // Retrieve the shape object using its ID.
            Shape rectShape = page.Shapes.GetShape((int)rectId);

            // Rotate the rectangle 45 degrees around its center.
            // SetAngle rotates about the shape's center and does not change its position.
            rectShape.SetAngle(45);

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}