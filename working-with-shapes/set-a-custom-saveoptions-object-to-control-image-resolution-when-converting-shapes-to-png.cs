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

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve a shape to be exported (example: first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes[0];

            // Create ImageSaveOptions for PNG format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Set the desired resolution in DPI (e.g., 300 DPI)
            saveOptions.Resolution = 300f;

            // Export the shape to a PNG file using the custom save options
            shape.ToImage("shape.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
