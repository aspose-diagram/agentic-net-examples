using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output CSV file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.csv";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create save options for CSV export.
            // Note: Aspose.Diagram does not expose an Encoding property for CSV.
            // The library writes CSV using UTF‑8 by default, so no explicit setting is required.
            DiagramSaveOptions saveOptions = new DiagramSaveOptions();

            // Save the diagram as CSV using the configured options
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram successfully saved to CSV at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}