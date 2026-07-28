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

            // Load the diagram file
            Diagram diagram = new Diagram("input.vsdx");

            // Create image save options for PNG format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Set custom DPI (e.g., 300) to improve OLE object preview clarity
            saveOptions.Resolution = 300f; // DPI

            // Optional: keep the original scale
            saveOptions.Scale = 1.0f;

            // Save the rendered diagram (including OLE object previews) to an image file
            diagram.Save("output.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
