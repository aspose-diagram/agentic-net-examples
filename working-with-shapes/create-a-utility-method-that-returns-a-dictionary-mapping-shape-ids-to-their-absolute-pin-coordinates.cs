using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Retrieve the mapping of shape IDs to absolute pin coordinates.
                Dictionary<long, (double X, double Y)> pinMap = GetShapeAbsolutePins(diagram);

                // Output the results.
                Console.WriteLine("Shape ID -> Absolute Pin Coordinates:");
                foreach (var kvp in pinMap)
                {
                    Console.WriteLine($"Shape ID {kvp.Key}: X = {kvp.Value.X}, Y = {kvp.Value.Y}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Returns a dictionary that maps each shape's ID to its absolute PinX and PinY coordinates.
    /// For shapes inside groups, the coordinates are calculated by recursively applying parent transformations.
    /// </summary>
    /// <param name="diagram">The loaded Diagram instance.</param>
    /// <returns>Dictionary where key = shape ID (long) and value = tuple of (X, Y) coordinates.</returns>
    static Dictionary<long, (double X, double Y)> GetShapeAbsolutePins(Diagram diagram)
    {
        var result = new Dictionary<long, (double X, double Y)>();

        // Iterate through all pages in the diagram.
        foreach (Page page in diagram.Pages)
        {
            // Iterate through all shapes on the current page.
            foreach (Shape shape in page.Shapes)
            {
                // Skip logically deleted shapes.
                if (shape.Del == BOOL.True)
                    continue;

                // Compute absolute coordinates.
                (double X, double Y) absolute = ComputeAbsolutePin(shape);
                result[shape.ID] = absolute;
            }
        }

        return result;
    }

    /// <summary>
    /// Computes the absolute PinX and PinY for a given shape by walking up its parent hierarchy.
    /// </summary>
    /// <param name="shape">The shape whose absolute pin is required.</param>
    /// <returns>Tuple containing absolute X and Y coordinates.</returns>
    static (double X, double Y) ComputeAbsolutePin(Shape shape)
    {
        // Start with the shape's own PinX and PinY values.
        double x = shape.XForm.PinX.Value;
        double y = shape.XForm.PinY.Value;

        // Walk up the parent chain, applying each parent's rotation and translation.
        Shape parent = shape.ParentShape;
        while (parent != null && parent.Del != BOOL.True)
        {
            // Parent's Pin (origin) coordinates.
            double parentX = parent.XForm.PinX.Value;
            double parentY = parent.XForm.PinY.Value;

            // Parent's rotation angle in degrees.
            double angleDeg = parent.XForm.Angle.Value;
            double rad = angleDeg * Math.PI / 180.0;

            // Rotate the current point around the origin (0,0).
            double rotatedX = x * Math.Cos(rad) - y * Math.Sin(rad);
            double rotatedY = x * Math.Sin(rad) + y * Math.Cos(rad);

            // Translate by the parent's Pin position.
            x = parentX + rotatedX;
            y = parentY + rotatedY;

            // Move to the next ancestor.
            parent = parent.ParentShape;
        }

        return (x, y);
    }
}