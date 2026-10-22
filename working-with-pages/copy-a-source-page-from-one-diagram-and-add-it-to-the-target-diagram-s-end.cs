using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: source diagram path, target diagram path, output diagram path.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <sourceDiagramPath> <targetDiagramPath> <outputDiagramPath> [sourcePageIndex]");
            return;
        }

        string sourcePath = args[0];
        string targetPath = args[1];
        string outputPath = args[2];
        int sourcePageIndex = 0;

        // Optional fourth argument to specify which page to copy (zero‑based index).
        if (args.Length >= 4 && !int.TryParse(args[3], out sourcePageIndex))
        {
            Console.Error.WriteLine("Invalid sourcePageIndex argument.");
            return;
        }

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
            // Load the source diagram.
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                // Validate source page index.
                if (sourcePageIndex < 0 || sourcePageIndex >= sourceDiagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Source page index {sourcePageIndex} is out of range. Diagram has {sourceDiagram.Pages.Count} pages.");
                    return;
                }

                // Retrieve the page to copy.
                Page sourcePage = sourceDiagram.Pages[sourcePageIndex];

                // Load the target diagram.
                using (Diagram targetDiagram = new Diagram(targetPath))
                {
                    // ------------------------------------------------------------
                    // Ensure that masters used by the source page exist in the target.
                    // ------------------------------------------------------------
                    foreach (Master srcMaster in sourceDiagram.Masters)
                    {
                        bool exists = false;
                        foreach (Master tgtMaster in targetDiagram.Masters)
                        {
                            if (tgtMaster.ID == srcMaster.ID)
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (!exists)
                        {
                            // Add the master from the source diagram to the target diagram.
                            // Directly adding the source master instance is acceptable here.
                            targetDiagram.Masters.Add(srcMaster);
                        }
                    }

                    // ------------------------------------------------------------
                    // Determine a new unique page ID for the copied page.
                    // ------------------------------------------------------------
                    int maxPageId = 0;
                    foreach (Page p in targetDiagram.Pages)
                    {
                        if (p.ID > maxPageId)
                            maxPageId = p.ID;
                    }

                    // ------------------------------------------------------------
                    // Create a new page in the target diagram and copy the source page contents.
                    // ------------------------------------------------------------
                    Page newPage = new Page();
                    newPage.Copy(sourcePage);
                    newPage.ID = maxPageId + 1; // Ensure a unique ID.
                    newPage.Name = sourcePage.Name + "_Copy"; // Optional: avoid name collisions.

                    // Add the new page to the end of the target diagram.
                    targetDiagram.Pages.Add(newPage);

                    // ------------------------------------------------------------
                    // Save the modified target diagram to the specified output path.
                    // ------------------------------------------------------------
                    targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Page '{sourcePage.Name}' copied successfully to '{outputPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}