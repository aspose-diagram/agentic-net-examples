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

            // Load the source Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure image save options for PNG format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Omit hidden pages during export (default is true)
            saveOptions.ExportHiddenPage = false;

            // Export the diagram to PNG using the configured options
            diagram.Save("output.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
