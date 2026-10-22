using System;
using System.IO;
using System.Collections.Generic;
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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect all original (foreground) pages to avoid modifying the collection while iterating.
                List<Page> foregroundPages = new List<Page>();
                foreach (Page pg in diagram.Pages)
                {
                    foregroundPages.Add(pg);
                }

                // Define the uniform background color (hex string).
                const string backgroundHexColor = "#ADD8E6"; // Light blue.

                // Process each foreground page.
                foreach (Page foregroundPage in foregroundPages)
                {
                    // Retrieve page dimensions.
                    double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

                    // Create a new background page.
                    Page backgroundPage = new Page();
                    diagram.Pages.Add(backgroundPage);
                    backgroundPage.Background = BOOL.True;

                    // Ensure the background page has the same size as the foreground page.
                    backgroundPage.PageSheet.PageProps.PageWidth.Value = pageWidth;
                    backgroundPage.PageSheet.PageProps.PageHeight.Value = pageHeight;

                    // Add a full‑page rectangle shape to serve as the background fill.
                    long rectShapeId = backgroundPage.AddShape(0, 0, pageWidth, pageHeight, "Rectangle");
                    Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

                    // Configure the rectangle's fill to be a solid color.
                    rectShape.Fill.FillPattern.Value = 1; // Solid fill.
                    rectShape.Fill.FillForegnd.Value = backgroundHexColor;

                    // Send the rectangle to the back so other shapes appear above it.
                    rectShape.SendToBack();

                    // Link the foreground page to the newly created background page.
                    foregroundPage.BackPage = backgroundPage;
                }

                // Prepare PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Optional: set compliance level if needed.
                // pdfOptions.Compliance = PdfCompliance.PdfA1b;

                // Export the diagram to PDF using the configured options.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully saved as PDF to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}