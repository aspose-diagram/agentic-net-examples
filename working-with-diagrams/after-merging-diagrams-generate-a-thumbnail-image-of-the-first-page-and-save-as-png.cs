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

            // Load the first diagram (base diagram)
            Diagram diagram1 = new Diagram("Diagram1.vsdx");

            // Load the second diagram to be merged
            Diagram diagram2 = new Diagram("Diagram2.vsdx");

            // Merge the second diagram into the first one
            diagram1.Combine(diagram2);

            // Prepare image save options for PNG format
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Render only the first page (0‑based index)
            imgOptions.PageIndex = 0;
            // Optional: set resolution or other properties if needed
            // imgOptions.Resolution = 96;

            // Save the thumbnail of the first page as PNG
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
