using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputCsvPath>");
            return;
        }

        string inputFolder = args[0];
        string outputCsvPath = args[1];

        // Verify the input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Prepare to collect results.
        var results = new System.Collections.Generic.List<(string FileName, int CustomPropCount)>();

        // Iterate over files in the folder (common Visio extensions).
        string[] visioExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vstx", ".vsdm", ".vssm", ".vstm", ".vss", ".vst" };
        foreach (string filePath in Directory.GetFiles(inputFolder))
        {
            // Process only files with Visio extensions.
            if (Array.IndexOf(visioExtensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                continue;

            // Guard against missing files (should not happen in GetFiles, but follow the rule).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Retrieve the count of custom document properties.
                    int customPropCount = diagram.DocumentProps.CustomProps.Count;
                    results.Add((Path.GetFileName(filePath), customPropCount));
                }
            }
            catch (Exception ex)
            {
                // Log loading errors and continue with the next file.
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }

        try
        {
            // Write the summary CSV.
            using (var writer = new StreamWriter(outputCsvPath, false))
            {
                // Header row.
                writer.WriteLine("Filename,CustomPropertyCount");

                // Data rows.
                foreach (var entry in results)
                {
                    writer.WriteLine($"{entry.FileName},{entry.CustomPropCount}");
                }
            }

            Console.WriteLine($"Summary CSV created at: {outputCsvPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write CSV file: {ex.Message}");
        }
    }
}