using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the first page (active page) for drawing
            Page page = diagram.ActivePage;

            // Add a rectangle shape directly using DrawRectangle.
            // Parameters: pinX, pinY, width, height (all in inches)
            double pinX = 2.0;
            double pinY = 2.0;
            double width = 4.0;
            double height = 2.0;
            long shapeId = page.DrawRectangle(pinX, pinY, width, height);

            // Retrieve the concrete Shape object using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Verify geometry data
            double actualPinX = shape.XForm.PinX.Value;
            double actualPinY = shape.XForm.PinY.Value;
            double actualWidth = shape.XForm.Width.Value;
            double actualHeight = shape.XForm.Height.Value;

            // Simple validation – throw if any value differs from expected
            if (Math.Abs(actualPinX - pinX) > 0.0001 ||
                Math.Abs(actualPinY - pinY) > 0.0001 ||
                Math.Abs(actualWidth - width) > 0.0001 ||
                Math.Abs(actualHeight - height) > 0.0001)
            {
                throw new Exception("Shape geometry does not match the expected placement.");
            }

            // Output the confirmed geometry
            Console.WriteLine($"Shape ID: {shapeId}");
            Console.WriteLine($"PinX: {actualPinX}");
            Console.WriteLine($"PinY: {actualPinY}");
            Console.WriteLine($"Width: {actualWidth}");
            Console.WriteLine($"Height: {actualHeight}");

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
