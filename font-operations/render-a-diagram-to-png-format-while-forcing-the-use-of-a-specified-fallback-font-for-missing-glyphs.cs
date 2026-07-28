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

            // Create save options for PNG rendering
            RenderingSaveOptions pngOptions = (RenderingSaveOptions)SaveOptions.CreateSaveOptions(SaveFileFormat.Png);
            // Specify a fallback font to use when the original font is missing
            pngOptions.DefaultFont = "MS Gothic";

            // Render and save the diagram as PNG using the configured options
            diagram.Save("output.png", pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
