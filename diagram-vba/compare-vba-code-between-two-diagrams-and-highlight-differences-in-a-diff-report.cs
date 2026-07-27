using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the two Visio files to compare
            string diagramPath1 = "Diagram1.vsdx";
            string diagramPath2 = "Diagram2.vsdx";

            // Load the diagrams
            Diagram diagram1 = new Diagram(diagramPath1);
            Diagram diagram2 = new Diagram(diagramPath2);

            // Build dictionaries of module name -> VBA code for each diagram
            var vbaMap1 = BuildVbaModuleMap(diagram1);
            var vbaMap2 = BuildVbaModuleMap(diagram2);

            // Prepare the diff report
            StringWriter report = new StringWriter();

            // Compare modules present in the first diagram
            foreach (var kvp in vbaMap1)
            {
                string moduleName = kvp.Key;
                string code1 = kvp.Value;

                if (vbaMap2.TryGetValue(moduleName, out string code2))
                {
                    // Both diagrams have the module – compare line by line
                    CompareModuleCode(moduleName, code1, code2, report);
                }
                else
                {
                    // Module missing in the second diagram
                    report.WriteLine($"Module '{moduleName}' exists in Diagram1 but not in Diagram2.");
                }
            }

            // Find modules that exist only in the second diagram
            foreach (var moduleName in vbaMap2.Keys)
            {
                if (!vbaMap1.ContainsKey(moduleName))
                {
                    report.WriteLine($"Module '{moduleName}' exists in Diagram2 but not in Diagram1.");
                }
            }

            // Output the diff report to console
            Console.WriteLine(report.ToString());

            // Also write the report to a text file
            string reportPath = "VbaDiffReport.txt";
            File.WriteAllText(reportPath, report.ToString());
            Console.WriteLine($"Diff report saved to '{reportPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Builds a dictionary of module name to its VBA source code
    private static System.Collections.Generic.Dictionary<string, string> BuildVbaModuleMap(Diagram diagram)
    {
        var map = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (VbaModule module in diagram.VbaProject.Modules)
        {
            // Ensure the module has a name; skip empty entries
            if (!string.IsNullOrWhiteSpace(module.Name))
            {
                map[module.Name] = module.Codes ?? string.Empty;
            }
        }
        return map;
    }

    // Compares two code strings line by line and writes differences to the report
    private static void CompareModuleCode(string moduleName, string code1, string code2, StringWriter report)
    {
        string[] lines1 = code1.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        string[] lines2 = code2.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        int maxLines = Math.Max(lines1.Length, lines2.Length);
        bool hasDifferences = false;

        for (int i = 0; i < maxLines; i++)
        {
            string line1 = i < lines1.Length ? lines1[i] : string.Empty;
            string line2 = i < lines2.Length ? lines2[i] : string.Empty;

            if (!string.Equals(line1, line2, StringComparison.Ordinal))
            {
                if (!hasDifferences)
                {
                    report.WriteLine($"Differences in module '{moduleName}':");
                    hasDifferences = true;
                }
                report.WriteLine($"  Line {i + 1}:");
                report.WriteLine($"    Diagram1: {line1}");
                report.WriteLine($"    Diagram2: {line2}");
            }
        }

        if (!hasDifferences)
        {
            report.WriteLine($"Module '{moduleName}' is identical in both diagrams.");
        }
    }
}
