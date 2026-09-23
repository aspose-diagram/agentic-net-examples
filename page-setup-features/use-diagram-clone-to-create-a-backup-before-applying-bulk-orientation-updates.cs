using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string sourcePath = "input.vsdx";

        // Verify the source file exists before proceeding
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the original diagram from the file
            Diagram diagram = new Diagram(sourcePath);

            // Create a backup copy by saving to a memory stream and reloading
            using (MemoryStream backupStream = new MemoryStream())
            {
                // Save the original diagram into the stream in VSDX format
                diagram.Save(backupStream, SaveFileFormat.Vsdx);
                backupStream.Position = 0; // Reset stream position for reading

                // Load a new Diagram instance from the stream (this is the backup)
                Diagram backupDiagram = new Diagram(backupStream);

                // Apply bulk orientation update: set all pages to Landscape
                foreach (Page page in diagram.Pages)
                {
                    // Ensure the page has a PrintProps section before modifying
                    if (page.PageSheet != null && page.PageSheet.PrintProps != null)
                    {
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }
                }

                // Save the unchanged backup diagram
                backupDiagram.Save("backup.vsdx", SaveFileFormat.Vsdx);

                // Save the updated diagram with the new orientation
                diagram.Save("updated.vsdx", SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}