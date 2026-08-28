using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load the Visio diagram (multi‑page)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure HTML save options to produce separate files per page
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Ensure each page is saved as an individual HTML file
                SaveAsSingleFile = false,
                // Render all pages of the diagram
                PageCount = diagram.Pages.Count,
                // Start rendering from the first page (0‑based index)
                PageIndex = 0
            };

            // Save the diagram to HTML; multiple files will be generated (one per page)
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
