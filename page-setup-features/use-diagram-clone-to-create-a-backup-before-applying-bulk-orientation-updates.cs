using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string sourcePath = "input.vsdx";
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        // Paths for backup and output files
        string backupPath = "backup.vsdx";
        string outputPath = "output.vsdx";

        // Load the diagram
        Diagram diagram = new Diagram(sourcePath);
        try
        {
            // Save a backup before making changes
            diagram.Save(backupPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Backup saved to '{backupPath}'.");

            // Apply bulk orientation updates (example: set all pages to Landscape)
            foreach (Page page in diagram.Pages)
            {
                if (page.PageSheet?.PrintProps != null)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("An error occurred: " + ex.Message);
        }
    }
}