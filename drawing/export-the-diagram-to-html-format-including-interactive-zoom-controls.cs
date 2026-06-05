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
            Diagram diagram = new Diagram("input.vsd");

            // Set up HTML save options with an interactive toolbar (includes zoom controls)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.SaveToolBar = true;          // enable toolbar with zoom buttons
            htmlOptions.SaveAsSingleFile = false;    // generate separate HTML files per page (optional)

            // Export the diagram to HTML using the configured options
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
