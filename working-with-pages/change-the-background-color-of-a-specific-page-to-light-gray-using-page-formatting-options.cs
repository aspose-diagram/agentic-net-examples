using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input file path, output file path, zero‑based page index
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <pageIndex>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        if (!int.TryParse(args[2], out int pageIndex))
        {
            Console.Error.WriteLine("Invalid page index.");
            return;
        }

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate page index range
                if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine("Page index out of range.");
                    return;
                }

                // Retrieve the target foreground page
                Page targetPage = diagram.Pages[pageIndex];

                // Create a new background page
                Page backgroundPage = new Page();
                backgroundPage.Background = BOOL.True; // Mark as background page

                // Retrieve page dimensions (in inches)
                double pageWidth = targetPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = targetPage.PageSheet.PageProps.PageHeight.Value;

                // Add a rectangle shape that spans the entire page
                // PinX and PinY are the center coordinates; set to half width/height
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;
                long rectShapeId = backgroundPage.AddShape(pinX, pinY, "Rectangle", false);
                Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

                // Set rectangle size to match page dimensions
                rectShape.XForm.Width.Value = pageWidth;
                rectShape.XForm.Height.Value = pageHeight;

                // Configure solid fill with light gray color (#D3D3D3)
                rectShape.Fill.FillPattern.Value = 1; // Solid fill
                rectShape.Fill.FillForegnd.Value = "#D3D3D3";

                // Add the background page to the diagram
                diagram.Pages.Add(backgroundPage);

                // Link the background page to the target page
                targetPage.BackPage = backgroundPage;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Background color applied successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}