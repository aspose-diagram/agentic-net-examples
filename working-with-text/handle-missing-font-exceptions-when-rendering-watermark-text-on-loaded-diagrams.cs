using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the output PDF
                string outputPath = "output.pdf";

                // Desired watermark font
                string desiredFont = "Calibri";
                // Fallback font to use when the desired font is missing
                string fallbackFont = "Arial";

                // Configure global default font before loading the diagram
                FontConfigs.DefaultFontName = fallbackFont;

                // Check if the desired font is installed on the system
                bool fontAvailable = false;
                InstalledFontCollection installedFonts = new InstalledFontCollection();
                foreach (var fontFamily in installedFonts.Families)
                {
                    // FontFamily may not have a strongly typed definition; use dynamic property access
                    try
                    {
                        if (string.Equals(fontFamily.Name, desiredFont, StringComparison.OrdinalIgnoreCase))
                        {
                            fontAvailable = true;
                            break;
                        }
                    }
                    catch
                    {
                        // Ignore any unexpected property access issues
                    }
                }

                // Choose the font to use for the watermark
                string watermarkFont = fontAvailable ? desiredFont : fallbackFont;
                if (!fontAvailable)
                {
                    Console.WriteLine($"Warning: Desired font \"{desiredFont}\" not found. Using fallback font \"{fallbackFont}\".");
                }

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (assumes at least one page exists)
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Watermark text and styling
                string watermarkText = "CONFIDENTIAL";
                string fontColor = "#FF0000"; // Red color in hex
                double fontSizePoints = 72; // 72 points = 1 inch
                double fontSizeInches = fontSizePoints / 72.0;

                // Add the watermark text shape covering the full page
                Shape watermarkShape = page.AddText(
                    pinX,                // PinX (center X)
                    pinY,                // PinY (center Y)
                    pageWidth,           // Width of the text box
                    pageHeight,          // Height of the text box
                    watermarkText,       // Text content
                    watermarkFont,       // Font name
                    fontColor,           // Font color
                    fontSizeInches       // Font size in inches
                );

                // Optional: send the watermark to the back so it doesn't obscure other shapes
                page.SendToBack(watermarkShape.ID);

                // Configure PDF save options with the same fallback font
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = fallbackFont;

                // Save the diagram as PDF
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Diagram saved with watermark to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }