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

            // Load the multi‑page Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Configure HTML save options to generate separate HTML files per page
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Ensure each page is saved as an individual file (default is false)
                SaveAsSingleFile = false,

                // Render all pages of the diagram
                PageCount = diagram.Pages.Count,

                // Start rendering from the first page (optional, default is 0)
                PageIndex = 0
            };

            // Save the diagram to HTML; separate files will be created for each page
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
