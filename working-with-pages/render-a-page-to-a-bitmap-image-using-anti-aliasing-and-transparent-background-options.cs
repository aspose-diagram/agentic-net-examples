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

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Index of the page to render (0‑based)
            int pageIndex = 0;

            // Configure image export options
            ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
            // Enable anti‑aliasing for smoother rendering
            options.SmoothingMode = Aspose.Diagram.Saving.SmoothingMode.AntiAlias;
            // Specify which page to render
            options.PageIndex = pageIndex;

            // Export the selected page to a PNG bitmap
            string outputPath = "page.png";
            diagram.Save(outputPath, options);

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine("Page rendered to image successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
