using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class VbaDiff
{
    static void Main(string[] args)
    {
        // Expect two diagram file paths as command‑line arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: VbaDiff <diagram1.vsdx> <diagram2.vsdx>");
            return;
        }

        string filePath1 = args[0];
        string filePath2 = args[1];

        // Load the diagrams using the provided constructors (lifecycle rule)
        Diagram diagram1 = new Diagram(filePath1);
        Diagram diagram2 = new Diagram(filePath2);

        // Access the VBA projects of each diagram
        VbaProject vbaProject1 = diagram1.VbaProject;
        VbaProject vbaProject2 = diagram2.VbaProject;

        // Build dictionaries: module name -> code
        var modules1 = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (VbaModule module in vbaProject1.Modules)
        {
            modules1[module.Name] = module.Codes ?? string.Empty;
        }

        var modules2 = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (VbaModule module in vbaProject2.Modules)
        {
            modules2[module.Name] = module.Codes ?? string.Empty;
        }

        // Prepare diff report lines
        var report = new List<string>();
        report.Add($"VBA Diff Report between '{Path.GetFileName(filePath1)}' and '{Path.GetFileName(filePath2)}'");
        report.Add(new string('=', 80));

        // Compare modules present in the first diagram
        foreach (var kvp in modules1)
        {
            string moduleName = kvp.Key;
            string code1 = kvp.Value;

            if (!modules2.ContainsKey(moduleName))
            {
                report.Add($"Module '{moduleName}' exists only in the first diagram.");
                continue;
            }

            string code2 = modules2[moduleName];
            if (code1 != code2)
            {
                report.Add($"Module '{moduleName}' differs:");
                report.Add("--- First Diagram ---");
                report.AddRange(code1.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None));
                report.Add("--- Second Diagram ---");
                report.AddRange(code2.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None));
                report.Add(new string('-', 40));
            }
        }

        // Identify modules that exist only in the second diagram
        foreach (var kvp in modules2)
        {
            if (!modules1.ContainsKey(kvp.Key))
            {
                report.Add($"Module '{kvp.Key}' exists only in the second diagram.");
            }
        }

        // Output report to console
        foreach (string line in report)
        {
            Console.WriteLine(line);
        }

        // Save report to a text file (standard .NET I/O, not a diagram save)
        string reportPath = "VbaDiffReport.txt";
        File.WriteAllLines(reportPath, report);
        Console.WriteLine($"Diff report saved to {reportPath}");
    }
}
