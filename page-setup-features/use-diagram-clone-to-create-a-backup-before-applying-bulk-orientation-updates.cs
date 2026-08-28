using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Paths for the original diagram, the modified output, and the backup copy
        string inputPath = "input.vsdx";
        // Guard to ensure the source file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output_modified.vsdx";
        string backupPath = "backup.vsdx";

        try
        {
            // Load the original diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a backup by loading the same file again (Clone not available)
            Diagram backupDiagram = new Diagram(inputPath);

            // Apply bulk orientation updates: set every page to Landscape orientation
            foreach (Page page in diagram.Pages)
            {
                // Ensure the page sheet and print properties are available
                if (page.PageSheet != null && page.PageSheet.PrintProps != null)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Save the backup diagram
            backupDiagram.Save(backupPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram processing completed. Modified diagram saved to '{outputPath}', backup saved to '{backupPath}'.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}