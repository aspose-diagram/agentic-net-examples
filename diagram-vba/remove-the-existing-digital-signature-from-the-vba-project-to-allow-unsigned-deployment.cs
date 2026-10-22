using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath;
        if (args.Length >= 2)
        {
            outputPath = args[1];
        }
        else
        {
            // Append suffix "_unsigned" before extension
            string dir = Path.GetDirectoryName(inputPath);
            string name = Path.GetFileNameWithoutExtension(inputPath);
            string ext = ".vsdx"; // save as non‑macro format
            outputPath = Path.Combine(dir, $"{name}_unsigned{ext}");
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Check if the VBA project is signed
            bool isSigned = diagram.VbaProject.IsSigned;
            Console.WriteLine($"VBA project signed: {isSigned}");

            if (isSigned)
            {
                // Remove all VBA/macros data (including the signature)
                diagram.VbProjectData = null;
                Console.WriteLine("Signature removed by clearing VBA project data.");
            }
            else
            {
                Console.WriteLine("No signature present; no changes made.");
            }

            // Save the diagram in a non‑macro format (VSDX)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}