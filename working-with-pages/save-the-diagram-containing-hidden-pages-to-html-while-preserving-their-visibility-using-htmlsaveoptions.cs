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

            // Load the diagram that contains hidden pages
            Diagram diagram = new Diagram(@"C:\Input\sample.vsdx");

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Preserve hidden pages in the output (default is true, set explicitly for clarity)
                ExportHiddenPage = true,

                // Render all pages (including hidden ones)
                PageCount = int.MaxValue
            };

            // Save the diagram to HTML while keeping hidden pages visible
            diagram.Save(@"C:\Output\sample.html", htmlOptions);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
