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
                // Path for the output PDF file
                string outputPdfPath = "output.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure font folder (system fonts) and default fallback font
                string systemFontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
                // The SetFontFolder method requires two parameters: path and recursive flag
                FontConfigs.SetFontFolder(systemFontFolder, true);
                // Set a default font to use when a required font is missing
                FontConfigs.DefaultFontName = "Arial";

                // Prepare PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Ensure the same default font is set for the save operation
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF
                diagram.Save(outputPdfPath, pdfOptions);
                Console.WriteLine($"Diagram saved to PDF: {outputPdfPath}");

                // Verify that all fonts used in the diagram are available on the system
                // (and therefore can be embedded in the PDF)
                InstalledFontCollection installedFonts = new InstalledFontCollection();

                bool allFontsEmbedded = true;
                foreach (Aspose.Diagram.Font diagramFont in diagram.Fonts)
                {
                    bool fontExists = installedFonts.Families.Any(f =>
                        string.Equals(f.Name, diagramFont.Name, StringComparison.OrdinalIgnoreCase));

                    if (!fontExists)
                    {
                        allFontsEmbedded = false;
                        Console.WriteLine($"Missing system font: {diagramFont.Name}");
                    }
                }

                if (allFontsEmbedded)
                {
                    Console.WriteLine("All fonts used in the diagram are available and will be embedded in the PDF.");
                }
                else
                {
                    throw new Exception("One or more fonts used in the diagram are not available on the system. PDF may have missing font embeddings.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }