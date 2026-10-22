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
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // Configure font directories before loading the diagram
        // ------------------------------------------------------------
        try
        {
            // Use the system fonts folder as a primary source
            string systemFonts = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            if (Directory.Exists(systemFonts))
            {
                // Enable recursive search
                FontConfigs.SetFontFolder(systemFonts, true);
            }
            else
            {
                Console.Error.WriteLine("System fonts folder not found. Font fallback may not work.");
            }

            // Set a default fallback font (used when the desired font is missing)
            FontConfigs.DefaultFontName = "Arial";
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error configuring fonts: {ex.Message}");
            return;
        }

        // ------------------------------------------------------------
        // Load the diagram inside a using block (mandatory)
        // ------------------------------------------------------------
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Determine watermark font, fallback if not installed
                // ------------------------------------------------------------
                string watermarkFont = "Calibri";
                string fallbackFont = FontConfigs.DefaultFontName;

                try
                {
                    InstalledFontCollection installedFonts = new InstalledFontCollection();
                    bool fontExists = installedFonts.Families.Any(f => string.Equals(f.Name, watermarkFont, StringComparison.OrdinalIgnoreCase));

                    if (!fontExists)
                    {
                        Console.Error.WriteLine($"Warning: Font '{watermarkFont}' is not installed. Falling back to '{fallbackFont}'.");
                        watermarkFont = fallbackFont;
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error checking installed fonts: {ex.Message}");
                    watermarkFont = fallbackFont;
                }

                // ------------------------------------------------------------
                // Add watermark text to each page
                // ------------------------------------------------------------
                string watermarkText = "CONFIDENTIAL";
                string fontColorHex = "#a5a5a5"; // Light gray
                double fontSizeInInches = 0.5;   // Approx. 36 points (0.5 inch)

                try
                {
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page dimensions (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Add a full‑page text shape as watermark
                        // Overload: AddText(pinX, pinY, width, height, text, fontName, fontColor, fontSize)
                        page.AddText(
                            0,                     // pinX
                            0,                     // pinY
                            pageWidth,             // width
                            pageHeight,            // height
                            watermarkText,         // text
                            watermarkFont,         // font name
                            fontColorHex,          // font color (hex)
                            fontSizeInInches);     // font size (in inches)
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error adding watermark: {ex.Message}");
                    return;
                }

                // ------------------------------------------------------------
                // Prepare PDF save options with fallback font
                // ------------------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicit format
                pdfOptions.DefaultFont = fallbackFont;      // Ensure fallback font is used

                // ------------------------------------------------------------
                // Save the diagram as PDF
                // ------------------------------------------------------------
                try
                {
                    diagram.Save(outputPath, pdfOptions);
                    Console.WriteLine($"Diagram saved successfully to: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
        }
    }
}