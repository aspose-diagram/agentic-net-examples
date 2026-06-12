using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve a shape (example: first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes.GetShape(1);

            // Create ImageSaveOptions for PNG format and set a custom DPI resolution
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.Resolution = 300f; // Desired resolution in dots per inch

            // Export the shape to a PNG file using the custom save options
            shape.ToImage("shape.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
