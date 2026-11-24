using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a using block to guarantee disposal.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Perform the conversion and save as PDF.
                diagram.Save(outputPath, SaveFileFormat.Pdf);
                Console.WriteLine($"Diagram successfully converted and saved to: {outputPath}");
            } // Diagram is disposed here.
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}