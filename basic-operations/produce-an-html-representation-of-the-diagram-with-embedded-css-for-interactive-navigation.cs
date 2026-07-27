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

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Embed all resources (images, CSS) into a single HTML file for easy navigation
                SaveAsSingleFile = true,

                // Include a title for the HTML page
                Title = "Interactive Diagram",

                // Disable the default toolbar if you want a cleaner UI
                SaveToolBar = false,

                // Export all pages (default) and enable navigation between them
                PageCount = int.MaxValue,
                PageIndex = 0,

                // Set resolution for generated images (optional)
                Resolution = 96
            };

            // Save the diagram as an HTML file with the specified options
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
