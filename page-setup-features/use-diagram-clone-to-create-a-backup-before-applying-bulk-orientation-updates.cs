using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        // Paths for the source diagram and the output files
        string sourcePath = "input.vsdx";
        string updatedPath = "updated.vsdx";
        string backupPath = "backup.vsdx";

        // Verify the source file exists before proceeding
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the original diagram
            Diagram diagram = new Diagram(sourcePath);

            // Create a backup copy by saving the loaded diagram to a separate file
            diagram.Save(backupPath, SaveFileFormat.Vsdx);

            // Apply bulk orientation updates: set all pages to Landscape orientation
            foreach (Page page in diagram.Pages)
            {
                // Set the page's print orientation to Landscape
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
            }

            // Save the updated diagram with the new orientation settings
            diagram.Save(updatedPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}