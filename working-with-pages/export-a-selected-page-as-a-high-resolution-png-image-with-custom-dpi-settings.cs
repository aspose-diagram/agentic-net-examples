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

            // Load the source Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Configure image save options for PNG with a custom DPI (e.g., 300)
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.Resolution = 300f;          // Set horizontal and vertical DPI
            saveOptions.PageIndex = 2;              // Zero‑based index of the page to export (e.g., third page)
            saveOptions.PageCount = 1;              // Export only this single page
            saveOptions.EnlargePage = true;         // Enlarge page if needed (default is true)

            // Export the selected page as a high‑resolution PNG image
            diagram.Save("selected_page.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
