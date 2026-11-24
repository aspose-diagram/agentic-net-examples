using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: at least one output path and one input diagram.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: MergeDiagrams <output.vsdx> <input1.vsdx> [<input2.vsdx> ...]");
            return;
        }

        string outputPath = args[0];

        // Collect input file paths.
        string[] inputPaths = new string[args.Length - 1];
        Array.Copy(args, 1, inputPaths, 0, inputPaths.Length);

        // Verify each input file exists.
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
            // Load the first diagram as the target diagram.
            using (Diagram targetDiagram = new Diagram(inputPaths[0]))
            {
                // Iterate over remaining source diagrams and combine them into the target.
                for (int i = 1; i < inputPaths.Length; i++)
                {
                    string srcPath = inputPaths[i];
                    using (Diagram sourceDiagram = new Diagram(srcPath))
                    {
                        // Combine pages and masters from sourceDiagram into targetDiagram.
                        targetDiagram.Combine(sourceDiagram);
                    }
                }

                // Save the merged diagram to the specified output path.
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Merged diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred during merging: {ex.Message}");
        }
    }
}