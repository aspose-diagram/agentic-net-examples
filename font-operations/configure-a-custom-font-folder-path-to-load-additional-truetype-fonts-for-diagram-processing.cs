using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the folder that contains additional TrueType fonts
            string customFontFolder = @"C:\MyCustomFonts";

            // Set to true if you want to include subfolders in the scan
            bool recursive = true;

            // Apply the font folder globally for all Diagram objects
            FontConfigs.SetFontFolder(customFontFolder, recursive);

            // Example: load a diagram after configuring the font folder
            Diagram diagram = new Diagram("input.vsdx");

            // (Optional) Set the font folder for this specific diagram instance
            diagram.FontDirs = new string[] { customFontFolder };

            // Continue with diagram processing...

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
