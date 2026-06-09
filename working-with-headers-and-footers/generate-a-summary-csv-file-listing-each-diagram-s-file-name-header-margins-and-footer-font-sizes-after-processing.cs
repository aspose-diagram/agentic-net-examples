using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Folder containing diagram files (change as needed)
        string inputFolder = Directory.GetCurrentDirectory();

        // Output CSV file path
        string outputCsvPath = Path.Combine(inputFolder, "DiagramSummary.csv");

        // Prepare a list to hold CSV rows
        List<string> csvLines = new List<string>();
        // Header row
        csvLines.Add("FileName,HeaderMargin,FooterMargin,FooterFontSize");

        // Get all Visio diagram files in the folder (common extensions)
        string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in diagramFiles)
        {
            // Filter by known Visio extensions (case‑insensitive)
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx" && extension != ".vssx" && extension != ".vss" && extension != ".vstx" && extension != ".vst")
            {
                continue;
            }

            // Guard for file existence
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Retrieve header and footer margins (in inches)
                double headerMargin = diagram.HeaderFooter.HeaderMargin.Value;
                double footerMargin = diagram.HeaderFooter.FooterMargin.Value;

                // Retrieve footer font size (height property represents size)
                int footerFontSize = diagram.HeaderFooter.HeaderFooterFont.Height;

                // Build CSV line
                string fileName = Path.GetFileName(filePath);
                string line = $"{fileName},{headerMargin},{footerMargin},{footerFontSize}";
                csvLines.Add(line);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }

        // Write all lines to the CSV file
        StringBuilder sb = new StringBuilder();
        foreach (string line in csvLines)
        {
            sb.AppendLine(line);
        }
        File.WriteAllText(outputCsvPath, sb.ToString());

        Console.WriteLine($"Summary CSV generated at: {outputCsvPath}");
    }
}