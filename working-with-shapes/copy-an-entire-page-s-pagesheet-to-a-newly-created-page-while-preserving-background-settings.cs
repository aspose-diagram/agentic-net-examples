using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Source page whose PageSheet will be copied (e.g., the first page)
            Page sourcePage = diagram.Pages[0];

            // Create a new page instance
            Page newPage = new Page(diagram.Pages.Count);
            // Add the new page to the diagram's page collection
            diagram.Pages.Add(newPage);

            // Copy the entire PageSheet from the source page to the new page
            newPage.PageSheet.Copy(sourcePage.PageSheet);

            // Preserve background settings of the source page
            newPage.Background = sourcePage.Background;
            newPage.BackPage = sourcePage.BackPage;

            // Optionally give the new page a distinct name
            newPage.Name = "CopiedPage";

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
