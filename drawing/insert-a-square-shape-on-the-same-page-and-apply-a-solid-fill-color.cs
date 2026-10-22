using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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
            // Load the existing Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page of the diagram.
            Page page = diagram.Pages[0];

            // Draw a square (rectangle with equal width and height) at position (1,1) with size 2x2.
            // The method returns the shape ID (long).
            long squareShapeId = page.DrawRectangle(1.0, 1.0, 2.0, 2.0);

            // Retrieve the Shape object using the returned ID.
            Shape squareShape = page.Shapes.GetShape(squareShapeId);

            // Apply a solid fill pattern.
            // FillPattern.Value = 1 corresponds to a solid fill.
            squareShape.Fill.FillPattern.Value = 1;

            // Set the fill foreground color to a solid green (hex format).
            squareShape.Fill.FillForegnd.Value = "#00FF00";

            // Ensure the fill is fully opaque (0 = opaque, 1 = fully transparent).
            squareShape.Fill.FillForegndTrans.Value = 0;

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