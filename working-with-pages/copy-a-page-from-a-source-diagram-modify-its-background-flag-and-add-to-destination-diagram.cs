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

            // Paths to the source diagram, destination (or new) diagram, and the output file.
            string sourcePath = "source.vsdx";
            string destinationPath = "destination.vsdx"; // can be an existing file or will be created anew
            string outputPath = "merged_output.vsdx";

            // Load the source diagram.
            Diagram srcDiagram = new Diagram(sourcePath);

            // Load the destination diagram if it exists; otherwise create a new empty diagram.
            Diagram destDiagram;
            if (System.IO.File.Exists(destinationPath))
            {
                destDiagram = new Diagram(destinationPath);
            }
            else
            {
                destDiagram = new Diagram(); // empty diagram
            }

            // -------------------------------------------------
            // 1. Copy masters from source to destination diagram.
            // -------------------------------------------------
            foreach (Master srcMaster in srcDiagram.Masters)
            {
                // Add master by its universal name to avoid duplicates.
                // The AddMaster method will ignore if the master already exists.
                destDiagram.AddMaster(srcDiagram, srcMaster.NameU);
            }

            // -------------------------------------------------
            // 2. Determine the next available page ID in the destination diagram.
            // -------------------------------------------------
            int maxPageId = 0;
            foreach (Page pg in destDiagram.Pages)
            {
                if (pg.ID > maxPageId)
                    maxPageId = pg.ID;
            }

            // -------------------------------------------------
            // 3. Select the page to copy from the source diagram.
            //    Here we copy the first page (index 0).
            // -------------------------------------------------
            Page srcPage = srcDiagram.Pages[0];

            // -------------------------------------------------
            // 4. Create a new page instance and copy the source page content.
            // -------------------------------------------------
            Page newPage = new Page();
            newPage.Name = srcPage.Name;               // preserve the original name
            newPage.ID = maxPageId + 1;                 // assign a unique ID
            newPage.Copy(srcPage);                     // deep copy of shapes, page sheet, etc.

            // -------------------------------------------------
            // 5. Modify the background flag of the newly added page.
            //    Setting it to TRUE makes the page a background page.
            // -------------------------------------------------
            newPage.Background = BOOL.True;

            // -------------------------------------------------
            // 6. Add the new page to the destination diagram.
            // -------------------------------------------------
            destDiagram.Pages.Add(newPage);

            // -------------------------------------------------
            // 7. Remove the default empty page that may exist in a newly created diagram.
            // -------------------------------------------------
            if (destDiagram.Pages.Count > 1)
            {
                // The first page (index 0) is typically the empty starter page.
                destDiagram.Pages.Remove(destDiagram.Pages[0]);
            }

            // -------------------------------------------------
            // 8. Save the merged diagram to the desired output file.
            // -------------------------------------------------
            destDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Page copied, background flag set, and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
