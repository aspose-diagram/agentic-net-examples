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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // Configure font locations before any diagram processing.
            // -----------------------------------------------------------------
            string systemFontDir = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            if (Directory.Exists(systemFontDir))
            {
                // Register the system font folder (recursive search).
                FontConfigs.SetFontFolder(systemFontDir, true);
            }
            else
            {
                Console.Error.WriteLine($"Warning: System font directory not found: {systemFontDir}");
            }

            // Set a default fallback font for missing fonts.
            FontConfigs.DefaultFontName = "Arial";

            // -----------------------------------------------------------------
            // Load the Visio diagram.
            // -----------------------------------------------------------------
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Check for missing fonts used in the diagram.
            // -----------------------------------------------------------------
            var installedFonts = new InstalledFontCollection();
            var installedNames = installedFonts.Families
                .Select(f => f.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (Font diagramFont in diagram.Fonts)
            {
                string fontName = diagramFont.Name;
                if (!installedNames.Contains(fontName))
                {
                    Console.Error.WriteLine($"Warning: Font \"{fontName}\" used in diagram is not installed. It will be substituted with the default font.");
                }
            }

            // -----------------------------------------------------------------
            // Add a full‑page watermark to each page.
            // -----------------------------------------------------------------
            const string watermarkText = "Sample Watermark";
            const string watermarkFont = "Calibri"; // Font that may be missing.
            const string watermarkColor = "#a5a5a5";
            const double watermarkFontSizePoints = 72.0; // 1 inch.
            double watermarkFontSizeInches = watermarkFontSizePoints / 72.0;

            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions.
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark.
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Use full page dimensions for the text box.
                double width = pageWidth;
                double height = pageHeight;

                // Add the watermark text.
                page.AddText(pinX, pinY, width, height, watermarkText, watermarkFont, watermarkColor, watermarkFontSizeInches);
            }

            // -----------------------------------------------------------------
            // Prepare PDF save options with the default font fallback.
            // -----------------------------------------------------------------
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set the format tracker.

            // -----------------------------------------------------------------
            // Save the diagram as PDF.
            // -----------------------------------------------------------------
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}