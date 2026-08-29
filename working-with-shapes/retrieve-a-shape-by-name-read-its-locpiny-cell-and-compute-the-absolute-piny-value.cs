using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve the shape by its name (replace with the actual shape name)
            Shape shape = diagram.Pages[0].Shapes.GetShape("MyShapeName");

            // Read the LocPinY value (y‑coordinate of the pin relative to the shape's origin)
            double locPinY = shape.XForm.LocPinY.Value;

            // Read the PinY value (y‑coordinate of the pin relative to the parent shape/page)
            double pinY = shape.XForm.PinY.Value;

            // Compute the absolute PinY value.
            // Assuming the parent is the page, the absolute Y coordinate of the pin is:
            double absolutePinY = pinY + locPinY;

            // Output the results
            Console.WriteLine($"LocPinY: {locPinY}");
            Console.WriteLine($"PinY: {pinY}");
            Console.WriteLine($"Absolute PinY: {absolutePinY}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
