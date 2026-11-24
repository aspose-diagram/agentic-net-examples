using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output SVG file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Export the entire diagram to SVG format
                diagram.Save(outputPath, SaveFileFormat.Svg);
                Console.WriteLine($"Diagram successfully exported to SVG: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error during SVG export: {ex.Message}");
        }
    }
}