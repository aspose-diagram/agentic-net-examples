using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to source and destination files
            string sourcePath = "source.vsdx";
            string destinationPath = "destination.vsdx";

            // Load the source diagram
            Diagram sourceDiagram = new Diagram(sourcePath);

            // Create a new (empty) destination diagram
            Diagram destinationDiagram = new Diagram();

            // -------------------------------------------------
            // 1. Copy masters from source to destination
            // -------------------------------------------------
            foreach (Master srcMaster in sourceDiagram.Masters)
            {
                // Add each master by name from the source diagram
                destinationDiagram.AddMaster(sourceDiagram, srcMaster.Name);
            }

            // -------------------------------------------------
            // 2. Determine the next available page ID in the destination diagram
            // -------------------------------------------------
            int maxPageId = 0;
            foreach (Page pg in destinationDiagram.Pages)
            {
                if (pg.ID > maxPageId)
                    maxPageId = pg.ID;
            }

            // -------------------------------------------------
            // 3. Copy the first page from the source diagram
            // -------------------------------------------------
            Page sourcePage = sourceDiagram.Pages[0];               // source page to copy
            Page copiedPage = new Page(maxPageId + 1);              // new page with a unique ID
            copiedPage.Copy(sourcePage);                           // copy all content

            // -------------------------------------------------
            // 4. Modify the background flag of the copied page
            // -------------------------------------------------
            copiedPage.Background = BOOL.True; // set as a background page (use BOOL.False for normal)

            // -------------------------------------------------
            // 5. Add the modified page to the destination diagram
            // -------------------------------------------------
            destinationDiagram.Pages.Add(copiedPage);

            // -------------------------------------------------
            // 6. Save the destination diagram
            // -------------------------------------------------
            destinationDiagram.Save(destinationPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
