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

            // Create HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Set to save as a single HTML file.
            // This embeds CSS and other resources directly into the HTML,
            // preventing generation of external CSS files.
            htmlOptions.SaveAsSingleFile = true;

            // Save the diagram as HTML with the configured options
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
