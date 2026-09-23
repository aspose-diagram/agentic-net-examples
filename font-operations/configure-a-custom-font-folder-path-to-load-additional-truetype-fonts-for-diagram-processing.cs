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

            // Path to the folder containing additional TrueType fonts
            string customFontFolder = @"C:\CustomFonts";

            // Register the custom font folder (recursive search)
            FontConfigs.SetFontFolder(customFontFolder, true);

            // Optional: set a fallback font name to use when a required font is missing
            FontConfigs.DefaultFontName = "Arial";

            // Example: load a diagram and save it to verify that the font configuration is applied
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Save the diagram as PDF (any other format can be used similarly)
            diagram.Save("output.pdf", new PdfSaveOptions());

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
