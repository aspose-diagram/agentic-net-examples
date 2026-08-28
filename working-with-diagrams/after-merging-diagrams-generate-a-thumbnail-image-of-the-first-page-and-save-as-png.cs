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

            // Load the diagrams to be merged
            Diagram diagram1 = new Diagram("Diagram1.vsdx");
            Diagram diagram2 = new Diagram("Diagram2.vsdx");

            // Merge the second diagram into the first one
            diagram1.Combine(diagram2);

            // Prepare image save options for a PNG thumbnail of the first page
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            imgOptions.PageIndex = 0;   // first page (0‑based index)
            imgOptions.PageCount = 1;   // render only this page

            // Save the thumbnail image
            diagram1.Save("Thumbnail.png", imgOptions);

            // Clean up resources
            diagram1.Dispose();
            diagram2.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
