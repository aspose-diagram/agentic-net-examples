using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard input folder existence.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Define supported Visio extensions.
        string[] extensions = new[] { ".vsdx", ".vsd", ".vssx", ".vss", ".vstx", ".vst", ".vsdm", ".vsdm" };

        // Get all files with supported extensions.
        string[] allFiles = Directory.GetFiles(inputFolder);
        foreach (string filePath in allFiles)
        {
            string ext = Path.GetExtension(filePath);
            if (Array.IndexOf(extensions, ext, 0, extensions.Length) < 0)
                continue; // Skip non‑Visio files.

            // Guard file existence (redundant but per rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            Console.WriteLine($"Processing file: {filePath}");

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Ensure the diagram contains a VBA project.
                if (diagram.VbaProject == null)
                {
                    Console.WriteLine($"No VBA project found in {filePath}.");
                    continue;
                }

                // Iterate through each VBA module.
                foreach (VbaModule module in diagram.VbaProject.Modules)
                {
                    string moduleName = module.Name;
                    string code = module.Codes ?? string.Empty;

                    // Validate VBA syntax: Sub/End Sub count matching.
                    int subCount = CountOccurrences(code, "Sub ");
                    int endSubCount = CountOccurrences(code, "End Sub");
                    if (subCount != endSubCount)
                    {
                        Console.Error.WriteLine($"Syntax error in module '{moduleName}' of file '{filePath}': Sub/End Sub count mismatch ({subCount} vs {endSubCount}).");
                    }

                    // Ensure at least one Sub or Function exists.
                    int functionCount = CountOccurrences(code, "Function ");
                    if (subCount == 0 && functionCount == 0)
                    {
                        Console.Error.WriteLine($"Warning: Module '{moduleName}' in file '{filePath}' contains no Sub or Function.");
                    }

                    // Prepare output .bas file path.
                    string baseFileName = Path.GetFileNameWithoutExtension(filePath);
                    string safeModuleName = MakeFileSystemSafeName(moduleName);
                    string outputPath = Path.Combine(outputFolder, $"{baseFileName}_{safeModuleName}.bas");

                    // Write the VBA code to the .bas file.
                    try
                    {
                        File.WriteAllText(outputPath, code);
                        Console.WriteLine($"Extracted module '{moduleName}' to '{outputPath}'.");
                    }
                    catch (Exception writeEx)
                    {
                        Console.Error.WriteLine($"Failed to write module '{moduleName}' to file: {writeEx.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }

    // Helper: count case‑insensitive occurrences of a substring.
    private static int CountOccurrences(string source, string substring)
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

    // Helper: make a string safe for file names by removing invalid characters.
    private static string MakeFileSystemSafeName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}