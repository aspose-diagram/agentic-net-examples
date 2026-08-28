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

            // Paths to the source and destination Visio files.
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram.
            Diagram diagram = new Diagram(inputPath);

            // Determine the current maximum page ID.
            int maxPageId = 0;
            foreach (Page page in diagram.Pages)
            {
                if (page.ID > maxPageId)
                    maxPageId = page.ID;
            }

            // Insert three blank pages at the beginning of the diagram.
            for (int i = 0; i < 3; i++)
            {
                // Create a new page with a unique ID.
                maxPageId++;
                Page newPage = new Page(maxPageId);

                // Add the page to the diagram.
                diagram.Pages.Add(newPage);

                // Move the newly added page to the first position (index 0).
                newPage.MoveTo(0);
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
