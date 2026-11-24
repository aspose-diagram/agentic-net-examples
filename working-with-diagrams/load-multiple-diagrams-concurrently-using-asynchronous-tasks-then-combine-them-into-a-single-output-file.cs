using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least one source diagram and an output path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <source1.vsdx> [<source2.vsdx> ...] <output.vsdx>");
            return;
        }

        // The last argument is the output file, the rest are input diagrams
        string outputPath = args[args.Length - 1];
        string[] inputPaths = args.Take(args.Length - 1).ToArray();

        // Guard: ensure every input file exists
        foreach (var path in inputPaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Load all diagram files concurrently into memory
        byte[][] fileData;
        try
        {
            var loadTasks = inputPaths.Select(p => File.ReadAllBytesAsync(p)).ToArray();
            Task.WaitAll(loadTasks);
            fileData = loadTasks.Select(t => t.Result).ToArray();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading input files: {ex.Message}");
            return;
        }

        // Combine diagrams using Aspose.Diagram.Combine
        try
        {
            // The first diagram serves as the target diagram
            using (var targetDiagram = new Diagram(new MemoryStream(fileData[0])))
            {
                // Iterate over remaining diagrams and merge them into the target
                for (int i = 1; i < fileData.Length; i++)
                {
                    using (var sourceDiagram = new Diagram(new MemoryStream(fileData[i])))
                    {
                        targetDiagram.Combine(sourceDiagram);
                    }
                }

                // Save the combined diagram to the specified output path
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Combined diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram combination or saving: {ex.Message}");
        }
    }
}