using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Verify that the diagram has at least three pages
            if (diagram.Pages.Count < 3)
            {
                throw new Exception("The diagram does not contain a third page to clone.");
            }

            // Retrieve the third page (zero‑based index)
            Page sourcePage = diagram.Pages[2];

            // Determine the highest existing page ID to assign a unique ID to the new page
            int maxId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxId)
                    maxId = p.ID;
            }

            // Create a new page with a new ID
            Page clonedPage = new Page(maxId + 1);

            // Copy the contents of the third page into the new page
            clonedPage.Copy(sourcePage);

            // Optionally give the cloned page a distinct name
            clonedPage.Name = sourcePage.Name + "_Clone";

            // Modify the height of the cloned page (example: set to 11 inches)
            clonedPage.PageSheet.PageProps.PageHeight.Value = 11.0;

            // Preserve the original width (or set a different width if desired)
            clonedPage.PageSheet.PageProps.PageWidth.Value = sourcePage.PageSheet.PageProps.PageWidth.Value;

            // Insert the cloned page into the diagram's page collection
            diagram.Pages.Add(clonedPage);

            // Save the updated diagram back to a file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
