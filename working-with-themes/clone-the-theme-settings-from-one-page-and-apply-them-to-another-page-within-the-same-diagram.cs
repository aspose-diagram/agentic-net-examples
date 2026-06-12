using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load the diagram
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Identify source and target pages (by index or name)
            int sourcePageIndex = 0;   // first page – adjust as needed
            int targetPageIndex = 1;   // page to receive the theme – adjust as needed

            Aspose.Diagram.Page sourcePage = diagram.Pages[sourcePageIndex];
            Aspose.Diagram.Page targetPage = diagram.Pages[targetPageIndex];

            // Copy the theme (pagesheet) from the source page to the target page
            targetPage.PageSheet.Copy(sourcePage.PageSheet);

            // Save the modified diagram
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
