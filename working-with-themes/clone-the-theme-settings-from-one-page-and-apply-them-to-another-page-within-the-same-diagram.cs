using System.IO;
using System;
using Aspose.Diagram;

class ThemeCloneExample
{
    static void Main()
    {
        try
        {

            // Load the existing diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Identify source and target pages (by index or name)
            // Here we assume page 0 is the source and page 1 is the target
            Page sourcePage = diagram.Pages[0];
            Page targetPage = diagram.Pages[1];

            // Clone theme settings from source page to target page
            // The Page.Copy method copies the entire page, including its theme.
            // If you only need the theme, this still transfers the theme settings.
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
