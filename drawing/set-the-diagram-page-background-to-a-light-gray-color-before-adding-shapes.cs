using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one foreground page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page as the foreground page.
                Page foregroundPage = diagram.Pages[0];

                // ------------------------------------------------------------
                // Create a background page and mark it as a background page.
                // ------------------------------------------------------------
                // Determine the next available page ID.
                int maxPageId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxPageId)
                        maxPageId = p.ID;
                }

                Page backgroundPage = new Page();
                backgroundPage.ID = maxPageId + 1;
                backgroundPage.Name = "Background-1";
                backgroundPage.Background = BOOL.True; // Mark as background page.

                // Add the background page to the diagram.
                diagram.Pages.Add(backgroundPage);

                // ------------------------------------------------------------
                // Add a full‑size rectangle shape to act as the background color.
                // ------------------------------------------------------------
                // Retrieve page dimensions from the foreground page.
                double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

                // Center coordinates for the rectangle.
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;

                // Add a rectangle shape covering the entire page on the background page.
                // The fourth parameter (isCalculate) must be a plain bool, not BOOL.
                long bgShapeId = backgroundPage.AddShape(centerX, centerY, "Rectangle", false);
                Shape bgShape = backgroundPage.Shapes.GetShape(bgShapeId);

                // Set solid fill pattern.
                bgShape.Fill.FillPattern.Value = 1; // Solid fill.
                // Light gray color (hex #D3D3D3).
                bgShape.Fill.FillForegnd.Value = "#D3D3D3";

                // Remove the rectangle border.
                bgShape.Line.LineWeight.Value = 0;

                // Send the background shape to the back so it stays behind other content.
                bgShape.SendToBack();

                // Link the foreground page to the newly created background page.
                foregroundPage.BackPage = backgroundPage;

                // ------------------------------------------------------------
                // Add a sample shape to the foreground page (demonstration).
                // ------------------------------------------------------------
                long shapeId = foregroundPage.AddShape(2.0, 2.0, "Rectangle", false);
                Shape shape = foregroundPage.Shapes.GetShape(shapeId);
                shape.Fill.FillPattern.Value = 1;
                shape.Fill.FillForegnd.Value = "#FF0000"; // Red fill for visibility.

                // ------------------------------------------------------------
                // Save the modified diagram.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}