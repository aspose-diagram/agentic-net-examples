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

            // Create save options appropriate for PNG format
            SaveOptions options = SaveOptions.CreateSaveOptions(SaveFileFormat.Png);
            // Specify a fallback font to use for characters that are missing in the original font
            options.DefaultFont = "MS Gothic";

            // Render and save the diagram as a PNG image using the defined options
            diagram.Save("output.png", options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
