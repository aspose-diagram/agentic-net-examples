using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Ensure the required arguments are provided: input VSDX path and output JPEG path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.jpg>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram using a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Set up image save options for JPEG format with 80% compression quality.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                saveOptions.JpegQuality = 80; // Quality range is 0-100.

                // Save the diagram to the specified JPEG file.
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram successfully converted to JPEG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}