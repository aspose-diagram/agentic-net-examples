using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <folderPath>");
            return;
        }

        string folderPath = args[0];

        // Verify that the specified folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Set a global fallback font name before loading any diagrams.
        FontConfigs.DefaultFontName = "Arial";

        // Get all files in the folder (filter common Visio extensions).
        string[] diagramFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in diagramFiles)
        {
            // Simple extension filter – process only Visio files.
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx" && ext != ".vsx" && ext != ".vtx")
            {
                continue;
            }

            // Guard against missing file (should not happen after GetFiles, but added per rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Access the global header/footer font object.
                HeaderFooterFont hfFont = diagram.HeaderFooter.HeaderFooterFont;

                // Apply identical font settings.
                hfFont.FaceName = "Arial";          // Font family name.
                hfFont.Height = -16;                // Height calculated for ~12pt (negative mapping as required).
                hfFont.Weight = 700;                // 700 = Bold weight.
                hfFont.Italic = BOOL.False;         // No italic.
                hfFont.Underline = BOOL.False;      // No underline.

                // Save the diagram back in its original format (using VSDX as a safe default).
                diagram.Save(filePath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }
}