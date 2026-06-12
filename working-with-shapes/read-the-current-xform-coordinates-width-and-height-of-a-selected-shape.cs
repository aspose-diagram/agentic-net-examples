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

            // Select the page that contains the shape (here we use the first page)
            Page page = diagram.Pages[0];

            // Identify the shape you want to inspect (replace with the actual shape ID)
            int shapeId = 1; // example shape ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Access the XForm object which holds positioning data
            XForm xform = shape.XForm;

            // Read the X and Y coordinates of the shape's pin (center of rotation)
            double pinX = xform.PinX.Value;
            double pinY = xform.PinY.Value;

            // Read the width and height of the shape (in drawing units)
            double width = xform.Width.Value;
            double height = xform.Height.Value;

            // Output the retrieved values
            Console.WriteLine($"Shape ID: {shapeId}");
            Console.WriteLine($"PinX: {pinX}");
            Console.WriteLine($"PinY: {pinY}");
            Console.WriteLine($"Width: {width}");
            Console.WriteLine($"Height: {height}");

            // Save the diagram if any modifications were made (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
