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

            // Load the Visio diagram from a file (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Create image save options for PNG format
            ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
            // Set the resolution to 300 DPI
            options.Resolution = 300f;

            // Export the diagram to a high‑resolution PNG image
            diagram.Save("output.png", options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
