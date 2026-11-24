using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: Program <DiagramPath1> <DiagramPath2>");
            return;
        }

        string diagramPath1 = args[0];
        string diagramPath2 = args[1];

        // Guard against missing files.
        if (!File.Exists(diagramPath1))
        {
            Console.Error.WriteLine($"File not found: {diagramPath1}");
            return;
        }
        if (!File.Exists(diagramPath2))
        {
            Console.Error.WriteLine($"File not found: {diagramPath2}");
            return;
        }

        try
        {
            // Load the first diagram inside a using block.
            using (Diagram diagram1 = new Diagram(diagramPath1))
            {
                // Load the second diagram inside a using block.
                using (Diagram diagram2 = new Diagram(diagramPath2))
                {
                    // Extract VBA modules from both diagrams.
                    Dictionary<string, string> modules1 = GetVbaModules(diagram1);
                    Dictionary<string, string> modules2 = GetVbaModules(diagram2);

                    // Generate diff report.
                    GenerateDiffReport(modules1, modules2);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagrams: {ex.Message}");
        }
    }

    // Retrieves a dictionary of module name -> VBA code for a given diagram.
    private static Dictionary<string, string> GetVbaModules(Diagram diagram)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // The VbaProject property may be null if the diagram has no macros.
        if (diagram.VbaProject != null)
        {
            VbaModuleCollection modules = diagram.VbaProject.Modules;
            for (int i = 0; i < modules.Count; i++)
            {
                VbaModule module = modules[i];
                string name = module.Name ?? $"Module_{i}";
                string code = module.Codes ?? string.Empty;
                dict[name] = code;
            }
        }

        return dict;
    }

    // Compares two module dictionaries and writes a diff report to the console.
    private static void GenerateDiffReport(Dictionary<string, string> modules1, Dictionary<string, string> modules2)
    {
        Console.WriteLine("=== VBA Diff Report ===");
        Console.WriteLine();

        // Modules present only in the first diagram.
        foreach (KeyValuePair<string, string> kvp in modules1)
        {
            if (!modules2.ContainsKey(kvp.Key))
            {
                Console.WriteLine($"Module only in first diagram: {kvp.Key}");
                Console.WriteLine("--- Code ---");
                Console.WriteLine(kvp.Value);
                Console.WriteLine();
            }
        }

        // Modules present only in the second diagram.
        foreach (KeyValuePair<string, string> kvp in modules2)
        {
            if (!modules1.ContainsKey(kvp.Key))
            {
                Console.WriteLine($"Module only in second diagram: {kvp.Key}");
                Console.WriteLine("--- Code ---");
                Console.WriteLine(kvp.Value);
                Console.WriteLine();
            }
        }

        // Modules present in both diagrams – compare their code.
        foreach (KeyValuePair<string, string> kvp in modules1)
        {
            string moduleName = kvp.Key;
            if (modules2.TryGetValue(moduleName, out string code2))
            {
                string code1 = kvp.Value;
                if (!string.Equals(code1, code2, StringComparison.Ordinal))
                {
                    Console.WriteLine($"Differences found in module: {moduleName}");
                    PrintLineDiffs(code1, code2);
                    Console.WriteLine();
                }
            }
        }

        Console.WriteLine("=== End of Report ===");
    }

    // Prints a simple line‑by‑line diff between two code strings.
    private static void PrintLineDiffs(string code1, string code2)
    {
        string[] lines1 = code1.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        string[] lines2 = code2.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        int maxLines = Math.Max(lines1.Length, lines2.Length);

        for (int i = 0; i < maxLines; i++)
        {
            string line1 = i < lines1.Length ? lines1[i] : string.Empty;
            string line2 = i < lines2.Length ? lines2[i] : string.Empty;

            if (!string.Equals(line1, line2, StringComparison.Ordinal))
            {
                Console.WriteLine($"- Line {i + 1} (First): {line1}");
                Console.WriteLine($"+ Line {i + 1} (Second): {line2}");
            }
        }
    }
}