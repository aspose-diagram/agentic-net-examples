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

            // Iterate through each page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure image save options for PNG with transparent background
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                options.PageIndex = i;          // zero‑based index of the page to render
                options.PageCount = 1;          // render only the selected page
                options.SaveForegroundPagesOnly = true; // ensures background remains transparent

                // Define output file name for the current page
                string outputFile = $"Page_{i + 1}.png";

                // Save the current page as a separate PNG file
                diagram.Save(outputFile, options);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
