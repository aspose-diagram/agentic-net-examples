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

            // Load the Visio diagram (replace with your actual file)
            Diagram diagram = new Diagram("input.vsdx");

            // Create image save options: PNG format with 300 DPI resolution
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.Resolution = 300; // DPI setting for high‑quality output

            // Save the diagram (first page) as a PNG image using the defined options
            diagram.Save("output.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
