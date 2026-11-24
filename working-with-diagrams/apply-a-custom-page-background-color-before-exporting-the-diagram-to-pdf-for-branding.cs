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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
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
            // Load the Visio diagram inside a using block (ensures proper disposal).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first (foreground) page.
                Page foregroundPage = diagram.Pages[0];

                // Retrieve page dimensions (in inches) to size the background shape.
                double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

                // ------------------------------------------------------------
                // Create a background page and mark it as a background.
                // ------------------------------------------------------------
                Page backgroundPage = new Page();
                backgroundPage.Name = "BackgroundPage";
                backgroundPage.Background = BOOL.True; // Mark as background page.

                // Add the background page to the diagram.
                diagram.Pages.Add(backgroundPage);

                // ------------------------------------------------------------
                // Add a full‑page rectangle shape to the background page.
                // ------------------------------------------------------------
                // Add a rectangle master shape at origin (pinX=0, pinY=0). The shape will be resized later.
                long rectShapeId = backgroundPage.AddShape(0, 0, "Rectangle", false);
                Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

                // Resize the rectangle to cover the entire page.
                rectShape.XForm.Width.Value = pageWidth;
                rectShape.XForm.Height.Value = pageHeight;
                // Center the rectangle on the page.
                rectShape.XForm.PinX.Value = pageWidth / 2.0;
                rectShape.XForm.PinY.Value = pageHeight / 2.0;

                // Set a solid fill pattern.
                rectShape.Fill.FillPattern.Value = 1; // Solid fill.
                // Apply the custom brand color (example: a light orange).
                rectShape.Fill.FillForegnd.Value = "#FFCC00";

                // Remove any outline stroke.
                rectShape.Line.LinePattern.Value = 0;

                // Send the rectangle to the back so other content appears on top.
                rectShape.SendToBack();

                // Link the foreground page to the newly created background page.
                foregroundPage.BackPage = backgroundPage;

                // ------------------------------------------------------------
                // Configure PDF save options.
                // ------------------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set format.
                pdfOptions.ExportHiddenPage = false;       // Do not export hidden pages.
                pdfOptions.HorizontalResolution = 300;    // High‑resolution raster content.
                pdfOptions.VerticalResolution = 300;
                pdfOptions.TextCompression = PdfTextCompression.Flate; // Reduce PDF size.

                // ------------------------------------------------------------
                // Save the diagram as a PDF with the branding background.
                // ------------------------------------------------------------
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"PDF successfully saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}