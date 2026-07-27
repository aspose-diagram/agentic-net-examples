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

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Set up image save options for PNG format with 300 DPI resolution
            ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
            options.Resolution = 300f; // DPI

            // Export the diagram to a high‑resolution PNG image
            diagram.Save("output.png", options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
