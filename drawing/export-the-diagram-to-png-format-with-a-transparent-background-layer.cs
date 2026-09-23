using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Verify the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Path for the exported PNG file
        string outputPath = "output.png";

        try
        {
            // Load the Visio diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Configure PNG export options (transparent background is default for PNG)
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Save the diagram as a PNG image using the configured options
            diagram.Save(outputPath, pngOptions);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}