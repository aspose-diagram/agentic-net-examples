using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output path
        string outputPath = "output.vsdx";

        // No input files to validate for this task

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // ------------------------------------------------------------
            // Create a background page and set its background flag
            // ------------------------------------------------------------
            Page backgroundPage = new Page();
            backgroundPage.Background = BOOL.True; // Mark as background page

            // Add the background page to the diagram
            diagram.Pages.Add(backgroundPage);

            // ------------------------------------------------------------
            // Determine page dimensions (use defaults if not set)
            // ------------------------------------------------------------
            double pageWidth = backgroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = backgroundPage.PageSheet.PageProps.PageHeight.Value;

            // ------------------------------------------------------------
            // Add a full‑page rectangle shape to act as the background color
            // ------------------------------------------------------------
            long bgRectId = backgroundPage.DrawRectangle(0, 0, pageWidth, pageHeight);
            Shape bgRect = backgroundPage.Shapes.GetShape(bgRectId);
            // Set solid fill pattern
            bgRect.Fill.FillPattern.Value = 1; // Solid
            // Light gray color (#D3D3D3)
            bgRect.Fill.FillForegnd.Value = "#D3D3D3";

            // ------------------------------------------------------------
            // Retrieve the foreground (first) page; if none exists, create one
            // ------------------------------------------------------------
            Page foregroundPage;
            if (diagram.Pages.Count > 1)
            {
                // The first page after the background page is the foreground page
                foregroundPage = diagram.Pages[1];
            }
            else
            {
                // Create a new foreground page
                foregroundPage = new Page();
                diagram.Pages.Add(foregroundPage);
            }

            // Link the foreground page to the background page
            foregroundPage.BackPage = backgroundPage;

            // ------------------------------------------------------------
            // Draw shapes on the foreground page
            // ------------------------------------------------------------
            // Example: draw a rectangle
            long rectId = foregroundPage.DrawRectangle(1, 1, 2, 2);
            Shape rectShape = foregroundPage.Shapes.GetShape(rectId);
            rectShape.Fill.FillPattern.Value = 1; // Solid fill
            rectShape.Fill.FillForegnd.Value = "#FF0000"; // Red fill

            // Example: draw an ellipse
            long ellipseId = foregroundPage.DrawEllipse(4, 4, 1.5, 1);
            Shape ellipseShape = foregroundPage.Shapes.GetShape(ellipseId);
            ellipseShape.Fill.FillPattern.Value = 1; // Solid fill
            ellipseShape.Fill.FillForegnd.Value = "#0000FF"; // Blue fill

            // ------------------------------------------------------------
            // Save the diagram to VSDX format
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}