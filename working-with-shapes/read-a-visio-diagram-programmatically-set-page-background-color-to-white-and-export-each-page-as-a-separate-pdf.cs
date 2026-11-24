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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // ------------------------------------------------------------
            // Create a dedicated background page with a white rectangle.
            // ------------------------------------------------------------
            Page backgroundPage = new Page();
            backgroundPage.Name = "Background_White";
            backgroundPage.Background = BOOL.True; // Mark as a background page.

            // Add the background page to the diagram.
            diagram.Pages.Add(backgroundPage);

            // Retrieve page dimensions (in inches) to size the rectangle.
            double pageWidth = backgroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = backgroundPage.PageSheet.PageProps.PageHeight.Value;

            // Add a rectangle shape that spans the entire page.
            // AddShape(pinX, pinY, masterName, isCalculate)
            long rectShapeId = backgroundPage.AddShape(0, 0, "Rectangle", false);
            Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

            // Set the rectangle fill to solid white.
            rectShape.Fill.FillPattern.Value = 1;               // Solid fill.
            rectShape.Fill.FillForegnd.Value = "#FFFFFF";       // White color.
            // Remove the rectangle border.
            rectShape.Line.LinePattern.Value = 0;

            // ------------------------------------------------------------
            // Link each foreground page to the newly created background page.
            // ------------------------------------------------------------
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Skip the background page itself.
                if (page.Background == BOOL.True)
                    continue;

                // Assign the background page.
                page.BackPage = backgroundPage;
            }

            // ------------------------------------------------------------
            // Export each page as a separate PDF file.
            // ------------------------------------------------------------
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Aspose.Diagram.Page page = diagram.Pages[i];

                // Skip background pages; we only export foreground pages.
                if (page.Background == BOOL.True)
                    continue;

                // Configure PDF save options for a single page.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;          // Explicit format.
                pdfOptions.PageIndex = i;                           // Zero‑based page index.
                pdfOptions.PageCount = 1;                           // Export only this page.
                pdfOptions.ExportHiddenPage = false;                // Do not export hidden pages.
                pdfOptions.TextCompression = PdfTextCompression.Flate; // Reduce file size.

                // Build output file name.
                string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}.pdf");

                // Save the current page as PDF.
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Exported page {i + 1} to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}