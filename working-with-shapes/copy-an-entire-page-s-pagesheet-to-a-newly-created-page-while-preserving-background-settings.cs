using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Identify the source page (e.g., by name or index)
            // Here we use the first page as the source
            Page sourcePage = diagram.Pages[0];

            // Add a new page to the diagram
            Page newPage = new Page();
            diagram.Pages.Add(newPage);

            // Preserve background settings
            newPage.Background = sourcePage.Background;
            newPage.BackPage = sourcePage.BackPage;

            // Copy the entire PageSheet from the source page to the new page
            newPage.PageSheet.Copy(sourcePage.PageSheet);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
