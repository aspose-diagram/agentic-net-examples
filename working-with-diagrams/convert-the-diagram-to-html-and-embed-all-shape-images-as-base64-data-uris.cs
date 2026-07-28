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

            // Configure HTML save options to embed all shape images as Base64 data URIs
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // When true, the HTML is saved as a single file with images embedded as Base64 strings
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
