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

            // Configure font directories for Aspose.Diagram.
            // The SetFontFolder method requires the folder path and a recursive flag.
            string fontFolder = @"C:\Windows\Fonts";
            FontConfigs.SetFontFolder(fontFolder, true);

            // Set a default fallback font to be used when a required font is missing.
            FontConfigs.DefaultFontName = "Arial";

            // NOTE:
            // Aspose.Diagram does not provide a public API to adjust the internal font cache size.
            // Therefore, the cache size cannot be set directly via the library.
            // If needed, cache behavior might be influenced through environment-specific settings
            // outside the scope of the Aspose.Diagram API.

            // Load a diagram (replace with your actual file path).
            Diagram diagram = new Diagram("input.vsdx");

            // Perform any diagram processing here.
            // ...

            // Save the diagram.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
