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

                // Paths for input diagram and output PDF
                string inputPath = "input.vsdx";
                string outputPath = "output.pdf";

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Configure font folder (required before any rendering)
                    string systemFontPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
                    FontConfigs.SetFontFolder(systemFontPath, true);

                    // Build a set of installed font names for quick lookup
                    var installedFontNames = new InstalledFontCollection()
                                                .Families
                                                .Select(f => f.Name)
                                                .ToHashSet(StringComparer.OrdinalIgnoreCase);

                    // Detect if any diagram font is missing on the system
                    bool missingFont = false;
                    foreach (Font font in diagram.Fonts) // explicit type as required
                    {
                        if (!installedFontNames.Contains(font.Name))
                        {
                            missingFont = true;
                            break;
                        }
                    }

                    // If a font is missing, set a fallback default font
                    if (missingFont)
                    {
                        FontConfigs.DefaultFontName = "Arial";
                    }

                    // Get the first page (you can adjust to target a specific page)
                    Page page = diagram.Pages[0];

                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Watermark properties
                    string watermarkText = "CONFIDENTIAL";
                    string watermarkFont = "Arial";
                    string watermarkColor = "#FF0000"; // Red color in HEX
                    double fontSizePoints = 72; // 72 points = 1 inch
                    double fontSizeInches = fontSizePoints / 72.0;

                    // Add the watermark text covering the full page
                    page.AddText(pinX, pinY, pageWidth, pageHeight,
                                 watermarkText, watermarkFont, watermarkColor, fontSizeInches);

                    // Prepare PDF save options with a default font fallback
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial";
                    pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                    // Save the diagram as PDF
                    diagram.Save(outputPath, pdfOptions);

                    Console.WriteLine("Diagram saved successfully with watermark.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }