using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and target Visio files
                string sourcePath = "source.vsdx";
                string targetPath = "target.vsdx";

                // Load the source diagram
                Diagram sourceDiagram = new Diagram(sourcePath);

                // Load the target diagram (must exist; otherwise create a new empty diagram)
                Diagram targetDiagram = new Diagram(targetPath);

                // Iterate through each page in the source diagram
                for (int i = 0; i < sourceDiagram.Pages.Count; i++)
                {
                    Page sourcePage = sourceDiagram.Pages[i];
                    Page targetPage;

                    // Ensure the target diagram has a corresponding page
                    if (i < targetDiagram.Pages.Count)
                    {
                        targetPage = targetDiagram.Pages[i];
                    }
                    else
                    {
                        // Create a new page in the target diagram with a unique ID
                        int maxId = 0;
                        foreach (Page p in targetDiagram.Pages)
                        {
                            if (p.ID > maxId) maxId = p.ID;
                        }

                        Page newPage = new Page(maxId + 1);
                        newPage.Name = sourcePage.Name;
                        targetDiagram.Pages.Add(newPage);
                        targetPage = newPage;
                    }

                    // Copy each annotation (comment) from the source page to the target page
                    foreach (Annotation annotation in sourcePage.PageSheet.Annotations)
                    {
                        // Adding the existing annotation object preserves its author (ReviewerID) and dates
                        targetPage.PageSheet.Annotations.Add(annotation);
                    }
                }

                // Save the updated target diagram
                targetDiagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }