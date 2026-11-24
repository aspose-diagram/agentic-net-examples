using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Access the VBA project.
            VbaProject vbaProject = diagram.VbaProject;

            // Prepare metadata container.
            var metadata = new VbaMetadata
            {
                ProjectName = vbaProject.Name,
                IsSigned = vbaProject.IsSigned,
                Modules = new List<ModuleInfo>()
            };

            // Iterate through all VBA modules.
            foreach (VbaModule module in vbaProject.Modules)
            {
                var moduleInfo = new ModuleInfo
                {
                    Name = module.Name,
                    Code = module.Codes,
                    IsValidSyntax = ValidateVbaSyntax(module.Codes)
                };
                metadata.Modules.Add(moduleInfo);
            }

            // Serialize metadata to JSON with indentation.
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(metadata, jsonOptions);

            // Write JSON to the specified output file.
            File.WriteAllText(outputPath, json);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to validate basic VBA syntax.
    // Checks that the number of Sub/End Sub and Function/End Function pairs match.
    static bool ValidateVbaSyntax(string code)
    {
        if (string.IsNullOrEmpty(code))
            return false;

        int subCount = CountOccurrences(code, "Sub ");
        int endSubCount = CountOccurrences(code, "End Sub");
        int funcCount = CountOccurrences(code, "Function ");
        int endFuncCount = CountOccurrences(code, "End Function");

        // Basic validation: each Sub must have a matching End Sub,
        // and each Function must have a matching End Function.
        bool subsBalanced = subCount == endSubCount && subCount > 0;
        bool functionsBalanced = funcCount == endFuncCount && funcCount > 0;

        // At least one Sub or Function should be present.
        bool containsProcedure = subCount > 0 || funcCount > 0;

        return subsBalanced && functionsBalanced && containsProcedure;
    }

    // Counts non‑overlapping occurrences of a substring within a string.
    static int CountOccurrences(string source, string substring)
    {
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

// Classes used for JSON serialization.
class VbaMetadata
{
    public string ProjectName { get; set; }
    public bool IsSigned { get; set; }
    public List<ModuleInfo> Modules { get; set; }
}

class ModuleInfo
{
    public string Name { get; set; }
    public string Code { get; set; }
    public bool IsValidSyntax { get; set; }
}