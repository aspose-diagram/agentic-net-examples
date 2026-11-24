using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that an input file path was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as a command-line argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Prepare an output path (same folder, with "_out.vsdx" suffix)
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_out.vsdx");

        try
        {
            // Load the diagram inside a using block to guarantee disposal after use
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Save the diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            } // Diagram is automatically disposed here
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}