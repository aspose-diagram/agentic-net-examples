using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <FirstVisioFile> <SecondVisioFile> <OutputVisioFile>");
            return;
        }

        string firstPath = args[0];
        string secondPath = args[1];
        string outputPath = args[2];

        // Guard against missing input files.
        if (!File.Exists(firstPath))
        {
            Console.Error.WriteLine($"Error: File not found – {firstPath}");
            return;
        }
        if (!File.Exists(secondPath))
        {
            Console.Error.WriteLine($"Error: File not found – {secondPath}");
            return;
        }

        try
        {
            // Load the first Visio diagram (target diagram).
            Diagram targetDiagram = new Diagram(firstPath);

            // Load the second Visio diagram (source diagram to be merged).
            Diagram sourceDiagram = new Diagram(secondPath);

            // Merge the source diagram into the target diagram.
            // This combines pages, masters, layers, and connections.
            targetDiagram.Combine(sourceDiagram);

            // Save the merged diagram to the specified output path in VSDX format.
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Successfully merged diagrams into '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any exceptions to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}