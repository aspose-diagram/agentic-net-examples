using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source Visio diagram (VSD/VSDX)
            Diagram diagram = new Diagram("input.vsd");

            // Create image save options for PNG format
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Set the desired resolution (dots per inch)
            pngOptions.Resolution = 300f; // Example: 300 DPI

            // Save the diagram to PNG using the custom resolution
            diagram.Save("output.png", pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
