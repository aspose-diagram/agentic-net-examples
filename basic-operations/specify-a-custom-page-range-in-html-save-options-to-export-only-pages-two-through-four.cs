using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source diagram file
            Diagram diagram = new Diagram("input.vsdx");

            // Set up HTML save options to render pages 2 through 4
            HTMLSaveOptions options = new HTMLSaveOptions();
            options.PageIndex = 1;   // Zero‑based index of the first page to render (page 2)
            options.PageCount = 3;   // Number of pages to render (pages 2, 3, and 4)

            // Save the diagram as HTML using the configured options
            diagram.Save("output.html", options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
