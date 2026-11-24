using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, zero‑based page index
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <pageIndex>");
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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
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
                    Console.Error.WriteLine($"Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} pages.");
                    return;
                }

                // Retrieve the target foreground page
                Page foregroundPage = diagram.Pages[pageIndex];

                // Create a new background page
                // The Add method creates a new page; adjust if a different API is required.
                Page backgroundPage = new Page();
                diagram.Pages.Add(backgroundPage);
                // Mark the new page as a background page
                backgroundPage.Background = BOOL.True;

                // Retrieve page dimensions (in inches)
                double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

                // Add a rectangle shape that spans the entire page
                // Use the built‑in "Rectangle" master; the last parameter indicates no calculation.
                long rectShapeId = backgroundPage.AddShape(pageWidth / 2, pageHeight / 2, "Rectangle", false);
                // Retrieve the shape object to modify its properties
                Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

                // Set the shape size to match the page dimensions
                rectShape.XForm.Width.Value = pageWidth;
                rectShape.XForm.Height.Value = pageHeight;

                // Configure fill: solid pattern (value 1) and light gray color (#D3D3D3)
                rectShape.Fill.FillPattern.Value = 1; // Solid fill
                rectShape.Fill.FillForegnd.Value = "#D3D3D3";

                // Link the foreground page to the newly created background page
                foregroundPage.BackPage = backgroundPage;

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Background color applied and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}