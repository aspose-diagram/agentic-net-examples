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

            // Load the source Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Prepare image save options for PNG format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Set the output page size – 100 px wide (height set to keep aspect, here also 100 px)
            saveOptions.PageSize = new PageSize(100, 100);

            // Render only the first page (thumbnail of a single page)
            saveOptions.PageIndex = 0;
            saveOptions.PageCount = 1;

            // Save the rendered thumbnail as PNG
            diagram.Save("thumbnail.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
