using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (index 0) and a shape on that page.
            // Shape index 1 is typically the first user shape (0 is the background shape).
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[1];

            // Define the new absolute coordinates (in inches) for the shape's pin.
            double newPinX = 5.0; // X‑coordinate
            double newPinY = 3.0; // Y‑coordinate

            // Update the PinX and PinY cells directly.
            shape.XForm.PinX.Value = newPinX;
            shape.XForm.PinY.Value = newPinY;

            // Refresh the shape to apply the changes to its geometry and connections.
            shape.RefreshData();

            // Save the modified diagram (replace with your desired output path).
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
