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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first (foreground) page
            Page foregroundPage = diagram.Pages[0];

            // Create a new background page
            Page backgroundPage = new Page();
            backgroundPage.Background = BOOL.True; // Mark as background page
            diagram.Pages.Add(backgroundPage);

            // Retrieve page dimensions (in inches)
            double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

            // Add a rectangle that spans the entire page on the background page
            // Parameters: pinX, pinY, width, height, master name, isCalculate (bool)
            long bgShapeId = backgroundPage.AddShape(0, 0, pageWidth, pageHeight, "Rectangle", false);
            Shape bgShape = backgroundPage.Shapes.GetShape(bgShapeId);

            // Set solid fill pattern
            bgShape.Fill.FillPattern.Value = 1; // Solid fill
            // Set desired background color (hex string, e.g., light blue)
            bgShape.Fill.FillForegnd.Value = "#ADD8E6";

            // Remove outline stroke
            bgShape.Line.LinePattern.Value = 0; // No line

            // Send the shape to the back so other content appears above it
            bgShape.SendToBack();

            // Make the background shape non‑selectable
            bgShape.Protection.LockSelect.Value = BOOL.True;

            // Link the foreground page to the background page
            foregroundPage.BackPage = backgroundPage;

            // Configure PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Export the first page (index 0)
            pngOptions.PageIndex = 0;

            // Save the diagram as a PNG image with the solid background color
            diagram.Save("output.png", pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
