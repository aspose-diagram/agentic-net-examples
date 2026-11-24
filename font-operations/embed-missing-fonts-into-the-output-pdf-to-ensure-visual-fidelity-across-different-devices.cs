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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile> [fontFolder]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string fontFolder = args.Length >= 3 ? args[2] : null;

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // If a custom font folder is supplied, verify it exists.
        if (!string.IsNullOrEmpty(fontFolder) && !Directory.Exists(fontFolder))
        {
            Console.Error.WriteLine($"Font folder not found: {fontFolder}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // Configure font handling for Aspose.Diagram.
            // -----------------------------------------------------------------
            // Set a default fallback font name.
            FontConfigs.DefaultFontName = "Arial";

            // Add custom font directory to the search paths if provided.
            if (!string.IsNullOrEmpty(fontFolder))
            {
                // The second argument enables recursive search.
                FontConfigs.SetFontFolder(fontFolder, true);
            }

            // -----------------------------------------------------------------
            // Load the Visio diagram inside a using block (mandatory pattern).
            // -----------------------------------------------------------------
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Optional: Validate that all fonts used in the diagram are installed.
                // -----------------------------------------------------------------
                // Enumerate system installed fonts using Aspose.Drawing.Text.
                InstalledFontCollection installedFontCollection = new InstalledFontCollection();
                var installedNames = installedFontCollection.Families
                    .Select(f => f.Name)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                // Iterate over fonts referenced by the diagram.
                foreach (Font font in diagram.Fonts)
                {
                    if (!installedNames.Contains(font.Name))
                    {
                        Console.WriteLine($"Warning: Font \"{font.Name}\" used in diagram is not installed on the system.");
                    }
                }

                // -----------------------------------------------------------------
                // Prepare PDF save options.
                // -----------------------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Set the default font to use when a required font is missing.
                pdfOptions.DefaultFont = "Arial";

                // -----------------------------------------------------------------
                // Save the diagram to PDF.
                // -----------------------------------------------------------------
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}