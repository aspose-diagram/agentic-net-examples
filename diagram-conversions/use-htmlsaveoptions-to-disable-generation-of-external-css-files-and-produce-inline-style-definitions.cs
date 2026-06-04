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

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Create HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Set to save as a single file – this embeds CSS (and other resources) directly
            // into the generated HTML, avoiding external CSS files.
            htmlOptions.SaveAsSingleFile = true;

            // Optional: disable the toolbar if not needed
            htmlOptions.SaveToolBar = false;

            // Save the diagram as HTML with inline style definitions
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
