using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output Visio file path
        string outputPath = "rotated_output.vsdx";
        // Name of the shape to rotate (as it appears in the ShapeSheet NameU)
        string targetShapeName = "Rectangle";
        // Desired rotation angle in degrees
        double rotationAngle = 45.0;

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume we work with the first page; adjust if needed
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                var page = diagram.Pages[0];

                // Locate the shape by its NameU property
                Shape targetShape = null;
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];
                    if (shape.NameU == targetShapeName)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with NameU \"{targetShapeName}\" not found on the first page.");
                    return;
                }

                // Rotate the shape to the specified angle using SetAngle (degrees)
                targetShape.SetAngle(rotationAngle);

                // Save the modified diagram to the output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Shape \"{targetShapeName}\" rotated to {rotationAngle} degrees and saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}