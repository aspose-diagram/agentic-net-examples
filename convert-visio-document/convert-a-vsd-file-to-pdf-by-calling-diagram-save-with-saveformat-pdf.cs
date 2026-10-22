using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input VSD file path and output PDF file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsd> <output.pdf>");
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
            // Load the Visio diagram from the specified VSD file.
            Diagram diagram = new Diagram(inputPath);

            // Save the diagram as PDF using the SaveFileFormat.Pdf enum value.
            diagram.Save(outputPath, SaveFileFormat.Pdf);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving to the error stream.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}