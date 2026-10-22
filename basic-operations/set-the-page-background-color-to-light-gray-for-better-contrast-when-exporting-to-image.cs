using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputImagePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine the maximum existing page ID to assign a new unique ID
            int maxPageId = 0;
            foreach (Page existingPage in diagram.Pages)
            {
                if (existingPage.ID > maxPageId)
                    maxPageId = existingPage.ID;
            }

            // Create a new background page
            Page backgroundPage = new Page(maxPageId + 1);
            backgroundPage.Background = BOOL.True; // Mark as background page

            // Add the background page to the diagram
            diagram.Pages.Add(backgroundPage);

            // Assume all foreground pages share the same dimensions; use the first page for size reference
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page referencePage = diagram.Pages[0];
            double pageWidth = referencePage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = referencePage.PageSheet.PageProps.PageHeight.Value;

            // Add a rectangle shape that covers the entire page on the background page
            // DrawRectangle(pinX, pinY, width, height) where pinX/Y are the center coordinates
            double centerX = pageWidth / 2.0;
            double centerY = pageHeight / 2.0;
            long rectShapeId = backgroundPage.DrawRectangle(centerX, centerY, pageWidth, pageHeight);
            Shape backgroundShape = backgroundPage.Shapes.GetShape(rectShapeId);

            // Set solid fill pattern
            backgroundShape.Fill.FillPattern.Value = 1;
            // Set light gray fill color
            backgroundShape.Fill.FillForegnd.Value = "#D3D3D3";
            // Remove outline by setting line pattern to none (0) and lock selection
            backgroundShape.Line.LinePattern.Value = 0;
            backgroundShape.Protection.LockSelect.Value = BOOL.True;

            // Link each foreground page to the background page
            foreach (Page page in diagram.Pages)
            {
                // Skip the background page itself
                if (page.Background == BOOL.True)
                    continue;

                page.BackPage = backgroundPage;
            }

            // Configure image save options (PNG format)
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.PageIndex = 0;   // Export the first page (adjust as needed)
            saveOptions.PageCount = 1;   // Export a single page

            // Save the diagram as an image
            diagram.Save(outputPath, saveOptions);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}