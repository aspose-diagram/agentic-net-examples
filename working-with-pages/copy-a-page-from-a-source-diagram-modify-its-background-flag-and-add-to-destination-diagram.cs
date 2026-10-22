using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input file paths (modify as needed)
        string sourcePath = "source.vsdx";
        if (!System.IO.File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }
        string destinationPath = "destination.vsdx";
        string outputPath = "merged_output.vsdx";

        // Guard: ensure source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Guard: ensure destination file exists
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }

        try
        {
            // Load source diagram inside a using block
            using (Diagram srcDiagram = new Diagram(sourcePath))
            {
                // Load destination diagram inside a using block
                using (Diagram destDiagram = new Diagram(destinationPath))
                {
                    // ------------------------------------------------------------
                    // Copy masters from source to destination (if not already present)
                    // ------------------------------------------------------------
                    foreach (Master srcMaster in srcDiagram.Masters)
                    {
                        // Simple add; duplicate IDs are handled internally by Aspose.Diagram
                        destDiagram.Masters.Add(srcMaster);
                    }

                    // ------------------------------------------------------------
                    // Copy theme from source to destination
                    // ------------------------------------------------------------
                    destDiagram.CopyTheme(srcDiagram);

                    // ------------------------------------------------------------
                    // Retrieve the first page from the source diagram
                    // ------------------------------------------------------------
                    if (srcDiagram.Pages.Count == 0)
                    {
                        Console.Error.WriteLine("Source diagram contains no pages to copy.");
                        return;
                    }

                    Page srcPage = srcDiagram.Pages[0];

                    // ------------------------------------------------------------
                    // Create a new page in the destination diagram and copy contents
                    // ------------------------------------------------------------
                    Page newPage = new Page();
                    newPage.Copy(srcPage); // Clone the source page's content

                    // ------------------------------------------------------------
                    // Modify the background flag of the copied page
                    // ------------------------------------------------------------
                    newPage.Background = BOOL.True; // Mark as a background page

                    // ------------------------------------------------------------
                    // Add the new page to the destination diagram
                    // ------------------------------------------------------------
                    destDiagram.Pages.Add(newPage);

                    // ------------------------------------------------------------
                    // Save the updated destination diagram to the output file
                    // ------------------------------------------------------------
                    destDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Page copied and background flag set. Output saved to: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}