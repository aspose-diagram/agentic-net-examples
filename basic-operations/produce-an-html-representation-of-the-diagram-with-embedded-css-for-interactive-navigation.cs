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
            using (var diagram = new Diagram("input.vsdx"))
            {
                // Configure HTML save options
                var htmlOptions = new HTMLSaveOptions
                {
                    // Embed all resources (CSS, JS, images) into a single HTML file
                    SaveAsSingleFile = true,

                    // Include the default navigation toolbar for page switching
                    SaveToolBar = true,

                    // Set a custom title for the generated HTML page
                    Title = "Interactive Diagram"
                };

                // Save the diagram as an HTML file with the specified options
                diagram.Save("output.html", htmlOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
