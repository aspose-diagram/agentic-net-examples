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

        // Set a fallback font before any diagram processing.
        FontConfigs.DefaultFontName = "Arial";

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // 1. Validate that all fonts used in the diagram are installed.
                // -----------------------------------------------------------------
                InstalledFontCollection systemFonts = new InstalledFontCollection();
                // Build a hash set of installed font names for fast lookup.
                var installedFontNames = systemFonts.Families
                    .Select(f => f.Name)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                foreach (Font font in diagram.Fonts)
                {
                    string fontName = font.Name;
                    if (!installedFontNames.Contains(fontName))
                    {
                        Console.Error.WriteLine($"Warning: Font \"{fontName}\" used in diagram is not installed on the system.");
                    }
                }

                // -----------------------------------------------------------------
                // 2. Validate that every character run meets the minimum font size.
                //    Accessibility guideline: minimum 12 points.
                // -----------------------------------------------------------------
                const double MinimumFontSize = 12.0;
                bool sizeViolationFound = false;

                // Iterate through all pages.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the page.
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Iterate through all character runs in the shape.
                        for (int charIndex = 0; charIndex < shape.Chars.Count; charIndex++)
                        {
                            Aspose.Diagram.Char ch = shape.Chars[charIndex];

                            // Font size is stored in the Size cell; use .Value.
                            double fontSize = ch.Size.Value;

                            if (fontSize < MinimumFontSize)
                            {
                                sizeViolationFound = true;
                                Console.Error.WriteLine(
                                    $"Error: Shape ID {shape.ID} on page {pageIndex + 1} contains text with font size {fontSize}pt (minimum {MinimumFontSize}pt required).");
                            }
                        }
                    }
                }

                if (sizeViolationFound)
                {
                    Console.Error.WriteLine("Font size validation failed. Export aborted.");
                    return;
                }

                // -----------------------------------------------------------------
                // 3. Configure PDF/A export options.
                // -----------------------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.Compliance = PdfCompliance.PdfA1b;          // PDF/A‑1b compliance.
                pdfOptions.DefaultFont = "Arial";                     // Fallback font.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;           // Explicitly set format.

                // Export the diagram to PDF/A.
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram successfully exported to PDF/A at: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}