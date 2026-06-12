using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the VSDX file
            Diagram diagram = new Diagram("input.vsdx");

            // ID of the sub‑shape whose absolute PinX we need
            int targetShapeId = 123; // replace with the actual ID

            // Locate the shape (including any child shapes) by its ID
            Shape shape = diagram.Pages[0].Shapes.GetShapeIncludingChild(targetShapeId);

            // Compute the absolute PinX coordinate
            double absolutePinX = CalculateAbsolutePinX(shape);

            Console.WriteLine($"Absolute PinX of shape ID {targetShapeId}: {absolutePinX}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Recursively adds the PinX of each parent shape to obtain the absolute value
    static double CalculateAbsolutePinX(Shape shape)
    {
        double pinX = shape.XForm.PinX.Value; // shape's PinX relative to its parent
        Shape parent = shape.ParentShape;
        while (parent != null)
        {
            pinX += parent.XForm.PinX.Value; // add parent's offset
            parent = parent.ParentShape;
        }
        return pinX;
    }
}
