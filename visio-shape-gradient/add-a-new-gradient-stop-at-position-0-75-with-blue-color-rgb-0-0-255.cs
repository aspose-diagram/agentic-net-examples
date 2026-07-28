using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page (PinX, PinY, Width, Height in inches)
            double pinX = 5.0;
            double pinY = 5.0;
            double width = 3.0;
            double height = 2.0;
            long rectId = page.DrawRectangle(pinX, pinY, width, height);

            // Retrieve the shape object using its ID (cast to int as required)
            Shape shape = page.Shapes.GetShape((int)rectId);

            // Enable gradient fill: set fill pattern, enable gradient, and set direction
            shape.Fill.FillPattern.Value = 25;                         // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True; // Turn on gradient
            // GradientDir.Value expects an int; cast the enum to int
            shape.Fill.GradientFill.GradientDir.Value = (int)GradientFillDir.Linear; // Linear gradient

            // Clear any existing gradient stops
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add a new gradient stop at position 0.75 with blue color (RGB 0,0,255)
            // Position is a fraction (0‑1) using MeasureConst.NUM
            // Color is specified as a hex string using ColorValue
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.75, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));

            // Save the diagram to a VSDX file
            diagram.Save("GradientExample.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}