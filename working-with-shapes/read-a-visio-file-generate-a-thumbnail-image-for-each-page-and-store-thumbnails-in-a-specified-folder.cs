using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioThumbnailGenerator
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string visioFilePath = @"C:\Input\sample.vsdx";

            // Folder where thumbnails will be saved
            string thumbnailFolder = @"C:\Output\Thumbnails";

            // Ensure the output folder exists
            if (!Directory.Exists(thumbnailFolder))
                Directory.CreateDirectory(thumbnailFolder);

            // Load the Visio diagram using the provided constructor (load rule)
            Diagram diagram = new Diagram(visioFilePath);

            // Iterate through each page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Build the thumbnail file name (e.g., Page_1.png, Page_2.png, ...)
                string thumbnailPath = Path.Combine(thumbnailFolder, $"Page_{i + 1}.png");

                // Configure image save options:
                // - Use PNG format (SaveFileFormat.Png)
                // - Set the page index to render only the current page
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    PageIndex = i   // Render the i‑th page only
                };

                // Save the current page as an image using the provided Save method (save rule)
                diagram.Save(thumbnailPath, saveOptions);
            }

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
