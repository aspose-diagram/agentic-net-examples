using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: output file path and at least one input diagram.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <output.vsdx> <input1.vsdx> [<input2.vsdx> ...]");
            return;
        }

        string outputPath = args[0];

        // Validate that each input file exists.
        for (int i = 1; i < args.Length; i++)
        {
            if (!File.Exists(args[i]))
            {
                Console.Error.WriteLine($"Input file not found: {args[i]}");
                return;
            }
        }

        try
        {
            // Load the first diagram which will serve as the master diagram.
            Diagram masterDiagram = new Diagram(args[1]);

            // Iterate over remaining diagrams and combine them into the master diagram.
            for (int i = 2; i < args.Length; i++)
            {
                Diagram sourceDiagram = new Diagram(args[i]);

                // Combine merges pages, masters, and shapes (including OLE objects) preserving positions.
                masterDiagram.Combine(sourceDiagram);
            }

            // Save the merged diagram to the specified output path in VSDX format.
            masterDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Merged diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}