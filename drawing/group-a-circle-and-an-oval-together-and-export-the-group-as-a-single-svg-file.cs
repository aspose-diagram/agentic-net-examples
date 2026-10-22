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
            Console.Error.WriteLine("Usage: Program <inputVisioFile.vsdx> <outputGroup.svg>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page of the diagram.
            Page page = diagram.Pages[0];

            // Draw a circle (ellipse with equal width and height).
            long circleId = page.DrawEllipse(1.0, 1.0, 2.0, 2.0);

            // Draw an oval (ellipse with different width and height).
            long ovalId = page.DrawEllipse(4.0, 1.0, 3.0, 2.0);

            // Retrieve the Shape objects for the drawn ellipse and oval.
            Shape circleShape = page.Shapes.GetShape(circleId);
            Shape ovalShape = page.Shapes.GetShape(ovalId);

            // Group the circle and oval together.
            Shape[] shapesToGroup = new Shape[] { circleShape, ovalShape };
            Shape groupShape = page.Shapes.Group(shapesToGroup);

            // Prepare SVG export options (default configuration).
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Export the group as a single SVG file.
            groupShape.ToSvg(outputPath, svgOptions);

            Console.WriteLine($"Group exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}