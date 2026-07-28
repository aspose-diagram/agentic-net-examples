using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Choose the source page whose PageSheet will be copied (e.g., the first page)
            Page sourcePage = diagram.Pages[0];

            // Add a new page to the diagram
            Page newPage = new Page();
            diagram.Pages.Add(newPage);

            // Copy the entire PageSheet from the source page to the new page
            newPage.PageSheet.Copy(sourcePage.PageSheet);

            // Preserve the background flag of the source page
            newPage.Background = sourcePage.Background;

            // Preserve the reference to a background page, if the source page has one
            if (sourcePage.BackPage != null)
            {
                newPage.BackPage = sourcePage.BackPage;
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
