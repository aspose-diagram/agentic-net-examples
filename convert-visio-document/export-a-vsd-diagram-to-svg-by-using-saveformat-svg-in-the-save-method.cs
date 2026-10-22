using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths.
        string inputPath;
        string outputPath;

        if (args.Length >= 2)
        {
            inputPath = args[0];
            outputPath = args[1];
        }
        else
        {
            // Default file names (modify as needed).
            inputPath = "input.vsdx";
            outputPath = "output.svg";
        }

        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Use a using block to guarantee disposal of resources.
            using (diagram)
            {
                // Export the diagram to SVG format using the appropriate SaveFileFormat.
                diagram.Save(outputPath, SaveFileFormat.Svg);
            }

            Console.WriteLine($"Diagram successfully exported to SVG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving to the error stream.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}