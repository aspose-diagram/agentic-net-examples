using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <sourceVisioPath> <backupVisioPath> <outputVisioPath>");
            return;
        }

        string sourcePath = args[0];
        string backupPath = args[1];
        string outputPath = args[2];

        // Guard: source file must exist.
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Error: Source file not found: {sourcePath}");
            return;
        }

        // Guard: backup directory must exist.
        string backupDir = Path.GetDirectoryName(backupPath);
        if (!string.IsNullOrEmpty(backupDir) && !Directory.Exists(backupDir))
        {
            Console.Error.WriteLine($"Error: Backup directory does not exist: {backupDir}");
            return;
        }

        // Guard: output directory must exist.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Error: Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the source Visio diagram.
            Diagram diagram = new Diagram(sourcePath);

            // NOTE: Diagram.Clone is not available in the current Aspose.Diagram API.
            // As an alternative, save the loaded diagram to the backup path before modifications.
            diagram.Save(backupPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Backup saved to: {backupPath}");

            // Bulk update: set all pages to Landscape orientation.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Ensure the PrintProps hierarchy exists before setting orientation.
                if (page.PageSheet != null &&
                    page.PageSheet.PrintProps != null &&
                    page.PageSheet.PrintProps.PrintPageOrientation != null)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }
                else
                {
                    Console.Error.WriteLine($"Warning: Unable to set orientation for page index {i} (missing PrintProps).");
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}