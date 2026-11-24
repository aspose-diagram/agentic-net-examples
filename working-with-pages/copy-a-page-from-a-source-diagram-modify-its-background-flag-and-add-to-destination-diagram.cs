using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <sourceDiagramPath> <destinationDiagramPath> <outputDiagramPath>");
            return;
        }

        string sourcePath = args[0];
        string destinationPath = args[1];
        string outputPath = args[2];

        // Guard against missing files.
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }

        try
        {
            // Load the source diagram inside a using block.
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                // Load the destination diagram inside a using block.
                using (Diagram destDiagram = new Diagram(destinationPath))
                {
                    // ------------------------------------------------------------
                    // Copy masters from source to destination (required for page copy).
                    // ------------------------------------------------------------
                    foreach (Master srcMaster in sourceDiagram.Masters)
                    {
                        // Add the master to the destination diagram.
                        // If a master with the same name already exists, Aspose.Diagram will handle duplication internally.
                        destDiagram.Masters.Add(srcMaster);
                    }

                    // ------------------------------------------------------------
                    // Retrieve the first page from the source diagram.
                    // ------------------------------------------------------------
                    if (sourceDiagram.Pages.Count == 0)
                    {
                        Console.Error.WriteLine("Source diagram contains no pages to copy.");
                        return;
                    }

                    Page srcPage = sourceDiagram.Pages[0];

                    // ------------------------------------------------------------
                    // Create a new page instance and copy the source page content.
                    // ------------------------------------------------------------
                    Page newPage = new Page();
                    newPage.Copy(srcPage); // Copies PageSheet, shapes, and other page data.

                    // ------------------------------------------------------------
                    // Modify the background flag of the copied page.
                    // ------------------------------------------------------------
                    newPage.Background = BOOL.True; // Mark the page as a background page.

                    // ------------------------------------------------------------
                    // Add the modified page to the destination diagram.
                    // ------------------------------------------------------------
                    destDiagram.Pages.Add(newPage);

                    // ------------------------------------------------------------
                    // Save the updated destination diagram to the specified output path.
                    // ------------------------------------------------------------
                    destDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Page copied and background flag set. Output saved to: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}