using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure the required arguments are provided: input VSD path and output CSV path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVsdPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify that the input file exists on the network share
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Save the diagram as CSV using the appropriate SaveFileFormat enum value
                diagram.Save(outputPath, SaveFileFormat.Csv);
            }

            Console.WriteLine($"Diagram successfully saved as CSV to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during loading or saving
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}