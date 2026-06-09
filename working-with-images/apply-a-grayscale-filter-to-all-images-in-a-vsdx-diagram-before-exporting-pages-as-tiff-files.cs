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

            // Load the VSDX diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Set up image save options to render pages as grayscale TIFF images
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff)
            {
                ImageColorMode = ImageColorMode.Grayscale, // Apply grayscale filter
                PageCount = diagram.Pages.Count,           // Export all pages
                Resolution = 300                           // Optional: set DPI
            };

            // Save the diagram pages to a multi‑page TIFF file
            diagram.Save("output.tiff", saveOptions);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
