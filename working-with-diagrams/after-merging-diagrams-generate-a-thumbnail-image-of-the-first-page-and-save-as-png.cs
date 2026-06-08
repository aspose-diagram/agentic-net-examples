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

            // Load the first diagram
            Diagram diagram1 = new Diagram("Diagram1.vsdx");

            // Load the second diagram to be merged
            Diagram diagram2 = new Diagram("Diagram2.vsdx");

            // Merge the second diagram into the first one
            diagram1.Combine(diagram2);

            // Configure image save options for a PNG thumbnail of the first page
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            imgOptions.PageIndex = 0;   // 0‑based index of the first page
            imgOptions.PageCount = 1;   // render only one page

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
