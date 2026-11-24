using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: first argument is the configuration file, second optional is output file.
        if (args.Length < 1)
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

        // Determine output path; default to "merged.vsdx" in the current directory.
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(Directory.GetCurrentDirectory(), "merged.vsdx");

        // Read all non-empty lines from the configuration file – each line should contain a source diagram path.
        string[] sourcePaths = File.ReadAllLines(configPath);
        var validSourcePaths = new System.Collections.Generic.List<string>();
        foreach (var line in sourcePaths)
        {
            string trimmed = line.Trim();
            if (string.IsNullOrEmpty(trimmed))
                continue;

            if (!File.Exists(trimmed))
            {
                Console.Error.WriteLine($"Source file not found and will be skipped: {trimmed}");
                continue;
            }

            validSourcePaths.Add(trimmed);
        }

        if (validSourcePaths.Count == 0)
        {
            Console.Error.WriteLine("No valid source diagram files were found in the configuration.");
            return;
        }

        try
        {
            // Use the first diagram as the initial target diagram.
            string firstPath = validSourcePaths[0];
            using (Diagram targetDiagram = new Diagram(firstPath))
            {
                // Iterate over remaining source diagrams and merge them into the target.
                for (int i = 1; i < validSourcePaths.Count; i++)
                {
                    string srcPath = validSourcePaths[i];
                    using (Diagram sourceDiagram = new Diagram(srcPath))
                    {
                        // Combine merges pages, masters, and other resources from source into target.
                        targetDiagram.Combine(sourceDiagram);
                    }
                }

                // Save the merged diagram to the specified output path in VSDX format.
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Merged diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related exceptions and report them.
            Console.Error.WriteLine($"An error occurred during processing: {ex.Message}");
        }
    }
}