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

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Create HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Generate a single HTML file with all resources (images, CSS, etc.) embedded as Base64
            htmlOptions.SaveAsSingleFile = true;

            // Optional: set resolution (default is 96 DPI)
            htmlOptions.Resolution = 96;

            // Save the diagram as a single HTML file
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
