using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output file path.
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: RemoveVbaSignature <inputFile> <outputFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram.
        Diagram diagram = new Diagram(inputPath);

        // Check if the VBA project is signed.
        bool isSigned = diagram.VbaProject.IsSigned;
        Console.WriteLine($"VBA project signed: {isSigned}");

        if (isSigned)
        {
            // Remove the entire VBA project data (including the signature).
            diagram.VbProjectData = null;
            Console.WriteLine("VBA signature removed by clearing VBA project data.");
        }
        else
        {
            Console.WriteLine("No signature found; no changes made to VBA project.");
        }

        // Save the diagram. Since the VBA project is removed, a non‑macro format is appropriate.
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
        Console.WriteLine($"Diagram saved to '{outputPath}'.");
    }
}
