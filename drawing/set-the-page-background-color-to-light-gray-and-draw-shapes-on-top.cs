using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "output.vsdx";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // ------------------------------------------------------------
            // Create foreground page (the main drawing page)
            // ------------------------------------------------------------
            Page foregroundPage = new Page();
            diagram.Pages.Add(foregroundPage);

            // ------------------------------------------------------------
            // Create background page and set it as a background page
            // ------------------------------------------------------------
            Page backgroundPage = new Page();
            // Mark this page as a background page
            backgroundPage.Background = BOOL.True;
            diagram.Pages.Add(backgroundPage);

            // Link the foreground page to the background page
            foregroundPage.BackPage = backgroundPage;

            // ------------------------------------------------------------
            // Determine page dimensions to size the background rectangle
            // ------------------------------------------------------------
            double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

            // ------------------------------------------------------------
            // Add a full‑page rectangle shape to the background page
            // ------------------------------------------------------------
            // Use the built‑in "Rectangle" master to create the shape
            long bgRectId = diagram.AddShape(0, 0, "Rectangle", diagram.Pages.IndexOf(backgroundPage));
            Shape bgRect = backgroundPage.Shapes.GetShape((int)bgRectId);
            // Set solid fill pattern
            bgRect.Fill.FillPattern.Value = 1; // Solid
            // Set light gray fill color (#D3D3D3)
            bgRect.Fill.FillForegnd.Value = "#D3D3D3";
            // Resize the rectangle to cover the entire page
            bgRect.XForm.Width.Value = pageWidth;
            bgRect.XForm.Height.Value = pageHeight;
            bgRect.XForm.PinX.Value = pageWidth / 2.0;
            bgRect.XForm.PinY.Value = pageHeight / 2.0;

            // ------------------------------------------------------------
            // Add shapes on top of the foreground page
            // ------------------------------------------------------------
            // Add a rectangle shape
            long rectId = diagram.AddShape(2, 2, "Rectangle", diagram.Pages.IndexOf(foregroundPage));
            Shape rectShape = foregroundPage.Shapes.GetShape((int)rectId);
            rectShape.Fill.FillPattern.Value = 1; // Solid
            rectShape.Fill.FillForegnd.Value = "#ADD8E6"; // Light blue
            rectShape.Line.LineWeight.Value = 0.02; // Thin line

            // Add an ellipse shape
            long ellipseId = diagram.AddShape(4, 4, "Ellipse", diagram.Pages.IndexOf(foregroundPage));
            Shape ellipseShape = foregroundPage.Shapes.GetShape((int)ellipseId);
            ellipseShape.Fill.FillPattern.Value = 1; // Solid
            ellipseShape.Fill.FillForegnd.Value = "#90EE90"; // Light green
            ellipseShape.Line.LineWeight.Value = 0.02;

            // Add some text to the foreground page
            foregroundPage.AddText(3, 3, 2, 0.5, "Sample Text", "Arial", "#000000", 0.2);

            // ------------------------------------------------------------
            // Save the diagram to VSDX format
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}