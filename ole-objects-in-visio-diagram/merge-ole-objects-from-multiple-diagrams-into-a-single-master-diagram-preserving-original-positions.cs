using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: first argument is output path, remaining are input diagram paths
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <output.vsdx> <input1.vsdx> [<input2.vsdx> ...]");
            return;
        }

        string outputPath = args[0];
        List<string> inputPaths = args.Skip(1).ToList();

        // Verify that each input file exists
        foreach (string path in inputPaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        try
        {
            // Load the first diagram which will become the master diagram
            Diagram masterDiagram = new Diagram(inputPaths[0]);

            // Iterate over remaining diagrams and combine them into the master diagram
            for (int i = 1; i < inputPaths.Count; i++)
            {
                string srcPath = inputPaths[i];
                try
                {
                    Diagram srcDiagram = new Diagram(srcPath);
                    // Combine merges pages, masters, and all shapes (including OLE objects)
                    masterDiagram.Combine(srcDiagram);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing source diagram '{srcPath}': {ex.Message}");
                    return;
                }
            }

            // Save the merged diagram to the specified output path in VSDX format
            masterDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Merged diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}