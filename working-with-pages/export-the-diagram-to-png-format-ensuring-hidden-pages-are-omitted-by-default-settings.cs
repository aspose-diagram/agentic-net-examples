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

            // Create image save options for PNG format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Omit hidden pages during export (default is true)
            saveOptions.ExportHiddenPage = false;

            // Save the diagram as a PNG image using the specified options
            diagram.Save("output.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
