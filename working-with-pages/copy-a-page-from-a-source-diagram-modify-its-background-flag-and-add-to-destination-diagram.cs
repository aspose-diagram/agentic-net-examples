using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source diagram, the existing destination diagram, and the output file
            string sourcePath = "source.vsdx";
            string destinationPath = "dest.vsdx";
            string outputPath = "merged.vsdx";

            // Load the diagrams
            Diagram srcDiagram = new Diagram(sourcePath);
            Diagram destDiagram = new Diagram(destinationPath);

            // Ensure all masters from the source are available in the destination
            foreach (Master srcMaster in srcDiagram.Masters)
            {
                bool exists = false;
                foreach (Master destMaster in destDiagram.Masters)
                {
                    if (destMaster.Name == srcMaster.Name)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    destDiagram.Masters.Add(srcMaster);
                }
            }

            // Select the page to copy (first page in the source diagram)
            Page srcPage = srcDiagram.Pages[0];

            // Determine the next available page ID in the destination diagram
            int maxPageId = 0;
            foreach (Page p in destDiagram.Pages)
            {
                if (p.ID > maxPageId)
                    maxPageId = p.ID;
            }

            // Prepare a copy of the source page
            Page pageCopy = srcPage;
            pageCopy.ID = maxPageId + 1;                     // Assign a new unique ID
            pageCopy.Name = srcPage.Name + "_Copy";          // Optional: give it a distinct name
            pageCopy.Background = BOOL.True;                 // Mark the page as a background page

            // Add the copied page to the destination diagram
            destDiagram.Pages.Add(pageCopy);

            // Save the updated destination diagram
            destDiagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
