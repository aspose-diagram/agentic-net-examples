using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (example ID = 1)
            Shape shape = page.Shapes.GetShape(1);

            // Apply rounded position values
            SetShapePosition(shape, 2.3456, 3.789);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Rounds PinX and PinY to two decimal places before assigning them to the shape
    static void SetShapePosition(Shape shape, double pinX, double pinY)
    {
        double roundedPinX = Math.Round(pinX, 2);
        double roundedPinY = Math.Round(pinY, 2);
        shape.XForm.PinX.Value = roundedPinX;
        shape.XForm.PinY.Value = roundedPinY;
    }
}
