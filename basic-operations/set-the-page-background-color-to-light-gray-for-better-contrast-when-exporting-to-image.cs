using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main()
    {
        try
        {

            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Ensure there is at least one page (default page is created)
                Page foregroundPage = diagram.Pages[0];

                // Retrieve the dimensions of the foreground page (in inches)
                double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

                // Create a new background page
                Page backgroundPage = new Page();
                backgroundPage.Background = BOOL.True; // Mark as background page

                // Set the background page size to match the foreground page
                backgroundPage.PageSheet.PageProps.PageWidth.Value = pageWidth;
                backgroundPage.PageSheet.PageProps.PageHeight.Value = pageHeight;

                // Draw a rectangle that covers the entire page area
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;
                long rectShapeId = backgroundPage.DrawRectangle(centerX, centerY, pageWidth, pageHeight);
                Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

                // Apply a solid light gray fill to the rectangle
                rectShape.Fill.FillPattern.Value = 1;          // Solid fill pattern
                rectShape.Fill.FillForegnd.Value = "#D3D3D3"; // Light gray color

                // Remove the rectangle border
                rectShape.Line.LinePattern.Value = 0; // No line pattern

                // Add the background page to the diagram
                diagram.Pages.Add(backgroundPage);

                // Link the foreground page to the newly created background page
                foregroundPage.BackPage = backgroundPage;

                // Export the diagram to an image (PNG) with the background applied
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                saveOptions.PageIndex = 0; // Export the first page
                diagram.Save("output.png", saveOptions);
            }

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
