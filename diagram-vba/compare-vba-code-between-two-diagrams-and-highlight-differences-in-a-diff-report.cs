using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <DiagramPath1> <DiagramPath2> [ReportOutputPath]");
            return;
        }

        string diagramPath1 = args[0];
        string diagramPath2 = args[1];
        string reportPath = args.Length >= 3 ? args[2] : null;

        // Guard against missing files
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

        Diagram diagram1 = null;
        Diagram diagram2 = null;

        try
        {
            // Load first diagram
            diagram1 = new Diagram(diagramPath1);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram 1: {ex.Message}");
            return;
        }

        try
        {
            // Load second diagram
            diagram2 = new Diagram(diagramPath2);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram 2: {ex.Message}");
            return;
        }

        try
        {
            // Build a report of VBA differences
            string report = GenerateVbaDiffReport(diagram1, diagram2, diagramPath1, diagramPath2);

            // Output to console
            Console.WriteLine(report);

            // Optionally write to a file
            if (!string.IsNullOrEmpty(reportPath))
            {
                try
                {
                    File.WriteAllText(reportPath, report);
                    Console.WriteLine($"Diff report written to: {reportPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to write report file: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diff processing: {ex.Message}");
        }
    }

    // Generates a textual diff report for VBA modules between two diagrams
    private static string GenerateVbaDiffReport(Diagram diagramA, Diagram diagramB, string pathA, string pathB)
    {
        using (StringWriter writer = new StringWriter())
        {
            writer.WriteLine("VBA Diff Report");
            writer.WriteLine($"Diagram A: {pathA}");
            writer.WriteLine($"Diagram B: {pathB}");
            writer.WriteLine(new string('=', 40));

            // Create dictionaries for quick lookup by module name
            Dictionary<string, string> modulesA = BuildModuleDictionary(diagramA);
            Dictionary<string, string> modulesB = BuildModuleDictionary(diagramB);

            // Check modules present in A
            foreach (string moduleName in new List<string>(modulesA.Keys))
            {
                if (modulesB.ContainsKey(moduleName))
                {
                    string codeA = modulesA[moduleName];
                    string codeB = modulesB[moduleName];

                    if (!string.Equals(codeA, codeB, StringComparison.Ordinal))
                    {
                        writer.WriteLine($"[Changed] Module: {moduleName}");
                        writer.WriteLine("--- Diagram A Code ---");
                        writer.WriteLine(codeA);
                        writer.WriteLine("--- Diagram B Code ---");
                        writer.WriteLine(codeB);
                        writer.WriteLine(new string('-', 30));
                    }
                    // Remove from B dictionary to handle remaining items later
                    modulesB.Remove(moduleName);
                }
                else
                {
                    writer.WriteLine($"[Removed] Module: {moduleName} (present only in Diagram A)");
                }
            }

            // Remaining modules in B are added modules
            foreach (string moduleName in modulesB.Keys)
            {
                writer.WriteLine($"[Added] Module: {moduleName} (present only in Diagram B)");
                writer.WriteLine("--- Diagram B Code ---");
                writer.WriteLine(modulesB[moduleName]);
                writer.WriteLine(new string('-', 30));
            }

            writer.WriteLine("End of Report");
            return writer.ToString();
        }
    }

    // Builds a dictionary of module name -> code for a given diagram
    private static Dictionary<string, string> BuildModuleDictionary(Diagram diagram)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        VbaModuleCollection modules = diagram.VbaProject.Modules;

        for (int i = 0; i < modules.Count; i++)
        {
            VbaModule module = modules[i];
            string name = module.Name ?? $"UnnamedModule_{i}";
            string code = module.Codes ?? string.Empty;
            dict[name] = code;
        }

        return dict;
    }
}