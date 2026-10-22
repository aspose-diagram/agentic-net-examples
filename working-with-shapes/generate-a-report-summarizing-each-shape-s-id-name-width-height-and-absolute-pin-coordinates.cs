using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve basic shape information.
                    long shapeId = shape.ID;
                    string shapeName = shape.NameU;
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;

                    // Compute absolute pin coordinates (handling group shapes recursively).
                    double absolutePinX = GetAbsolutePinX(shape);
                    double absolutePinY = GetAbsolutePinY(shape);

                    // Output the shape report line.
                    Console.WriteLine($"ID: {shapeId}, Name: {shapeName}, Width: {width}, Height: {height}, PinX: {absolutePinX}, PinY: {absolutePinY}");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Recursively calculates the absolute PinX for a shape, accounting for parent groups.
    private static double GetAbsolutePinX(Shape shape)
    {
        // If the shape has no parent, its PinX is already absolute.
        if (shape.ParentShape == null)
        {
            return shape.XForm.PinX.Value;
        }

        // Add the parent's absolute PinX to the shape's local PinX.
        double parentAbsX = GetAbsolutePinX(shape.ParentShape);
        return parentAbsX + shape.XForm.PinX.Value;
    }

    // Recursively calculates the absolute PinY for a shape, accounting for parent groups.
    private static double GetAbsolutePinY(Shape shape)
    {
        // If the shape has no parent, its PinY is already absolute.
        if (shape.ParentShape == null)
        {
            return shape.XForm.PinY.Value;
        }

        // Add the parent's absolute PinY to the shape's local PinY.
        double parentAbsY = GetAbsolutePinY(shape.ParentShape);
        return parentAbsY + shape.XForm.PinY.Value;
    }
}