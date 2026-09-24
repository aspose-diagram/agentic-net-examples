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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path for the exported PNG image
            string outputPath = "output.png";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PNG export options with a custom DPI of 600
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            pngOptions.Resolution = 600f; // DPI setting

            // Export the diagram (all pages) to PNG using the specified options
            diagram.Save(outputPath, pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
