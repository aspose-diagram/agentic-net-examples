using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the folder that contains additional TrueType fonts
            string customFontFolder = @"C:\MyFonts";

            // Set the folder for the whole Aspose.Diagram library (recursive scan of subfolders)
            FontConfigs.SetFontFolder(customFontFolder, true);

            // Load a diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Optionally, set the font directories for this specific diagram instance
            diagram.FontDirs = new[] { customFontFolder };

            // Perform diagram processing here...

            // Save the diagram (replace with your desired output path and format)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
