using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new diagram (contains a default page)
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Draw a rectangle shape on the page
            // Parameters: pinX, pinY, width, height (all in inches)
            long rectId = page.DrawRectangle(2.0, 2.0, 3.0, 2.0);

            // Retrieve the shape object using the returned ID
            Shape shape = page.Shapes.GetShape((int)rectId);

            // Enable gradient fill
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
            shape.Fill.GradientFill.GradientDir.Value = 0; // Direction (0 = left‑to‑right)

            // Clear any existing gradient stops
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add a new gradient stop at position 0.75 with blue color (RGB 0,0,255)
            // Position is a fraction (0‑1) using MeasureConst.NUM
            // Color is specified as a hex string using ColorValue
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.75, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));

            // Save the diagram to a VSDX file
            diagram.Save("GradientRectangle.vsdx", SaveFileFormat.Vsdx);
        }
    }