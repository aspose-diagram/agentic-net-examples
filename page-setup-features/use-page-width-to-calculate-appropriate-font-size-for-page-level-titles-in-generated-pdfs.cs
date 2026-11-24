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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page to add a title based on page width.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Retrieve the current page.
                Page page = diagram.Pages[i];

                // Get page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate a font size proportional to the page width.
                // Example: 10 points per inch of page width.
                double titleFontSize = pageWidth * 10.0;

                // Define title text.
                string titleText = $"Page {i + 1} Title";

                // Position the title near the top centre of the page.
                // PinX = 0 (left edge), PinY = pageHeight - 0.5 inches (margin from top).
                double pinX = 0.0;
                double pinY = pageHeight - 0.5; // 0.5 inch margin from top.
                double titleWidth = pageWidth;   // Full page width.
                double titleHeight = 0.5;        // Height allocated for the title.

                // Add the title text shape.
                // Parameters: pinX, pinY, width, height, text, fontName, fontColor (hex), fontSize.
                Shape titleShape = page.AddText(pinX, pinY, titleWidth, titleHeight,
                                                titleText, "Arial", "#000000", titleFontSize);
                // Optional: center the text horizontally.
                // The AddText method respects the width parameter; centering can be handled via alignment if needed.
            }

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Export hidden pages if any (set to false for typical use).
                ExportHiddenPage = false,
                // Set high resolution for raster images embedded in the PDF.
                HorizontalResolution = 300,
                VerticalResolution = 300,
                // Use Flate compression to reduce PDF size.
                TextCompression = PdfTextCompression.Flate
            };

            // Save the diagram as a PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"PDF successfully saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}