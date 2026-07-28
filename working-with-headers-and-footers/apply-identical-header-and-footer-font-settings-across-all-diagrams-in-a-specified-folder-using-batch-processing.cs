using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder to process: use first argument or current directory
        string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Supported Visio file extensions
        string[] extensions = new[] { "*.vsdx", "*.vsd", "*.vdx", "*.vsdm", "*.vssx", "*.vstx" };

        // Collect all diagram files in the folder (non‑recursive)
        var diagramFiles = new System.Collections.Generic.List<string>();
        foreach (var ext in extensions)
        {
            diagramFiles.AddRange(Directory.GetFiles(folderPath, ext, SearchOption.TopDirectoryOnly));
        }

        if (diagramFiles.Count == 0)
        {
            Console.WriteLine("No Visio diagram files found in the specified folder.");
            return;
        }

        foreach (var filePath in diagramFiles)
        {
            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Apply global header/footer font settings
                var headerFooterFont = diagram.HeaderFooter.HeaderFooterFont;
                headerFooterFont.FaceName = "Arial";          // Font family
                headerFooterFont.Weight = 700;                // Bold (700 = Bold, 400 = Regular)
                headerFooterFont.Height = -16;                // Point size mapping (e.g., -16 ≈ 12pt)
                headerFooterFont.Italic = BOOL.True;          // Italic style
                headerFooterFont.Underline = BOOL.False;      // No underline

                // Optional: set a common color for header/footer text
                diagram.HeaderFooter.HeaderFooterColor = Color.Black;

                // Save the diagram back to the same file (overwrites original)
                diagram.Save(filePath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
