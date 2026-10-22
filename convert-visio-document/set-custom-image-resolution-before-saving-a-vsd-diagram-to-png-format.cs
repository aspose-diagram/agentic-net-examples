using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.png";

        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Configure image save options with a custom resolution (e.g., 300 DPI)
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.Resolution = 300f;               // Set resolution in DPI
            saveOptions.ExportHiddenPage = false;        // Do not export hidden pages (bool, not BOOL)

            // Save the diagram as a PNG image using the configured options
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram successfully saved to '{outputPath}' with resolution {saveOptions.Resolution} DPI.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
