using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source diagram, the target diagram and the output file
            string sourcePath = "source.vsdx";
            string targetPath = "target.vsdx";
            string outputPath = "merged.vsdx";

            // Load the source and target diagrams
            Diagram sourceDiagram = new Diagram(sourcePath);
            Diagram targetDiagram = new Diagram(targetPath);

            // ------------------------------------------------------------
            // 1. Copy masters from source to target (required for page copy)
            // ------------------------------------------------------------
            foreach (Master srcMaster in sourceDiagram.Masters)
            {
                // Add each master to the target diagram
                // Duplicate masters are ignored by the library, so this is safe
                targetDiagram.Masters.Add(srcMaster);
            }

            // ------------------------------------------------------------
            // 2. Get the page to copy from the source diagram
            // ------------------------------------------------------------
            // Here we copy the first page; adjust the index or use GetPage(name) as needed
            Page sourcePage = sourceDiagram.Pages[0];

            // ------------------------------------------------------------
            // 3. Determine the next available page ID in the target diagram
            // ------------------------------------------------------------
            int maxPageId = 0;
            foreach (Page p in targetDiagram.Pages)
            {
                if (p.ID > maxPageId)
                    maxPageId = p.ID;
            }

            // ------------------------------------------------------------
            // 4. Prepare a copy of the source page for insertion
            // ------------------------------------------------------------
            // Create a new page instance and copy the source page's content
            Page newPage = new Page(maxPageId + 1);
            newPage.Copy(sourcePage);

            // Ensure the page name is unique (optional)
            newPage.Name = sourcePage.Name + "_Copy";

            // ------------------------------------------------------------
            // 5. Add the copied page to the end of the target diagram
            // ------------------------------------------------------------
            targetDiagram.Pages.Add(newPage);

            // ------------------------------------------------------------
            // 6. Save the merged diagram
            // ------------------------------------------------------------
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Page copied successfully. Output saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
