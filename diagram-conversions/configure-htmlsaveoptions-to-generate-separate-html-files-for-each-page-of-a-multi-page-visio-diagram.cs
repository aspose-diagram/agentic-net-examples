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

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure HTML save options to generate separate HTML files per page
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Ensure each page is saved to its own file (default is false, set explicitly for clarity)
                SaveAsSingleFile = false,

                // Start rendering from the first page (0‑based index)
                PageIndex = 0,

                // Render all pages in the diagram
                PageCount = diagram.Pages.Count
            };

            // Save the diagram as HTML; multiple files will be created (e.g., output.html, output_1.html, ...)
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
