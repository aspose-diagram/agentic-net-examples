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

            // Load an existing Visio diagram (VSD/VSDX)
            Diagram diagram = new Diagram("input.vsd");

            // Create image save options for PNG format
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Set custom resolution (dots per inch) for the generated PNG image
            pngOptions.Resolution = 300f; // e.g., 300 DPI

            // Save the diagram as a PNG image using the specified options
            diagram.Save("output.png", pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
