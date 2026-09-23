using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the existing diagram
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Locate the target shape by its universal name (case‑insensitive)
        Shape targetShape = null;
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (!string.IsNullOrEmpty(shape.NameU) &&
                    shape.NameU.Equals("Rectangle", StringComparison.OrdinalIgnoreCase))
                {
                    targetShape = shape;
                    break;
                }
            }
            if (targetShape != null) break;
        }

        if (targetShape == null)
        {
            Console.Error.WriteLine("Target shape not found.");
            return;
        }

        // Desired new dimensions (in inches)
        double newWidth = 3.0;
        double newHeight = 2.0;

        // Original dimensions from the shape's XForm
        double origWidth = targetShape.XForm.Width.Value;
        double origHeight = targetShape.XForm.Height.Value;

        if (origWidth == 0 || origHeight == 0)
        {
            Console.Error.WriteLine("Original shape dimensions are zero; cannot scale.");
            return;
        }

        // Scaling factors for X and Y axes
        double scaleX = newWidth / origWidth;
        double scaleY = newHeight / origHeight;

        // Update geometry vertices in the first geometry section, if present
        if (targetShape.Geoms.Count > 0)
        {
            Geom geom = (Geom)targetShape.Geoms[0];
            foreach (object segment in geom.CoordinateCol)
            {
                if (segment is MoveTo move)
                {
                    // Scale MoveTo coordinates
                    move.X.Value *= scaleX;
                    move.Y.Value *= scaleY;
                }
                else if (segment is LineTo line)
                {
                    // Scale LineTo coordinates
                    line.X.Value *= scaleX;
                    line.Y.Value *= scaleY;
                }
                else if (segment is ArcTo arc)
                {
                    // Scale ArcTo end point; radius scaling is optional and omitted
                    arc.X.Value *= scaleX;
                    arc.Y.Value *= scaleY;
                }
                // Additional segment types (e.g., EllipticalArcTo) can be handled similarly if needed
            }
        }

        // Apply the new width and height to the shape's XForm
        targetShape.XForm.Width.Value = newWidth;
        targetShape.XForm.Height.Value = newHeight;

        // Save the modified diagram
        string outputPath = "output.vsdx";
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}