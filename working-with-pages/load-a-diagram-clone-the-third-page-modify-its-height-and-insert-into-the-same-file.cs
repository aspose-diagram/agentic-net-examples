using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the existing Visio file
            string inputPath = "input.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Ensure there are at least three pages
            if (diagram.Pages.Count < 3)
            {
                throw new Exception("The diagram does not contain a third page to clone.");
            }

            // Retrieve the third page (zero‑based index 2)
            Page sourcePage = diagram.Pages[2];

            // Create a new blank page instance
            Page clonedPage = new Page();

            // Copy all contents from the source page to the new page
            clonedPage.Copy(sourcePage);

            // Modify the height of the cloned page (e.g., increase by 2 inches)
            double originalHeight = sourcePage.PageSheet.PageProps.PageHeight.Value;
            clonedPage.PageSheet.PageProps.PageHeight.Value = originalHeight + 2.0;

            // Optionally, keep the same width as the source page
            clonedPage.PageSheet.PageProps.PageWidth.Value = sourcePage.PageSheet.PageProps.PageWidth.Value;

            // Assign a unique ID to the new page
            int maxId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxId)
                    maxId = p.ID;
            }
            clonedPage.ID = maxId + 1;

            // Give the cloned page a distinct name
            clonedPage.Name = "ClonedPage3";

            // Insert the cloned page into the diagram
            diagram.Pages.Add(clonedPage);

            // Save the updated diagram (overwrites the original file)
            diagram.Save(inputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
