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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputImagePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page to work with
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first (foreground) page
            Page foregroundPage = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

            // Create a new background page
            Page backgroundPage = new Page();

            // Assign a unique ID to the background page
            int maxId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxId) maxId = p.ID;
            }
            backgroundPage.ID = maxId + 1;
            backgroundPage.Name = "Background";
            backgroundPage.Background = BOOL.True; // Mark as background page

            // Add a full‑page rectangle shape to serve as the background canvas
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;
            long rectShapeId = backgroundPage.AddShape(pinX, pinY, "Rectangle", false);
            Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

            // Configure the rectangle's fill to a solid light gray color (#D3D3D3)
            rectShape.Fill.FillPattern.Value = 1;               // Solid fill
            rectShape.Fill.FillForegnd.Value = "#D3D3D3";       // Light gray
            rectShape.Line.LinePattern.Value = 0;               // No border

            // Add the background page to the diagram
            diagram.Pages.Add(backgroundPage);

            // Link the foreground page to the newly created background page
            foregroundPage.BackPage = backgroundPage;

            // Prepare image save options (PNG format)
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Export the diagram (including the background) to an image file
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}