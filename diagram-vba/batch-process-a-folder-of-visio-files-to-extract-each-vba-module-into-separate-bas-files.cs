using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Get folder path from first argument
        string folderPath = args.Length > 0 ? args[0] : string.Empty;
        if (string.IsNullOrWhiteSpace(folderPath))
        {
            Console.WriteLine("Please provide the folder path as the first argument.");
            return;
        }

        // Verify folder exists
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Define Visio file extensions to process
        string[] extensions = new[]
        {
            ".vsdx", ".vsdm", ".vsd", ".vsx",
            ".vssx", ".vss", ".vstx", ".vst"
        };

        // Get all matching files in the folder
        var files = Directory.GetFiles(folderPath)
            .Where(f => extensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
            .ToList();

        if (files.Count == 0)
        {
            Console.WriteLine("No Visio files found in the specified folder.");
            return;
        }

        // Process each Visio file
        foreach (var filePath in files)
        {
            // Guard against missing file
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(filePath);

                // Check for VBA project
                if (diagram.VbaProject == null)
                {
                    Console.WriteLine($"No VBA project in file: {filePath}");
                    continue;
                }

                // Check for VBA modules
                if (diagram.VbaProject.Modules == null || diagram.VbaProject.Modules.Count == 0)
                {
                    Console.WriteLine($"No VBA modules in file: {filePath}");
                    continue;
                }

                // Prepare output directory for extracted modules
                string outputDir = Path.Combine(
                    Path.GetDirectoryName(filePath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(filePath) + "_VbaModules");
                Directory.CreateDirectory(outputDir);

                // Iterate through each VBA module
                foreach (VbaModule module in diagram.VbaProject.Modules)
                {
                    string moduleName = module.Name ?? "UnnamedModule";
                    string code = module.Codes ?? string.Empty;

                    // Simple syntax validation: count Sub and End Sub occurrences
                    int subCount = CountOccurrences(code, "Sub ");
                    int endSubCount = CountOccurrences(code, "End Sub");
                    if (subCount != endSubCount)
                    {
                        Console.Error.WriteLine(
                            $"Syntax mismatch in module '{moduleName}' of file '{filePath}': " +
                            $"Sub count {subCount} vs End Sub count {endSubCount}.");
                    }

                    // Ensure the module contains at least one Sub or Function
                    bool containsProcedure = code.IndexOf("Sub ", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                             code.IndexOf("Function ", StringComparison.OrdinalIgnoreCase) >= 0;
                    if (!containsProcedure)
                    {
                        Console.Error.WriteLine(
                            $"Warning: Module '{moduleName}' in file '{filePath}' does not contain Sub or Function definitions.");
                    }

                    // Build output file path
                    string outFileName = $"{Path.GetFileNameWithoutExtension(filePath)}_{moduleName}.bas";
                    string outPath = Path.Combine(outputDir, outFileName);

                    // Write the VBA code to a .bas file
                    File.WriteAllText(outPath, code);
                    Console.WriteLine($"Extracted module '{moduleName}' to '{outPath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Helper method to count case‑insensitive occurrences of a substring
    static int CountOccurrences(string source, string substring)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(substring))
            return 0;

        int count = 0;
        int index = 0;
        while ((index = source.IndexOf(substring, index, StringComparison.OrdinalIgnoreCase)) != -1)
        {
            count++;
            index += substring.Length;
        }
        return count;
    }
}