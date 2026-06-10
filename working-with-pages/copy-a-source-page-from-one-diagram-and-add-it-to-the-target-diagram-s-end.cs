using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source diagram containing the page to copy
            Diagram srcDiagram = new Diagram("source.vdx");

            // Load the target diagram where the page will be appended
            Diagram targetDiagram = new Diagram("target.vdx");

            // Select the page to copy from the source diagram (e.g., the first page)
            Page sourcePage = srcDiagram.Pages[0];

            // Add a new blank page at the end of the target diagram's pages collection
            Page newPage = new Page();
            targetDiagram.Pages.Add(newPage);

            // Copy the entire content of the source page into the newly added page
            newPage.Copy(sourcePage);

            // Save the updated target diagram
            targetDiagram.Save("target_modified.vdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
