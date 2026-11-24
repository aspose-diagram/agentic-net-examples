using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing diagram files.
        string inputFolder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Prepare the CSV output file.
        string csvPath = Path.Combine(inputFolder, "summary.csv");
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("FileName,HeaderMargin,FooterFontSize");

        // Get all Visio files (VSDX and VSD) in the folder.
        string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        var supportedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".vsdx", ".vsd" };

        foreach (string filePath in diagramFiles)
        {
            if (!supportedExtensions.Contains(Path.GetExtension(filePath)))
                continue; // Skip non‑Visio files.

            // Guard against missing file (should not happen after GetFiles, but follow the rule).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // ----- Processing: modify header margin and footer font size -----
                // Set a new header margin (example: 0.5 inches).
                diagram.HeaderFooter.HeaderMargin.Value = 0.5;

                // Set footer font size to 12 points.
                // According to the specification, Height stores a negative scaled value:
                // Height = round(DesiredPointSize * -1.333)
                int desiredPointSize = 12;
                int scaledHeight = (int)Math.Round(desiredPointSize * -1.333);
                diagram.HeaderFooter.HeaderFooterFont.Height = scaledHeight;

                // Save the modified diagram back to the same file.
                diagram.Save(filePath, SaveFileFormat.Vsdx);
                // -----------------------------------------------------------------

                // Retrieve the values after processing.
                double headerMargin = diagram.HeaderFooter.HeaderMargin.Value;
                int footerFontHeight = diagram.HeaderFooter.HeaderFooterFont.Height;

                // Append a line to the CSV.
                string fileName = Path.GetFileName(filePath);
                csvBuilder.AppendLine($"{fileName},{headerMargin},{footerFontHeight}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }

        // Write the CSV file.
        try
        {
            File.WriteAllText(csvPath, csvBuilder.ToString());
            Console.WriteLine($"Summary CSV created at: {csvPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write CSV file: {ex.Message}");
        }
    }
}