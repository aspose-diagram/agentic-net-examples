using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputHtmlFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output HTML path. If not supplied, place it beside the input file.
        string outputPath = args.Length >= 2 ? args[1] : Path.ChangeExtension(inputPath, ".html");

        try
        {
            // -----------------------------------------------------------------
            // Configure Aspose.Diagram to use a web‑safe fallback font.
            // This ensures that any missing fonts are rendered with Arial.
            // -----------------------------------------------------------------
            FontConfigs.DefaultFontName = "Arial";

            // -----------------------------------------------------------------
            // Load the Visio diagram.
            // -----------------------------------------------------------------
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Define a list of web‑safe fonts.
            // -----------------------------------------------------------------
            List<string> webSafeFonts = new List<string>
            {
                "Arial", "Helvetica", "Times New Roman", "Courier New", "Verdana",
                "Georgia", "Trebuchet MS", "Comic Sans MS", "Impact", "Tahoma"
            };

            // -----------------------------------------------------------------
            // Enumerate installed system fonts using Aspose.Drawing.Text.
            // -----------------------------------------------------------------
            InstalledFontCollection systemFonts = new InstalledFontCollection();
            HashSet<string> installedFontNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var family in systemFonts.Families)
            {
                // The FontFamily type is not strongly typed here to avoid compile issues.
                // Assume it has a Name property.
                installedFontNames.Add(family.Name);
            }

            // -----------------------------------------------------------------
            // Check each font used in the diagram against the web‑safe list.
            // -----------------------------------------------------------------
            foreach (Font font in diagram.Fonts)
            {
                string fontName = font.Name;
                if (!webSafeFonts.Contains(fontName))
                {
                    Console.WriteLine($"Warning: Font \"{fontName}\" is not a web‑safe font. It will be substituted with the default font.");
                }
                else if (!installedFontNames.Contains(fontName))
                {
                    Console.WriteLine($"Warning: Web‑safe font \"{fontName}\" is not installed on this system.");
                }
            }

            // -----------------------------------------------------------------
            // Prepare HTML save options.
            // -----------------------------------------------------------------
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Export only visible pages and omit comments for a cleaner output.
                ExportHiddenPage = false,          // BOOL is not used here; plain bool required.
                IsExportComments = false          // BOOL is not used here; plain bool required.
            };

            // -----------------------------------------------------------------
            // Save the diagram to HTML.
            // -----------------------------------------------------------------
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully exported to HTML: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
