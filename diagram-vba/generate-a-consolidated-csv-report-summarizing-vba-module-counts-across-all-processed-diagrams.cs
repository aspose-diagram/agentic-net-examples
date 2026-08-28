using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class VbaModuleReport
{
    static void Main(string[] args)
    {
        try
        {

            // Expect diagram file paths as command‑line arguments
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide at least one Visio diagram file path.");
                return;
            }

            var csv = new StringBuilder();
            // CSV header
            csv.AppendLine("DiagramPath,TotalModules,Procedural,Document,Class,Designer");

            foreach (var diagramPath in args)
            {
                // Load diagram using the Diagram(string) constructor (load rule)
                using (var diagram = new Diagram(diagramPath))
                {
                    int total = 0;
                    int procedural = 0, document = 0, @class = 0, designer = 0;

                    var vbaProject = diagram.VbaProject;
                    if (vbaProject != null)
                    {
                        var modules = vbaProject.Modules;
                        total = modules.Count;

                        // Iterate through each VbaModule and count by type
                        foreach (VbaModule module in modules)
                        {
                            switch (module.Type)
                            {
                                case VbaModuleType.Procedural:
                                    procedural++;
                                    break;
                                case VbaModuleType.Document:
                                    document++;
                                    break;
                                case VbaModuleType.Class:
                                    @class++;
                                    break;
                                case VbaModuleType.Designer:
                                    designer++;
                                    break;
                            }
                        }
                    }

                    // Append a CSV line for the current diagram
                    csv.AppendLine($"{Escape(diagramPath)},{total},{procedural},{document},{@class},{designer}");
                }
            }

            // Write the consolidated CSV report to disk (save rule)
            const string outputFile = "VbaModuleReport.csv";
            File.WriteAllText(outputFile, csv.ToString());
            Console.WriteLine($"VBA module report generated: {outputFile}");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }

    // Helper to escape commas and quotes in CSV fields
    static string Escape(string value)
    {
        if (value.Contains(",") || value.Contains("\""))
        {
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }
        return value;
    }
}
