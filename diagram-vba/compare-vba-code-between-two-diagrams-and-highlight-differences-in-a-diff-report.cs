using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect two file paths: first diagram, second diagram
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiffVba <DiagramPath1> <DiagramPath2>");
            return;
        }

        string path1 = args[0];
        string path2 = args[1];

        Diagram diagram1;
        Diagram diagram2;

        try
        {
            diagram1 = new Diagram(path1);
            diagram2 = new Diagram(path2);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading diagrams: {ex.Message}");
            return;
        }

        // Ensure VBA projects exist
        if (diagram1.VbaProject == null || diagram2.VbaProject == null)
        {
            Console.WriteLine("One of the diagrams does not contain a VBA project.");
            return;
        }

        // Build dictionaries of module name -> code
        var modules1 = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (VbaModule mod in diagram1.VbaProject.Modules)
        {
            modules1[mod.Name] = mod.Codes ?? string.Empty;
        }

        var modules2 = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (VbaModule mod in diagram2.VbaProject.Modules)
        {
            modules2[mod.Name] = mod.Codes ?? string.Empty;
        }

        // Union of all module names
        var allModuleNames = new HashSet<string>(modules1.Keys, StringComparer.OrdinalIgnoreCase);
        allModuleNames.UnionWith(modules2.Keys);

        bool anyDifferences = false;

        foreach (string moduleName in allModuleNames)
        {
            modules1.TryGetValue(moduleName, out string code1);
            modules2.TryGetValue(moduleName, out string code2);

            // Normalize nulls
            code1 ??= string.Empty;
            code2 ??= string.Empty;

            if (code1 == code2)
            {
                // No difference for this module
                continue;
            }

            anyDifferences = true;
            Console.WriteLine($"--- Differences in module: {moduleName} ---");

            string[] lines1 = code1.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            string[] lines2 = code2.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            int maxLines = Math.Max(lines1.Length, lines2.Length);

            for (int i = 0; i < maxLines; i++)
            {
                string line1 = i < lines1.Length ? lines1[i] : string.Empty;
                string line2 = i < lines2.Length ? lines2[i] : string.Empty;

                if (line1 != line2)
                {
                    Console.WriteLine($"Line {i + 1}:");
                    Console.WriteLine($"  Diagram 1: {line1}");
                    Console.WriteLine($"  Diagram 2: {line2}");
                }
            }

            Console.WriteLine(); // Blank line between modules
        }

        if (!anyDifferences)
        {
            Console.WriteLine("No differences found between the VBA modules of the two diagrams.");
        }
    }
}
