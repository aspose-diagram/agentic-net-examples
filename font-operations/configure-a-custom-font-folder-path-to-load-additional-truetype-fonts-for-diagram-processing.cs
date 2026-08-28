using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Configure the custom font folder (search subfolders recursively)
            string customFontFolder = @"C:\CustomFonts";
            FontConfigs.SetFontFolder(customFontFolder, true);

            // Optional: set a fallback font in case a required font is missing
            FontConfigs.DefaultFontName = "Arial";

            // Load a Visio diagram (replace with your actual file path)
            string inputPath = "sample.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Save the diagram to PDF to verify that the custom fonts are applied
            diagram.Save("output.pdf", SaveFileFormat.Pdf);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
