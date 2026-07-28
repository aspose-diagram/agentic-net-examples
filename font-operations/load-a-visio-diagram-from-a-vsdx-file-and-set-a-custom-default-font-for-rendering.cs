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

            // Path to the source VSDX file
            string inputPath = "input.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Set a custom default font for rendering
            // This font will be used when the original font is missing or unavailable
            FontConfigs.DefaultFontName = "Calibri";

            // (Optional) If you need to add additional font folders:
            // FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);

            // Save the diagram with the default font applied (optional step)
            string outputPath = "output.vsdx";
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);
            saveOptions.DefaultFont = "Calibri";
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
