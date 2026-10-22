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
        // Expect two arguments: input Visio file path and output JSON file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the VBA project (read‑only)
            VbaProject vbaProject = diagram.VbaProject;

            // Prepare the metadata object
            VbaProjectInfo projectInfo = new VbaProjectInfo
            {
                Name = vbaProject.Name,
                IsSigned = vbaProject.IsSigned,
                Modules = new List<VbaModuleInfo>()
            };

            // Iterate through all VBA modules
            foreach (VbaModule module in vbaProject.Modules)
            {
                // Validate VBA code by checking Sub/End Sub pairing
                bool isValid = ValidateVbaCode(module.Codes);

                VbaModuleInfo moduleInfo = new VbaModuleInfo
                {
                    Name = module.Name,
                    Code = module.Codes,
                    IsValid = isValid
                };

                projectInfo.Modules.Add(moduleInfo);
            }

            // Serialize metadata to formatted JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(projectInfo, jsonOptions);

            // Ensure the output directory exists
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Write JSON to the specified file
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"VBA project metadata has been written to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to count occurrences of a substring (case‑sensitive)
    private static int CountOccurrences(string source, string substring)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(substring))
            return 0;

        int count = 0;
        int index = 0;
        while ((index = source.IndexOf(substring, index, StringComparison.Ordinal)) != -1)
        {
            count++;
            index += substring.Length;
        }
        return count;
    }

    // Validates VBA code by ensuring each Sub has a matching End Sub and at least one Sub or Function exists
    private static bool ValidateVbaCode(string code)
    {
        int subCount = CountOccurrences(code, "Sub ");
        int endSubCount = CountOccurrences(code, "End Sub");
        int functionCount = CountOccurrences(code, "Function ");

        // Valid if Sub/End Sub pairs match and there is at least one Sub or Function
        return (subCount == endSubCount) && (subCount > 0 || functionCount > 0);
    }
}

// DTO for serializing VBA project information
class VbaProjectInfo
{
    public string Name { get; set; }
    public bool IsSigned { get; set; }
    public List<VbaModuleInfo> Modules { get; set; }
}

// DTO for serializing individual VBA module information
class VbaModuleInfo
{
    public string Name { get; set; }
    public string Code { get; set; }
    public bool IsValid { get; set; }
}