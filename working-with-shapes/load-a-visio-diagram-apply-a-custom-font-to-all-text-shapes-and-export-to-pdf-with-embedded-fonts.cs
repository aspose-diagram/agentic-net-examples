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
            try
            {

                // Input Visio file path and output PDF path.
                // You can replace these with your actual file locations or pass them via command‑line arguments.
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.pdf";

                // -----------------------------------------------------------------
                // 1. Configure font sources before loading the diagram.
                // -----------------------------------------------------------------
                // Add the system fonts folder (recursive) so Aspose.Diagram can locate fonts.
                FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);

                // Define the custom font you want to apply to all text shapes.
                string customFontName = "Calibri";

                // Set the default fallback font for rendering (used when a font is missing).
                FontConfigs.DefaultFontName = customFontName;

                // -----------------------------------------------------------------
                // 2. Load the Visio diagram.
                // -----------------------------------------------------------------
                Diagram diagram = new Diagram(inputPath);

                // -----------------------------------------------------------------
                // 3. Validate that the diagram's fonts are installed on the system.
                // -----------------------------------------------------------------
                var installedFontNames = new InstalledFontCollection()
                                            .Families
                                            .Select(f => f.Name)
                                            .ToList();

                foreach (Font font in diagram.Fonts)
                {
                    if (!installedFontNames.Contains(font.Name))
                    {
                        Console.WriteLine($"Warning: Font \"{font.Name}\" used in the diagram is not installed on this machine.");
                    }
                }

                // -----------------------------------------------------------------
                // 4. Apply the custom font to every shape that contains text.
                // -----------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Ensure the shape actually has text.
                        if (shape.Text != null && !string.IsNullOrEmpty(shape.Text.Value.Text))
                        {
                            // If the shape has no character formatting entries, create one.
                            if (shape.Chars.Count == 0)
                            {
                                Aspose.Diagram.Char newChar = new Aspose.Diagram.Char();
                                newChar.IX = 0; // start index
                                newChar.FontName.Value = customFontName;
                                shape.Chars.Add(newChar);
                            }
                            else
                            {
                                // Update existing character runs.
                                foreach (Aspose.Diagram.Char ch in shape.Chars)
                                {
                                    ch.FontName.Value = customFontName;
                                }
                            }
                        }
                    }
                }

                // -----------------------------------------------------------------
                // 5. Save the diagram as PDF with embedded fonts.
                // -----------------------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Use the same custom font as the default for any missing glyphs.
                    DefaultFont = customFontName,
                    // Explicitly set the format (required for some scenarios).
                    SaveFormat = SaveFileFormat.Pdf,
                    // Export hidden pages if needed (set to false to exclude them).
                    ExportHiddenPage = false
                };

                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Diagram has been saved to PDF at: {Path.GetFullPath(outputPath)}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }