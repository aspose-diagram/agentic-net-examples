using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportTriangleDiagram
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram that contains the triangle.
            // The Diagram constructor that takes a file path is the standard load method.
            Diagram diagram = new Diagram("triangle.vsd");

            // Create image save options for PNG format.
            // No additional settings are changed, so default resolution is used.
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Export the diagram to a PNG image file using the Save method overload that accepts SaveOptions.
            diagram.Save("triangle.png", pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
