using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source diagram containing the page to copy
            Diagram sourceDiagram = new Diagram("source.vsdx");

            // Load the target diagram where the page will be added
            Diagram targetDiagram = new Diagram("target.vsdx");

            // Select the page to copy from the source diagram (e.g., first page)
            int sourcePageIndex = 0;
            Page sourcePage = sourceDiagram.Pages[sourcePageIndex];

            // Create a new empty page and add it to the target diagram's Pages collection
            Page newPage = new Page();
            targetDiagram.Pages.Add(newPage);

            // Copy the contents of the source page into the newly added page
            newPage.Copy(sourcePage);

            // Save the updated target diagram
            targetDiagram.Save("target_modified.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
