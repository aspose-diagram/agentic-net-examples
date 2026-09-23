using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the source and target Visio files
                string sourcePath = "source.vsdx";
                string targetPath = "target.vsdx";
                string outputPath = "merged.vsdx";

                // Index of the page to copy from the source diagram (0‑based)
                int sourcePageIndex = 0;

                // Desired insertion index in the target diagram (0‑based)
                int insertIndex = 1;

                try
                {
                    // Load the source and target diagrams
                    Diagram srcDiagram = new Diagram(sourcePath);
                    Diagram tgtDiagram = new Diagram(targetPath);

                    // Retrieve the page to be copied from the source diagram
                    Page srcPage = srcDiagram.Pages[sourcePageIndex];

                    // Determine the maximum existing page ID in the target diagram
                    int maxPageId = 0;
                    foreach (Page p in tgtDiagram.Pages)
                    {
                        if (p.ID > maxPageId)
                            maxPageId = p.ID;
                    }

                    // Create a new page instance and assign a unique ID
                    Page newPage = new Page();
                    newPage.ID = maxPageId + 1;
                    newPage.Name = srcPage.Name; // Preserve the original page name (optional)

                    // Copy the contents of the source page into the new page
                    newPage.Copy(srcPage);

                    // Add the new page to the target diagram (appended at the end)
                    tgtDiagram.Pages.Add(newPage);

                    // Move the newly added page to the desired insertion index
                    newPage.MoveTo(insertIndex);

                    // Save the modified target diagram
                    tgtDiagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine("Page copied successfully.");
                    Console.WriteLine($"Source page '{srcPage.Name}' (ID {srcPage.ID}) copied to '{outputPath}' at index {insertIndex}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred during the page copy operation:");
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }