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
            string inputPath = "input.vsd";
            // Path for the exported PNG image
            string outputPath = "output.png";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure image export options
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Set custom resolution (dots per inch)
            saveOptions.Resolution = 300f; // 300 DPI

            // Save the diagram as a PNG image with the specified resolution
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram saved to '{outputPath}' with resolution {saveOptions.Resolution} DPI.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
