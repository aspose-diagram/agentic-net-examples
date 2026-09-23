using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Determine the next available page ID
            int maxPageId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxPageId)
                    maxPageId = p.ID;
            }

            // Create a new blank page and assign a unique ID
            Page newPage = new Page(maxPageId + 1);
            // Optionally set a name for the new page
            newPage.Name = "InsertedPage";

            // Add the new page to the diagram (adds at the end)
            diagram.Pages.Add(newPage);

            // Move the newly added page to index 2 (zero‑based index)
            // This will place it as the third page in the collection
            newPage.MoveTo(2);

            // Save the modified diagram (replace with desired output path)
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
