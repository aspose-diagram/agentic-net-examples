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

            // Load an existing Visio diagram
            // (Assumes the file "input.vsdx" exists in the working directory)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure HTML save options to embed all resources (including SVG) into a single file
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                SaveAsSingleFile = true   // Embed images/SVG inline rather than creating separate files
            };

            // Save the diagram as an HTML file with inline SVG
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
