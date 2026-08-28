using System.IO;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class RenderDiagramPage
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram(@"C:\Input\sample.vsdx");

            // Create image save options for PNG format (supports transparency)
            ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);

            // Enable anti‑aliasing for smoother lines and curves
            options.SmoothingMode = SmoothingMode.AntiAlias;
            options.CompositingQuality = CompositingQuality.HighQuality;
            options.InterpolationMode = InterpolationMode.HighQualityBicubic;
            options.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Set resolution (optional, e.g., 300 DPI)
            options.Resolution = 300;

            // Ensure the background is transparent (PNG respects alpha channel)
            // No explicit property needed; transparent background is the default for PNG
            // when no background shape fills the page.

            // Render the first page (index 0) to an image file
            options.PageIndex = 0;   // 0‑based index of the page to render
            options.PageCount = 1;   // Render only one page

            // Save the rendered page as a bitmap image
            diagram.Save(@"C:\Output\page0.png", options);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
