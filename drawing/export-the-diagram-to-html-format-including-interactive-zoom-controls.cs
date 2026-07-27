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

            // Configure HTML save options to include the toolbar (zoom controls)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.SaveToolBar = true;          // enable interactive toolbar with zoom
            htmlOptions.SaveAsSingleFile = false;    // generate separate HTML and image files

            // Export the diagram to HTML using the configured options
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
