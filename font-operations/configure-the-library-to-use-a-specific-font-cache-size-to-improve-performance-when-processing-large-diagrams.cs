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

            // NOTE: Aspose.Diagram does not expose a FontCacheSize property.
            // The library manages font caching internally. We can configure
            // default font handling and font folder locations to improve
            // performance when processing large diagrams.

            // Set a fallback font to be used when a required font is missing.
            FontConfigs.DefaultFontName = "Arial";

            // Add a font folder (example path). The second argument enables
            // recursive scanning of subfolders.
            FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);

            // Load an existing Visio diagram.
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // ... perform diagram processing here ...

            // Save the diagram. Use the two‑argument overload with a valid
            // SaveFileFormat enum value.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
