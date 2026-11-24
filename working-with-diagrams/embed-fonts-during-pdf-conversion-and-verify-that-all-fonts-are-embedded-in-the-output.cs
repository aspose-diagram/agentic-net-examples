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
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure font folder (system fonts) for rendering.
                // Adjust the path if running on a non-Windows OS.
                string systemFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts));
                FontConfigs.SetFontFolder(systemFontPath, true);

                // Set a fallback default font for missing fonts.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF.
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram saved to PDF: {outputPath}");

                // Verify that all fonts used in the diagram are available on the system.
                // This does not guarantee embedding, but ensures no missing fonts.
                InstalledFontCollection systemFonts = new InstalledFontCollection();

                bool allFontsAvailable = true;

                foreach (Font diagramFont in diagram.Fonts)
                {
                    // Check if the font name exists in the installed system fonts (case‑insensitive).
                    bool found = systemFonts.Families
                        .Any(f => string.Equals(f.Name, diagramFont.Name, StringComparison.OrdinalIgnoreCase));

                    if (!found)
                    {
                        allFontsAvailable = false;
                        Console.WriteLine($"Warning: Font \"{diagramFont.Name}\" used in diagram is not installed on the system.");
                    }
                }

                if (allFontsAvailable)
                {
                    Console.WriteLine("All fonts used in the diagram are available on the system.");
                }
                else
                {
                    Console.WriteLine("Some fonts are missing on the system. PDF may use fallback fonts.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}