using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output PDF file path
        string outputPath = "output.pdf";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Store the original number of pages to avoid processing newly added background pages
            int originalPageCount = diagram.Pages.Count;

            // Process each original page
            for (int i = 0; i < originalPageCount; i++)
            {
                // Retrieve the foreground page
                Page foregroundPage = diagram.Pages[i];

                // Get page dimensions (in inches)
                double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

                // Create a new background page
                Page backgroundPage = new Page();
                backgroundPage.Background = BOOL.True;               // Mark as background
                backgroundPage.Name = $"Background_{foregroundPage.Name}";

                // Add the background page to the diagram
                diagram.Pages.Add(backgroundPage);

                // Add a rectangle shape that spans the entire page
                // PinX and PinY are the center coordinates of the shape
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;
                long bgShapeId = backgroundPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle");
                Shape bgShape = backgroundPage.Shapes.GetShape(bgShapeId);

                // Set solid fill pattern
                bgShape.Fill.FillPattern.Value = 1;                     // Solid fill
                bgShape.Fill.FillForegnd.Value = "#ADD8E6";            // Light blue background color

                // Remove outline
                bgShape.Line.LinePattern.Value = LinePatternValue.None; // No border

                // Send the shape to the back so other content appears above it
                bgShape.SendToBack();

                // Make the background shape non‑selectable
                bgShape.Protection.LockSelect.Value = BOOL.True;

                // Link the foreground page to its background page
                foregroundPage.BackPage = backgroundPage;
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";          // Fallback font
            pdfOptions.ExportHiddenPage = false;       // Do not export hidden pages

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}