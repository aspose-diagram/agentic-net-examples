using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Set up HTML save options to export pages 2 through 4
                HTMLSaveOptions options = new HTMLSaveOptions();
                options.PageIndex = 1; // Zero‑based index of the first page to export (page 2)
                options.PageCount = 3; // Number of pages to export (pages 2, 3, and 4)

                // Save the selected pages as an HTML file
                diagram.Save("output.html", options);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
