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

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Create image save options for PNG format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Set the page size so that the generated image is 100 pixels wide.
            // Height is set to 100 as a simple square thumbnail; adjust as needed for aspect ratio.
            saveOptions.PageSize = new PageSize(100, 100);

            // Render only the first page (index 0) and limit to a single page
            saveOptions.PageIndex = 0;
            saveOptions.PageCount = 1;

            // Save the rendered page as a PNG thumbnail
            diagram.Save("thumbnail.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
