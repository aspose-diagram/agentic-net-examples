using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input file paths
        string sourcePath = "source.vsdx";
        if (!System.IO.File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }
        string targetPath = "target.vsdx";
        string outputPath = "merged.vsdx";

        // Guard clauses for input files
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

        // Index of the page to copy from source diagram (0‑based)
        int sourcePageIndex = 0; // adjust as needed

        // Desired insertion index in the target diagram (0‑based)
        int insertIndex = 1; // adjust as needed

        try
        {
            // Load source diagram
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                // Load target diagram
                using (Diagram targetDiagram = new Diagram(targetPath))
                {
                    // Validate source page index
                    if (sourcePageIndex < 0 || sourcePageIndex >= sourceDiagram.Pages.Count)
                    {
                        Console.Error.WriteLine("Invalid source page index.");
                        return;
                    }

                    // Validate insertion index (must be within current page count)
                    if (insertIndex < 0 || insertIndex > targetDiagram.Pages.Count)
                    {
                        Console.Error.WriteLine("Invalid insertion index for target diagram.");
                        return;
                    }

                    // ------------------------------------------------------------
                    // Copy masters from source to target (avoid duplicates)
                    // ------------------------------------------------------------
                    foreach (Master srcMaster in sourceDiagram.Masters)
                    {
                        bool exists = false;
                        foreach (Master tgtMaster in targetDiagram.Masters)
                        {
                            if (tgtMaster.NameU == srcMaster.NameU)
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (!exists)
                        {
                            targetDiagram.Masters.Add(srcMaster);
                        }
                    }

                    // ------------------------------------------------------------
                    // Retrieve the page to copy from the source diagram
                    // ------------------------------------------------------------
                    Page srcPage = sourceDiagram.Pages[sourcePageIndex];

                    // ------------------------------------------------------------
                    // Create a new page and copy contents from the source page
                    // ------------------------------------------------------------
                    Page newPage = new Page();
                    newPage.Copy(srcPage);

                    // ------------------------------------------------------------
                    // Assign a new unique ID to the copied page
                    // ------------------------------------------------------------
                    int maxId = 0; // ID is an int, so use int for maxId
                    foreach (Page p in targetDiagram.Pages)
                    {
                        if (p.ID > maxId)
                        {
                            maxId = p.ID;
                        }
                    }
                    newPage.ID = maxId + 1; // assign new unique int ID

                    // ------------------------------------------------------------
                    // Optionally rename the copied page
                    // ------------------------------------------------------------
                    newPage.Name = srcPage.Name + "_Copy";

                    // ------------------------------------------------------------
                    // Add the new page to the target diagram
                    // ------------------------------------------------------------
                    targetDiagram.Pages.Add(newPage);

                    // ------------------------------------------------------------
                    // Move the newly added page to the desired insertion index
                    // ------------------------------------------------------------
                    newPage.MoveTo(insertIndex);

                    // ------------------------------------------------------------
                    // Save the modified target diagram
                    // ------------------------------------------------------------
                    targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Page copied successfully. Output saved to: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
