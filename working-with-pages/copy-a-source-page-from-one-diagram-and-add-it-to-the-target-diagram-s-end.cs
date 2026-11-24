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
            Console.Error.WriteLine("Usage: Program <sourceDiagramPath> <targetDiagramPath> <outputDiagramPath>");
            return;
        }

        string sourcePath = args[0];
        string targetPath = args[1];
        string outputPath = args[2];

        // Guard against missing files.
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        try
        {
            // Load the source diagram inside a using block.
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                // Load the target diagram inside a using block.
                using (Diagram targetDiagram = new Diagram(targetPath))
                {
                    // ------------------------------------------------------------
                    // Step 1: Retrieve the first page from the source diagram.
                    // ------------------------------------------------------------
                    Page sourcePage = sourceDiagram.Pages[0];

                    // ------------------------------------------------------------
                    // Step 2: Copy all masters from source to target (required for shapes on the page).
                    // ------------------------------------------------------------
                    foreach (Master srcMaster in sourceDiagram.Masters)
                    {
                        // Add the master to the target diagram's master collection.
                        targetDiagram.Masters.Add(srcMaster);
                    }

                    // ------------------------------------------------------------
                    // Step 3: Create a new page in the target diagram and copy the source page content.
                    // ------------------------------------------------------------
                    Page newPage = new Page();
                    newPage.Copy(sourcePage);

                    // ------------------------------------------------------------
                    // Step 4: Assign a unique ID and a distinct name to the new page.
                    // ------------------------------------------------------------
                    int maxPageId = 0;
                    foreach (Page p in targetDiagram.Pages)
                    {
                        if (p.ID > maxPageId)
                            maxPageId = p.ID;
                    }
                    newPage.ID = maxPageId + 1;
                    newPage.Name = sourcePage.Name + "_Copy";

                    // ------------------------------------------------------------
                    // Step 5: Append the new page to the end of the target diagram.
                    // ------------------------------------------------------------
                    targetDiagram.Pages.Add(newPage);

                    // ------------------------------------------------------------
                    // Step 6: Save the modified target diagram to the specified output path.
                    // ------------------------------------------------------------
                    targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Page '{sourcePage.Name}' copied successfully to '{outputPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}