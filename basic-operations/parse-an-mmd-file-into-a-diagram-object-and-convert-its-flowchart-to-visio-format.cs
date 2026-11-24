using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input MMD file path and the output Visio file path as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.mmd> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the MMD file into a Diagram object inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Save the loaded diagram as a Visio VSDX file.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram successfully saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}