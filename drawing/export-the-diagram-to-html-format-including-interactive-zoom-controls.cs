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
            Diagram diagram = new Diagram("input.vsdx");

            // Configure HTML export options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Enable the toolbar which provides interactive zoom controls
                SaveToolBar = true,

                // Export each page as a separate HTML file (set to true for a single file)
                SaveAsSingleFile = false,

                // Optional: set resolution, page size, etc., if needed
                // Resolution = 96,
                // PageSize = new Size(800, 600)
            };

            // Export the diagram to HTML using the configured options
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
