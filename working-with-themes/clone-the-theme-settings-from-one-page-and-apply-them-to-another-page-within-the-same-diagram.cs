using System.IO;
using System;
using Aspose.Diagram;

class ThemeCloneExample
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Identify source and target pages by their indexes (0‑based)
            int sourcePageIndex = 0;   // page from which to copy the theme
            int targetPageIndex = 1;   // page to which the theme will be applied

            // Get the Page objects
            Page sourcePage = diagram.Pages[sourcePageIndex];
            Page targetPage = diagram.Pages[targetPageIndex];

            // Clone the theme settings from the source page to the target page
            // The Page.Copy method copies the pagesheet, which includes theme information.
            targetPage.Copy(sourcePage);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
