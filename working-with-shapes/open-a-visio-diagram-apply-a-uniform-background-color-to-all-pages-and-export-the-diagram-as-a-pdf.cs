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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath> [backgroundHexColor]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string backgroundColor = args.Length >= 3 ? args[2] : "#ADD8E6"; // Default light blue.

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages to add a full‑page background rectangle.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center coordinates for the rectangle shape.
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add a rectangle shape that spans the entire page.
                    // The built‑in master name "Rectangle" is used.
                    long shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle");
                    Shape bgShape = page.Shapes.GetShape(shapeId);

                    // Set solid fill pattern.
                    bgShape.Fill.FillPattern.Value = 1; // Solid fill.
                    // Apply the requested background color (hex string, e.g., "#FF0000").
                    bgShape.Fill.FillForegnd.Value = backgroundColor;

                    // Send the background shape to the back so other content appears above it.
                    bgShape.SendToBack();
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Explicitly set the format tracker.
                    SaveFormat = SaveFileFormat.Pdf,
                    // Use PDF/A‑1b compliance as an example.
                    Compliance = PdfCompliance.PdfA1b,
                    // Enable text compression to reduce file size.
                    TextCompression = PdfTextCompression.Flate,
                    // Set a default fallback font.
                    DefaultFont = "Arial",
                    // Export hidden pages if any.
                    ExportHiddenPage = true,
                    // High‑resolution raster images inside the PDF.
                    HorizontalResolution = 300,
                    VerticalResolution = 300,
                    JpegQuality = 100
                };

                // Save the diagram as PDF using the configured options.
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}