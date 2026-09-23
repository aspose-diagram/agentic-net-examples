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

            // Add a rectangle shape that covers the entire page on the background page
            // Parameters: pinX, pinY, width, height, master name, isCalculate
            long bgShapeId = backgroundPage.AddShape(0, 0, pageWidth, pageHeight, "Rectangle", false);
            Shape bgShape = backgroundPage.Shapes.GetShape(bgShapeId);

            // Set the shape fill to solid white
            bgShape.Fill.FillPattern.Value = 1;          // Solid fill
            bgShape.Fill.FillForegnd.Value = "#FFFFFF"; // White color

            // Remove any outline
            bgShape.Line.LinePattern.Value = 0; // No line

            // Send the shape to the back so it appears behind other content
            bgShape.SendToBack();

            // Link the background page to the foreground page
            foregroundPage.BackPage = backgroundPage;

            // Export the diagram as PNG with default options
            var pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save("output.png", pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
