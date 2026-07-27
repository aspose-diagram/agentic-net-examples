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
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Get the first (foreground) page
                Page foregroundPage = diagram.Pages[0];

                // Create a new background page
                Page backgroundPage = new Page();
                backgroundPage.Background = BOOL.True; // Mark as background page
                diagram.Pages.Add(backgroundPage);

                // Copy page dimensions from the foreground page
                double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

                // Add a rectangle that covers the entire page on the background page
                // PinX and PinY are the center coordinates of the shape
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;
                long rectShapeId = backgroundPage.DrawRectangle(pinX, pinY, pageWidth, pageHeight);
                Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

                // Set solid fill pattern and the desired background color (hex string)
                rectShape.Fill.FillPattern.Value = 1;               // Solid fill
                rectShape.Fill.FillForegnd.Value = "#ADD8E6";       // Light blue background

                // Remove the rectangle border
                rectShape.Line.LinePattern.Value = 0;               // No line

                // Send the rectangle to the back so other shapes appear above it
                rectShape.SendToBack();

                // Associate the background page with the foreground page
                foregroundPage.BackPage = backgroundPage;

                // Configure PNG export options
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                pngOptions.PageIndex = 0; // Export the first page

                // Save the diagram as a PNG image with the solid background color
                diagram.Save("output.png", pngOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
