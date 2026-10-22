using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Default input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.jpg";

        // Override defaults with command‑line arguments if provided
        if (args.Length >= 1 && !string.IsNullOrWhiteSpace(args[0]))
            inputPath = args[0];
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
            outputPath = args[1];

        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Configure image export options for JPEG format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
            // Set JPEG quality (0‑100). Adjust as needed.
            saveOptions.JpegQuality = 90;
            // ExportHiddenPage is a bool property, so use a plain boolean literal
            saveOptions.ExportHiddenPage = false;

            // Export the diagram to a JPEG image using the configured options
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram successfully exported to JPEG: \"{outputPath}\"");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}