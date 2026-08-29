using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Choose a shape – here we take the first shape on the first page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Ensure the shape's positioning data is up‑to‑date
            shape.RefreshData();

            // Read XForm positioning values
            double pinX = shape.XForm.PinX.Value;      // X coordinate of the shape's pin (center of rotation)
            double pinY = shape.XForm.PinY.Value;      // Y coordinate of the shape's pin (center of rotation)
            double width = shape.XForm.Width.Value;    // Width of the shape in drawing units
            double height = shape.XForm.Height.Value;  // Height of the shape in drawing units

            // Example output
            System.Console.WriteLine($"PinX: {pinX}");
            System.Console.WriteLine($"PinY: {pinY}");
            System.Console.WriteLine($"Width: {width}");
            System.Console.WriteLine($"Height: {height}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
