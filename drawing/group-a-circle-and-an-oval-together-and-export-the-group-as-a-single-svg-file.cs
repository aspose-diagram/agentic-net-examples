using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> <outputSvgFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page of the diagram.
                Page page = diagram.Pages[0];

                // Draw a circle (ellipse with equal width and height).
                // Parameters: pinX, pinY, width, height.
                long circleId = page.DrawEllipse(1.0, 1.0, 2.0, 2.0);

                // Draw an oval (ellipse with different width and height).
                long ovalId = page.DrawEllipse(4.0, 1.0, 3.0, 1.5);

                // Retrieve the Shape objects for the drawn ellipses.
                Shape circleShape = page.Shapes.GetShape(circleId);
                Shape ovalShape = page.Shapes.GetShape(ovalId);

                // Group the circle and oval together.
                // The Group method returns the new group as a Shape.
                Shape groupShape = page.Shapes.Group(new Shape[] { circleShape, ovalShape });

                // Export the group as a single SVG file.
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                groupShape.ToSvg(outputPath, svgOptions);
            }

            Console.WriteLine($"Group exported successfully to SVG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}