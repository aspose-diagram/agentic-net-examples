using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [Landscape|Portrait]");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Determine desired orientation; default to Landscape if not specified or invalid
        string orientationArg = args.Length > 1 ? args[1] : "Landscape";
        PrintPageOrientationValue desiredOrientation = PrintPageOrientationValue.Landscape;
        if (string.Equals(orientationArg, "Portrait", StringComparison.OrdinalIgnoreCase))
        {
            desiredOrientation = PrintPageOrientationValue.Portrait;
        }
        else if (!string.Equals(orientationArg, "Landscape", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"Warning: Unrecognized orientation '{orientationArg}'. Defaulting to Landscape.");
        }

        try
        {
            // Load the original diagram inside a using block (mandatory disposal pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Build backup file name (original name + "_backup")
                string dir = Path.GetDirectoryName(inputPath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string backupPath = Path.Combine(dir, $"{fileNameWithoutExt}_backup.vsdx");

                // Save the backup before any modifications
                diagram.Save(backupPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Backup saved to: {backupPath}");

                // Apply bulk orientation update to all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Ensure the page has a PageSheet and PrintProps before accessing
                    if (page.PageSheet != null && page.PageSheet.PrintProps != null)
                    {
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = desiredOrientation;
                    }
                }

                // Save the modified diagram (original file is left untouched; a new file is created)
                string outputPath = Path.Combine(dir, $"{fileNameWithoutExt}_updated.vsdx");
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Updated diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or IO exceptions and report them
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}