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
                // Set up HTML save options
                var htmlOptions = new HTMLSaveOptions
                {
                    // Use a web‑safe font as fallback for any missing fonts
                    DefaultFont = "Arial",
                    // Save the whole diagram as a single HTML file (embeds images, CSS, etc.)
                    SaveAsSingleFile = true,
                    // Optional: omit the toolbar for cleaner output
                    SaveToolBar = false
                };

                // Export the diagram to HTML with the specified options
                diagram.Save("output.html", htmlOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
