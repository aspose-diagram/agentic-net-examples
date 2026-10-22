using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: first argument is the path to a text file containing source diagram paths (one per line)
        // second argument is the output combined diagram path (optional)
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <configFilePath> [outputFilePath]");
            return;
        }

        string configPath = args[0];
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Determine output path
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(configPath) ?? "", "Combined.vsdx");

        // Read source diagram file paths from configuration file
        List<string> sourcePaths = new List<string>();
        try
        {
            foreach (var line in File.ReadAllLines(configPath))
            {
                string trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed))
                    continue;

                if (!File.Exists(trimmed))
                {
                    Console.Error.WriteLine($"Source diagram file not found: {trimmed}");
                    return;
                }
                sourcePaths.Add(trimmed);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration file: {ex.Message}");
            return;
        }

        if (sourcePaths.Count == 0)
        {
            Console.Error.WriteLine("No valid source diagram paths found in configuration.");
            return;
        }

        // Use the first diagram as the initial target diagram
        string firstDiagramPath = sourcePaths[0];
        try
        {
            using (Diagram targetDiagram = new Diagram(firstDiagramPath))
            {
                // Iterate over remaining source diagrams and combine them into the target
                for (int i = 1; i < sourcePaths.Count; i++)
                {
                    string srcPath = sourcePaths[i];
                    try
                    {
                        using (Diagram srcDiagram = new Diagram(srcPath))
                        {
                            // Combine pages, masters, and other resources from srcDiagram into targetDiagram
                            targetDiagram.Combine(srcDiagram);
                        }
                    }
                    catch (Exception exCombine)
                    {
                        Console.Error.WriteLine($"Error processing source diagram '{srcPath}': {exCombine.Message}");
                        // Continue with next source diagram
                    }
                }

                // Save the combined diagram to the specified output path
                try
                {
                    targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Combined diagram saved to: {outputPath}");
                }
                catch (Exception exSave)
                {
                    Console.Error.WriteLine($"Error saving combined diagram: {exSave.Message}");
                }
            }
        }
        catch (Exception exTarget)
        {
            Console.Error.WriteLine($"Error loading initial target diagram '{firstDiagramPath}': {exTarget.Message}");
        }
    }
}