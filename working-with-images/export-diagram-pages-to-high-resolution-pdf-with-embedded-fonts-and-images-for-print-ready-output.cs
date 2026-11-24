using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
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
                // ------------------------------------------------------------
                // Validate that all fonts used in the diagram are installed on the system.
                // This helps ensure the PDF renders with the correct glyphs.
                // ------------------------------------------------------------
                InstalledFontCollection systemFonts = new InstalledFontCollection();
                var installedFamilyNames = systemFonts.Families
                    .Select(f => f.Name)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                foreach (Font font in diagram.Fonts)
                {
                    // Font name is stored in the Name property.
                    string fontName = font.Name;
                    if (!string.IsNullOrEmpty(fontName) && !installedFamilyNames.Contains(fontName))
                    {
                        Console.Error.WriteLine($"Warning: Font \"{fontName}\" used in diagram is not installed on this system.");
                    }
                }

                // ------------------------------------------------------------
                // Configure PDF save options for high‑resolution, print‑ready output.
                // ------------------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Set DPI for rasterized content (images, effects).
                pdfOptions.HorizontalResolution = 300;
                pdfOptions.VerticalResolution = 300;

                // Set JPEG quality for any raster images embedded in the PDF.
                pdfOptions.JpegQuality = 100;

                // Enable text compression to reduce file size.
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Specify the default font to use when a required font is missing.
                pdfOptions.DefaultFont = "Arial";

                // Explicitly set the save format (required by the API).
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Note: Aspose.Diagram does not provide a direct option to embed all fonts.
                // The DefaultFont property is the closest alternative.

                // ------------------------------------------------------------
                // Save the entire diagram (all pages) to a high‑resolution PDF.
                // ------------------------------------------------------------
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}