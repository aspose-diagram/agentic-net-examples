using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";

        // Path for the exported PNG image of the first page
        string outputPath = "first_page.png";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Set up high‑resolution PNG export options
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.Resolution = 300f;   // DPI for high quality
            saveOptions.PageIndex = 0;       // Zero‑based index of the first page
            saveOptions.PageCount = 1;       // Export only this page

            // Export the first page as a PNG image
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"First page exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
