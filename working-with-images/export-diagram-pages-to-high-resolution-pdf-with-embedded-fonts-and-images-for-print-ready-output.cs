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
        // Expect input Visio file path and output PDF file path as arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate that all fonts used in the diagram are installed on the system
                ValidateFonts(diagram);

                // Configure PDF save options for high‑resolution, print‑ready output
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";                     // Fallback font if a font is missing
                pdfOptions.ExportHiddenPage = false;                 // Do not export hidden pages (bool, not BOOL)
                pdfOptions.Compliance = PdfCompliance.PdfA1b;        // PDF/A‑1b compliance for print readiness
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;          // Explicitly set format
                pdfOptions.PageSavingCallback = new PdfPageSavingCallback(); // Log page‑saving events

                // Save the diagram to PDF using the configured options
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"PDF saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Checks each font used in the diagram against installed system fonts
    static void ValidateFonts(Diagram diagram)
    {
        // Retrieve installed font family names
        InstalledFontCollection installedFonts = new InstalledFontCollection();
        var installedNames = installedFonts.Families
                                            .Select(f => f.Name)
                                            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        // Iterate over fonts referenced in the diagram
        foreach (Font font in diagram.Fonts)
        {
            string fontName = font.Name;
            if (!string.IsNullOrEmpty(fontName) && !installedNames.Contains(fontName))
            {
                Console.WriteLine($"Warning: Font '{fontName}' used in diagram is not installed on the system.");
            }
        }
    }
}

// Custom callback to log page‑saving events during PDF export
class PdfPageSavingCallback : Aspose.Diagram.Saving.IPageSavingCallback
{
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}");
    }

    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1}");
    }
}
