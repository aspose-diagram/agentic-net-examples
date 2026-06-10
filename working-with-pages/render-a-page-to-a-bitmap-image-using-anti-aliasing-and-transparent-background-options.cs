using System;
using System.IO;
using System.Drawing.Drawing2D;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class RenderDiagramPage
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Configure image save options
            var options = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // Render only the first page
                PageIndex = 0,
                PageCount = 1,

                // Enable anti‑aliasing for smoother lines and curves
                SmoothingMode = SmoothingMode.AntiAlias,
                CompositingQuality = CompositingQuality.HighQuality,
                PixelOffsetMode = PixelOffsetMode.HighQuality,

                // Set a high resolution for better quality (optional)
                Resolution = 300
            };

            // Save the rendered page as a PNG image (supports transparency)
            diagram.Save("output.png", options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
