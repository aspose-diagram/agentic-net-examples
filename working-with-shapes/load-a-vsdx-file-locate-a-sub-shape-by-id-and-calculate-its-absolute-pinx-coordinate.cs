using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (VSDX)
        string inputPath = "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Assume the shape we are interested in is on the first page
            Page page = diagram.Pages[0];

            // ID of the sub‑shape (replace with the actual ID)
            long subShapeId = 12345;

            // Retrieve the shape by its ID
            Shape subShape = page.Shapes.GetShape(subShapeId);

            // Calculate the absolute PinX coordinate of the sub‑shape
            double absolutePinX = GetAbsolutePinX(subShape);

            // Output the result
            Console.WriteLine($"Absolute PinX of shape ID {subShapeId}: {absolutePinX}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Recursively computes the absolute PinX by adding parent offsets.
    // This simple implementation adds the PinX of each ancestor shape.
    // For complex groups with rotation, a full matrix transformation would be required.
    private static double GetAbsolutePinX(Shape shape)
    {
        double pinX = shape.XForm.PinX.Value; // Base PinX of the shape

        // Traverse up the hierarchy, adding each parent shape's PinX
        Shape? parent = shape.ParentShape;
        while (parent != null)
        {
            pinX += parent.XForm.PinX.Value;
            parent = parent.ParentShape;
        }

        return pinX;
    }
}