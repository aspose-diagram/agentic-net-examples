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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Enumerate fonts used in the diagram.
                Console.WriteLine("Fonts used in the diagram:");
                InstalledFontCollection installedFonts = new InstalledFontCollection();

                foreach (Font font in diagram.Fonts)
                {
                    // Check if the font is installed on the system.
                    bool isInstalled = installedFonts.Families
                        .Any(f => string.Equals(f.Name, font.Name, StringComparison.OrdinalIgnoreCase));

                    Console.WriteLine($"- {font.Name} (Installed: {isInstalled})");
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Set the fallback font for any missing fonts.
                pdfOptions.DefaultFont = "Arial";

                // Explicitly set the format tracker.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as PDF.
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}