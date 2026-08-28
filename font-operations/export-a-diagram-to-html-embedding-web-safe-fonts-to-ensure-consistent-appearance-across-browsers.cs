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

            // Set up HTML export options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.DefaultFont = "Arial";          // web‑safe font to ensure consistent rendering
            htmlOptions.SaveAsSingleFile = true;        // embed all resources (images, CSS) in one HTML file
            htmlOptions.Title = "Exported Diagram";     // optional title for the HTML page

            // Export the diagram to HTML using the configured options
            diagram.Save("output.html", htmlOptions);

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
