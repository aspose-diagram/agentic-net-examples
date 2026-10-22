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
        // Expect two arguments: input diagram path and output PDF/A path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Step 1: Validate that all text characters meet minimum font size
            // -----------------------------------------------------------------
            const double MinimumFontSizePoints = 12.0; // points, per accessibility guidelines
            bool sizeViolationFound = false;

            // Iterate through all pages, shapes, and characters
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has character formatting data
                    if (shape.Chars == null)
                        continue;

                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        // Font size is stored in inches; convert to points (1 inch = 72 points)
                        double fontSizePoints = ch.Size.Value * 72.0;
                        if (fontSizePoints < MinimumFontSizePoints)
                        {
                            sizeViolationFound = true;
                            Console.Error.WriteLine(
                                $"Font size violation: Page {page.ID}, Shape ID {shape.ID} has size {fontSizePoints:F2}pt (minimum {MinimumFontSizePoints}pt).");
                        }
                    }
                }
            }

            if (sizeViolationFound)
            {
                Console.Error.WriteLine("One or more font size violations were detected. Export aborted.");
                return;
            }

            // -----------------------------------------------------------------
            // Step 2: Validate that all fonts used in the diagram are installed
            // -----------------------------------------------------------------
            // Gather installed font family names using Aspose.Drawing.Text
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            var installedFontNames = installedFonts.Families
                .Select(f => f.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            // Iterate over fonts referenced by the diagram
            foreach (Font font in diagram.Fonts)
            {
                string fontName = font.Name;
                if (!installedFontNames.Contains(fontName))
                {
                    Console.Error.WriteLine($"Warning: Font '{fontName}' used in the diagram is not installed on the system.");
                }
            }

            // -----------------------------------------------------------------
            // Step 3: Configure fallback/default font for rendering
            // -----------------------------------------------------------------
            // Use Arial if available; otherwise pick any installed font
            string fallbackFont = installedFontNames.Contains("Arial")
                ? "Arial"
                : installedFontNames.FirstOrDefault() ?? "Arial";

            FontConfigs.DefaultFontName = fallbackFont;

            // -----------------------------------------------------------------
            // Step 4: Export diagram to PDF/A (PDF/A-1b compliance)
            // -----------------------------------------------------------------
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.Compliance = PdfCompliance.PdfA1b; // PDF/A-1b compliance
            pdfOptions.DefaultFont = fallbackFont;        // Set default font for missing glyphs
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;   // Explicitly set the format

            // Save the diagram as PDF/A
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully exported to PDF/A at: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}