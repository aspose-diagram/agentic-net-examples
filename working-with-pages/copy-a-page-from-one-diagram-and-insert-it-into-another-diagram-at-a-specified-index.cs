using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments:
            // args[0] - source diagram file path
            // args[1] - target diagram file path
            // args[2] - index of the page to copy from source (zero‑based)
            // args[3] - index at which to insert the copied page into target (zero‑based)
            // args[4] - output diagram file path (where the modified target will be saved)

            if (args.Length < 5)
            {
                Console.WriteLine("Usage: DiagramPageCopyExample <sourcePath> <targetPath> <sourcePageIndex> <insertIndex> <outputPath>");
                return;
            }

            string sourcePath = args[0];
            string targetPath = args[1];
            int sourcePageIndex = int.Parse(args[2]);
            int insertIndex = int.Parse(args[3]);
            string outputPath = args[4];

            try
            {
                // Load source and target diagrams
                Diagram sourceDiagram = new Diagram(sourcePath);
                Diagram targetDiagram = new Diagram(targetPath);

                // -------------------------------------------------
                // 1. Ensure all masters used by the source page exist in the target diagram
                // -------------------------------------------------
                foreach (Master srcMaster in sourceDiagram.Masters)
                {
                    bool masterExists = false;
                    foreach (Master tgtMaster in targetDiagram.Masters)
                    {
                        if (tgtMaster.Name == srcMaster.Name)
                        {
                            masterExists = true;
                            break;
                        }
                    }

                    if (!masterExists)
                    {
                        // Add the master from the source diagram to the target diagram
                        targetDiagram.AddMaster(sourceDiagram, srcMaster.Name);
                    }
                }

                // -------------------------------------------------
                // 2. Retrieve the page to copy from the source diagram
                // -------------------------------------------------
                if (sourcePageIndex < 0 || sourcePageIndex >= sourceDiagram.Pages.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(sourcePageIndex), "Source page index is out of range.");
                }

                Page srcPage = sourceDiagram.Pages[sourcePageIndex];

                // -------------------------------------------------
                // 3. Determine a new unique page ID for the copied page
                // -------------------------------------------------
                int maxPageId = 0;
                foreach (Page p in targetDiagram.Pages)
                {
                    if (p.ID > maxPageId)
                        maxPageId = p.ID;
                }
                int newPageId = maxPageId + 1;

                // -------------------------------------------------
                // 4. Create a new page instance and copy the content
                // -------------------------------------------------
                Page newPage = new Page(newPageId);
                srcPage.Copy(newPage); // Copies shapes, page sheet, etc.

                // -------------------------------------------------
                // 5. Add the new page to the target diagram and position it
                // -------------------------------------------------
                targetDiagram.Pages.Add(newPage); // Adds at the end
                // Move the page to the desired index (if different from the end)
                if (insertIndex >= 0 && insertIndex < targetDiagram.Pages.Count)
                {
                    newPage.MoveTo(insertIndex);
                }

                // -------------------------------------------------
                // 6. Save the modified target diagram
                // -------------------------------------------------
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Page copied successfully. Output saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }