using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page (index 0)
            Page page = diagram.Pages[0];

            // Get a shape to move (skip the page shape at index 0)
            Shape shape = page.Shapes[1];

            // Define new absolute coordinates for the shape's pin
            double newPinX = 5.0; // example X coordinate
            double newPinY = 3.0; // example Y coordinate

            // Move the shape to the new position
            shape.MoveTo(newPinX, newPinY);

            // Refresh shape data to ensure internal cells are updated
            shape.RefreshData();

            // Retrieve the updated PinX and PinY values
            double pinX = shape.XForm.PinX.Value;
            double pinY = shape.XForm.PinY.Value;

            // Output the results
            Console.WriteLine($"Shape ID: {shape.ID}");
            Console.WriteLine($"New PinX: {pinX}");
            Console.WriteLine($"New PinY: {pinY}");

            // Optionally save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
