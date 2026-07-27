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

            // Create a new diagram instance
            using (Diagram diagram = new Diagram())
            {
                // Add a foreground page (the page that will contain the actual drawing)
                Page foregroundPage = new Page();
                diagram.Pages.Add(foregroundPage);

                // Retrieve page dimensions (in inches)
                double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

                // Add a background page and mark it as a background
                Page backgroundPage = new Page();
                backgroundPage.Background = BOOL.True;
                diagram.Pages.Add(backgroundPage);

                // Create a rectangle shape that spans the entire page
                double pinX = pageWidth / 2.0;   // Center X
                double pinY = pageHeight / 2.0;  // Center Y
                long bgShapeId = backgroundPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle");

                // Retrieve the shape to set its fill and line properties
                Shape bgShape = backgroundPage.Shapes.GetShape(bgShapeId);
                // Solid fill pattern
                bgShape.Fill.FillPattern.Value = 1;
                // Light gray color (hex code)
                bgShape.Fill.FillForegnd.Value = "#D3D3D3";
                // No outline
                bgShape.Line.LinePattern.Value = 0;
                // Ensure the background shape is behind other content
                bgShape.SendToBack();

                // Link the foreground page to the background page
                foregroundPage.BackPage = backgroundPage;

                // Export the diagram to an image (PNG) with the background applied
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                saveOptions.PageIndex = 0; // Export the first (and only) page
                diagram.Save("output.png", saveOptions);
            }

            Console.WriteLine("Diagram exported with light gray background.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
