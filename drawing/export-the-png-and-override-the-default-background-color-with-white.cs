using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PNG file path
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

            // ------------------------------------------------------------
            // Create a background page and set its background flag to true
            // ------------------------------------------------------------
            Page backgroundPage = new Page();
            diagram.Pages.Add(backgroundPage);
            backgroundPage.Background = BOOL.True;

            // ------------------------------------------------------------
            // Determine page dimensions (in inches) from the first foreground page
            // ------------------------------------------------------------
            Page foregroundPage = diagram.Pages[0];
            double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

            // ------------------------------------------------------------
            // Add a rectangle shape that spans the entire page on the background page
            // ------------------------------------------------------------
            long bgShapeId = backgroundPage.DrawRectangle(0, 0, pageWidth, pageHeight);
            Shape bgShape = backgroundPage.Shapes.GetShape(bgShapeId);

            // Set the background fill color to white
            bgShape.Fill.FillBkgnd.Value = "#FFFFFF";

            // Remove the shape's border by setting line pattern to 0 (no line)
            bgShape.Line.LinePattern.Value = 0;

            // Make the background shape non-selectable
            bgShape.Protection.LockSelect.Value = BOOL.True;

            // ------------------------------------------------------------
            // Link the foreground page to the newly created background page
            // ------------------------------------------------------------
            foregroundPage.BackPage = backgroundPage;

            // ------------------------------------------------------------
            // Export the diagram to PNG using ImageSaveOptions
            // ------------------------------------------------------------
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Optional: set resolution, export hidden pages, etc., if needed
            // pngOptions.Resolution = 300f;
            // pngOptions.ExportHiddenPage = BOOL.False;

            diagram.Save(outputPath, pngOptions);
            Console.WriteLine($"Diagram exported successfully to PNG: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}