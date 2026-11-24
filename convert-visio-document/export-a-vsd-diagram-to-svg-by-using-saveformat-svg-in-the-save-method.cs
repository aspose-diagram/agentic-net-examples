using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input‑vsd‑file>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output SVG path (same folder, same name with .svg extension).
        string outputPath = Path.ChangeExtension(inputPath, ".svg");

        try
        {
            // Load the Visio diagram inside a using block (ensures proper disposal).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Export the diagram to SVG using the SaveFileFormat.Svg enum.
                diagram.Save(outputPath, SaveFileFormat.Svg);
            }

            Console.WriteLine($"Diagram successfully exported to SVG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}