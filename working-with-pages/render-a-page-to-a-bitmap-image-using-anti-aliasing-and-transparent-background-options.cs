using System.IO;
using System;
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
            var diagram = new Diagram(@"C:\Input\sample.vsdx");

            // Configure image save options
            var options = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // Enable anti‑aliasing for smoother lines and curves
                SmoothingMode = SmoothingMode.AntiAlias,
                // Improve compositing quality (optional, but enhances rendering)
                CompositingQuality = CompositingQuality.HighQuality,
                // Use high‑quality pixel offset (optional)
                PixelOffsetMode = PixelOffsetMode.HighQuality,
                // Ensure the background remains transparent (PNG supports transparency by default)
                // No explicit property needed; just use PNG format.
            };

            // Render the first page (index 0) to a bitmap image
            // PageIndex defaults to 0, so we don't need to set it explicitly.
            diagram.Save(@"C:\Output\page0.png", options);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
