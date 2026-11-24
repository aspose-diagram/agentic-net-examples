using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPngPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one foreground page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first (foreground) page
            Page foregroundPage = diagram.Pages[0];

            // Create a new background page
            Page backgroundPage = new Page();
            diagram.Pages.Add(backgroundPage);
            backgroundPage.Background = BOOL.True; // Mark as background page

            // Retrieve page dimensions from the foreground page
            double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

            // Draw a rectangle that spans the entire page on the background page
            // The DrawRectangle method returns the shape ID of the created shape
            long rectShapeId = backgroundPage.DrawRectangle(0, 0, pageWidth, pageHeight);

            // Retrieve the rectangle shape to configure its appearance
            Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

            // Set the fill background color to white
            rectShape.Fill.FillBkgnd.Value = "#FFFFFF";

            // Remove any outline by setting line pattern to 0 (no line)
            rectShape.Line.LinePattern.Value = 0;

            // Make the background shape non-selectable
            rectShape.Protection.LockSelect.Value = BOOL.True;

            // Link the foreground page to the newly created background page
            foregroundPage.BackPage = backgroundPage;

            // Prepare PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Export only the foreground page (background is part of the page rendering)
            pngOptions.PageIndex = 0;
            pngOptions.PageCount = 1;

            // Save the diagram as PNG with the specified options
            diagram.Save(outputPath, pngOptions);

            Console.WriteLine($"Diagram exported successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}