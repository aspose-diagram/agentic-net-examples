using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-visio-file>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Determine output path (same folder, suffix "_unsigned.vsdm").
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_unsigned.vsdm");

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Check if the VBA project is signed.
            bool isSigned = diagram.VbaProject.IsSigned;
            Console.WriteLine($"VBA project signed: {isSigned}");

            if (isSigned)
            {
                // Remove all VBA data (including the signature) by clearing the project data.
                // This effectively unsigns the diagram.
                diagram.VbProjectData = null;
                Console.WriteLine("Existing VBA signature removed.");
            }
            else
            {
                Console.WriteLine("VBA project is already unsigned.");
            }

            // Save the diagram in a macro-enabled format (VSDM) to preserve any remaining VBA (if any).
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Diagram saved without signature to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}