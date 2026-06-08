using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramToHtml
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            var diagram = new Diagram("input.vsdx");

            // Configure HTML save options to embed all shape images as Base64 data URIs
            var htmlOptions = new HTMLSaveOptions
            {
                // When true, the generated HTML is saved as a single file with images embedded.
                SaveAsSingleFile = true
            };

            // Save the diagram as HTML using the configured options
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
