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
            Console.Error.WriteLine("Usage: Program <input.mmd> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the MMD source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Mermaid (MMD) file into a Diagram object.
            Diagram diagram = new Diagram(inputPath);

            // Save the diagram as a Visio VSDX file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Successfully converted \"{inputPath}\" to Visio format at \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}