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

            // Create HTML save options and configure them
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.SaveAsSingleFile = true;   // Embed all resources (images, CSS, etc.) as base64
            htmlOptions.Resolution = 96;           // Optional: set DPI for generated images

            // Save the diagram as a single HTML file with embedded resources
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
