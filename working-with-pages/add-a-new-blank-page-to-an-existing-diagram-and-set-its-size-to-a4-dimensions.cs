using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine the maximum existing page ID
            int maxPageId = 0;
            foreach (Page page in diagram.Pages)
            {
                if (page.ID > maxPageId)
                    maxPageId = page.ID;
            }

            // Create a new blank page
            Page newPage = new Page();
            newPage.ID = maxPageId + 1;               // Assign a unique ID
            newPage.Name = "NewPage";                 // Optional: set a name

            // Set A4 size (width = 8.27 inches, height = 11.69 inches)
            newPage.PageSheet.PageProps.PageWidth.Value = 8.27;
            newPage.PageSheet.PageProps.PageHeight.Value = 11.69;

            // Add the new page to the diagram
            diagram.Pages.Add(newPage);

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
