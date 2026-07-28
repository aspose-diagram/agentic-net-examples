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

                // Paths to input Visio file and output PDF
                string inputPath = "input.vsdx";
                string outputPath = "output.pdf";

                // Configure font folder (adjust the path as needed)
                // The second argument indicates whether to search subfolders recursively
                FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Verify that every font used in the diagram is installed on the system
                InstalledFontCollection systemFonts = new InstalledFontCollection();
                var installedFontNames = systemFonts.Families
                    .Select(f => f.Name)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                foreach (Font font in diagram.Fonts)
                {
                    if (!installedFontNames.Contains(font.Name))
                    {
                        throw new Exception($"Font '{font.Name}' used in the diagram is not installed on the system.");
                    }
                }

                // Set PDF save options and specify a fallback default font
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    DefaultFont = "Arial"
                };

                // Save the diagram as PDF; fonts will be embedded automatically
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("PDF saved successfully with all fonts embedded.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }