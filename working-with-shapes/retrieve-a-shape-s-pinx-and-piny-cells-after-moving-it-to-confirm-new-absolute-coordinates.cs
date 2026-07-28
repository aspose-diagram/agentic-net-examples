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

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve a shape; index 1 typically skips the page background shape
            Shape shape = page.Shapes[1];

            // Define new absolute coordinates for the shape's pin
            double newPinX = 5.0; // example X coordinate
            double newPinY = 3.0; // example Y coordinate

            // Move the shape to the new position
            shape.MoveTo(newPinX, newPinY);

            // Refresh shape data to ensure internal values are updated
            shape.RefreshData();

            // Retrieve the updated PinX and PinY values from the shape's XForm
            double pinX = shape.XForm.PinX.Value;
            double pinY = shape.XForm.PinY.Value;

            // Output the coordinates to verify the move
            Console.WriteLine($"Updated PinX: {pinX}");
            Console.WriteLine($"Updated PinY: {pinY}");

            // Save the modified diagram (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
