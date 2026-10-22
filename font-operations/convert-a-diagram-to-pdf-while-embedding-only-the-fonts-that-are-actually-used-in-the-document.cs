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
            Console.Error.WriteLine("Usage: DiagramToPdf <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Validate that all fonts used in the diagram are installed on the system.
            // This step does not affect embedding; it only informs the user about missing fonts.
            // -----------------------------------------------------------------
            InstalledFontCollection installedFonts = new InstalledFontCollection();

            // Build a list of installed font family names for quick lookup.
            var installedFontNames = installedFonts.Families
                .Select(f => f.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (Aspose.Diagram.Font font in diagram.Fonts)
            {
                // Some Font objects may have an empty Name; skip those.
                if (string.IsNullOrWhiteSpace(font.Name))
                    continue;

                if (!installedFontNames.Contains(font.Name))
                {
                    Console.Error.WriteLine($"Warning: Font \"{font.Name}\" used in the diagram is not installed on this system.");
                }
            }

            // -----------------------------------------------------------------
            // Configure PDF save options.
            // By default Aspose.Diagram embeds only the fonts that are actually used.
            // No explicit property exists to control this, so we rely on the default behavior.
            // -----------------------------------------------------------------
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Explicitly set the format tracker (required by some versions of the API).
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Optional: specify a fallback font if a required font is missing.
            pdfOptions.DefaultFont = "Arial";

            // Ensure the output directory exists.
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Save the diagram as PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);

            // Dispose the diagram to free resources.
            diagram.Dispose();

            Console.WriteLine($"Diagram successfully converted to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}