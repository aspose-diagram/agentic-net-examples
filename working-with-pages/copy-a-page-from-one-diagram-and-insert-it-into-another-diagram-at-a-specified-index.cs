using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: <sourceDiagramPath> <targetDiagramPath> <outputDiagramPath> <sourcePageIndex> <insertIndex>");
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

        // Parse page indices.
        if (!int.TryParse(args[3], out int sourcePageIndex))
        {
            Console.Error.WriteLine("Invalid source page index.");
            return;
        }
        if (!int.TryParse(args[4], out int insertIndex))
        {
            Console.Error.WriteLine("Invalid insert index.");
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
                    Console.Error.WriteLine("Source page index is out of range.");
                    return;
                }

                // Retrieve the page to be copied.
                Page sourcePage = sourceDiagram.Pages[sourcePageIndex];

                // Load the target diagram.
                using (Diagram targetDiagram = new Diagram(targetPath))
                {
                    // Create a new page and copy contents from the source page.
                    Page newPage = new Page();
                    newPage.Copy(sourcePage);

                    // Ensure the new page has a unique ID within the target diagram.
                    int maxId = 0; // Changed to int to match Page.ID type.
                    foreach (Page p in targetDiagram.Pages)
                    {
                        if (p.ID > maxId)
                            maxId = p.ID;
                    }
                    newPage.ID = maxId + 1;

                    // Add the new page to the target diagram (appended at the end).
                    targetDiagram.Pages.Add(newPage);

                    // If the requested insert index is within the valid range, move the page.
                    if (insertIndex >= 0 && insertIndex < targetDiagram.Pages.Count)
                    {
                        // MoveTo repositions the page within the collection.
                        newPage.MoveTo(insertIndex);
                    }

                    // Save the modified diagram to the output path.
                    targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Page copied from '{sourcePath}' (index {sourcePageIndex}) and inserted into '{targetPath}' at index {insertIndex}.");
                    Console.WriteLine($"Result saved to: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
