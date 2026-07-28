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

            // Get the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // Select a shape by its ID (replace 1 with the desired shape ID)
            Shape shape = page.Shapes.GetShape(1);

            // Access the shape's XForm which holds positioning data
            XForm xform = shape.XForm;

            // Read the X and Y coordinates of the shape's pin (center of rotation)
            double pinX = xform.PinX.Value;
            double pinY = xform.PinY.Value;

            // Read the shape's width and height
            double width = xform.Width.Value;
            double height = xform.Height.Value;

            // Output the retrieved values
            Console.WriteLine($"PinX: {pinX}");
            Console.WriteLine($"PinY: {pinY}");
            Console.WriteLine($"Width: {width}");
            Console.WriteLine($"Height: {height}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
