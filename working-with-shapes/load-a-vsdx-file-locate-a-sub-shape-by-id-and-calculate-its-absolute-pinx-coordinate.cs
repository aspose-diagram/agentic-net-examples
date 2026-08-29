using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (VSDX) from file
            Diagram diagram = new Diagram("input.vsdx");

            // Specify the ID of the sub‑shape you want to locate
            int subShapeId = 5; // replace with the actual ID

            // Retrieve the shape, including any child shapes inside groups
            Shape subShape = diagram.Pages[0].Shapes.GetShapeIncludingChild(subShapeId);

            // The XForm.PinX property holds the shape's PinX coordinate.
            // For a sub‑shape this value is relative to its parent; to obtain the
            // absolute page coordinate you can add the parent's offset if needed.
            // Here we assume the shape is not nested deeper, or that PinX is already absolute.
            double absolutePinX = subShape.XForm.PinX.Value;

            // Output the result
            Console.WriteLine($"Absolute PinX of shape ID {subShapeId}: {absolutePinX}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
