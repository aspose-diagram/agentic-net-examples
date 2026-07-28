using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VSDX file
            string filePath = "input.vsdx";

            // Load the diagram (uses the provided load rule)
            Diagram diagram = new Diagram(filePath);

            // ID of the sub‑shape whose absolute PinX we want
            int subShapeId = 123; // replace with the actual ID

            // Retrieve the shape including its children (uses the provided GetShapeIncludingChild rule)
            Shape subShape = diagram.Pages[0].Shapes.GetShapeIncludingChild(subShapeId);

            // Calculate the absolute PinX coordinate.
            // For a shape, XForm.PinX gives the absolute position of the shape's pin on the page.
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
