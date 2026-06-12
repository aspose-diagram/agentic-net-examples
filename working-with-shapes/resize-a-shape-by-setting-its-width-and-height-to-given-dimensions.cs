using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Identify the page and shape to resize
            // Here we use the first page and a shape with a known ID (replace with actual ID as needed)
            Page page = diagram.Pages[0];
            long shapeId = 1; // TODO: set the correct shape ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Desired dimensions (in inches)
            double newWidth = 2.0;
            double newHeight = 1.0;

            // Resize the shape using the provided SetWidth and SetHeight methods
            shape.SetWidth(newWidth);
            shape.SetHeight(newHeight);

            // Save the modified diagram (uses the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
