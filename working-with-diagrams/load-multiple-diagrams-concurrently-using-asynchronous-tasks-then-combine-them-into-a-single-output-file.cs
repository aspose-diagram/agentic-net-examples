using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Run the asynchronous workflow synchronously.
        RunAsync(args).GetAwaiter().GetResult();
    }

    private static async Task RunAsync(string[] args)
    {
        // Validate arguments: first argument is output file, remaining are input files.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <outputPath> <inputPath1> [<inputPath2> ...]");
            return;
        }

        string outputPath = args[0];
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        // Collect input file paths.
        var inputPaths = new List<string>();
        for (int i = 1; i < args.Length; i++)
        {
            string path = args[i];
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
            inputPaths.Add(path);
        }

        // Load all diagrams concurrently.
        var loadTasks = new List<Task<Diagram>>();
        foreach (var path in inputPaths)
        {
            loadTasks.Add(Task.Run(() =>
            {
                try
                {
                    // Load diagram from file.
                    return new Diagram(path);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to load diagram '{path}': {ex.Message}");
                    throw;
                }
            }));
        }

        Diagram[] diagrams;
        try
        {
            diagrams = await Task.WhenAll(loadTasks);
        }
        catch
        {
            // One or more load operations failed; abort.
            return;
        }

        // Use the first diagram as the base for combination.
        Diagram baseDiagram = diagrams[0];

        // Combine remaining diagrams into the base diagram.
        for (int i = 1; i < diagrams.Length; i++)
        {
            try
            {
                baseDiagram.Combine(diagrams[i]);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to combine diagram '{inputPaths[i]}' into base diagram: {ex.Message}");
                // Continue with next diagram.
            }
        }

        // Save the combined diagram to the specified output path.
        try
        {
            baseDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Combined diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save combined diagram: {ex.Message}");
        }
    }
}