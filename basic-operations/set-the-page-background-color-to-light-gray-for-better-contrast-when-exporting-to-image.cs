using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output image file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.png";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first (foreground) page
            Page foregroundPage = diagram.Pages[0];

            // Create a new background page
            // Use a new ID that is greater than existing pages
            int newPageId = diagram.Pages.Count + 1;
            Page backgroundPage = new Page(newPageId);
            backgroundPage.Name = "BackgroundPage";
            backgroundPage.Background = BOOL.True; // Mark as background page

            // Retrieve page dimensions (in inches)
            double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

            // Calculate center position for the background rectangle
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;

            // Add a rectangle shape that spans the entire page
            // Master name "Rectangle" is a built‑in shape
            long shapeId = backgroundPage.AddShape(pinX, pinY, "Rectangle", false);

            // Retrieve the shape object to modify its fill
            Shape bgShape = backgroundPage.Shapes.GetShape(shapeId);

            // Set solid fill pattern
            bgShape.Fill.FillPattern.Value = 1; // Solid fill

            // Set light gray background color (hex #D3D3D3)
            bgShape.Fill.FillBkgnd.Value = "#D3D3D3";

            // Remove border by setting line pattern to 0
            bgShape.Line.LinePattern.Value = 0;

            // Add the background page to the diagram
            diagram.Pages.Add(backgroundPage);

            // Link the foreground page to the new background page
            foregroundPage.BackPage = backgroundPage;

            // Prepare image save options (PNG format)
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Export only the first page (optional)
            saveOptions.PageIndex = 0;
            saveOptions.PageCount = 1;

            // Save the diagram as an image
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram exported successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}