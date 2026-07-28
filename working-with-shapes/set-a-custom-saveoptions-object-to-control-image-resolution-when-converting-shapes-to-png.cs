using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ShapeToPngWithResolution
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve a shape to be converted (example: first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes[0];

            // Create ImageSaveOptions for PNG format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Set the desired resolution (dots per inch)
            saveOptions.Resolution = 300f; // 300 DPI

            // Convert the shape to a PNG image using the custom save options
            shape.ToImage("output.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
