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

            // Create a new background page
            Page backgroundPage = new Page();
            backgroundPage.Background = BOOL.True;

            // Get dimensions of the first (foreground) page to size the background shape
            Page firstPage = diagram.Pages[0];
            double pageWidth = firstPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = firstPage.PageSheet.PageProps.PageHeight.Value;

            // Add a rectangle shape that covers the whole page
            // PinX and PinY are the center coordinates of the shape
            double centerX = pageWidth / 2.0;
            double centerY = pageHeight / 2.0;
            long bgShapeId = backgroundPage.AddShape(centerX, centerY, pageWidth, pageHeight, "Rectangle");

            // Retrieve the shape and set its fill to solid white
            Shape bgShape = backgroundPage.Shapes.GetShape(bgShapeId);
            bgShape.Fill.FillPattern.Value = 1;               // Solid fill
            bgShape.Fill.FillForegnd.Value = "#FFFFFF";       // White color
            bgShape.Line.LinePattern.Value = 0;               // No outline
            bgShape.SendToBack();                             // Ensure it is behind other content
            bgShape.Protection.LockSelect.Value = BOOL.True; // Make it non‑selectable

            // Add the background page to the diagram
            diagram.Pages.Add(backgroundPage);

            // Link each foreground page to the new background page
            foreach (Page page in diagram.Pages)
            {
                if (page.Background == BOOL.False)
                {
                    page.BackPage = backgroundPage;
                }
            }

            // Export the diagram to PNG with the overridden background
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save("output.png", pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
