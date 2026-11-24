using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Prepare a dictionary to hold master usage counts.
        Dictionary<string, int> masterUsage = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has an associated master.
                        if (shape.Master != null)
                        {
                            string masterName = shape.Master.Name;

                            // Increment usage count for this master.
                            if (masterUsage.ContainsKey(masterName))
                                masterUsage[masterName]++;
                            else
                                masterUsage[masterName] = 1;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Build the report content.
        var reportLines = new List<string>();
        reportLines.Add("Master Usage Report");
        reportLines.Add("====================");
        reportLines.Add($"Diagram: {Path.GetFileName(inputPath)}");
        reportLines.Add($"Generated on: {DateTime.Now}");
        reportLines.Add(string.Empty);
        reportLines.Add("Master Name | Usage Count");
        reportLines.Add("--------------------------");

        foreach (KeyValuePair<string, int> entry in masterUsage)
        {
            reportLines.Add($"{entry.Key} | {entry.Value}");
        }

        // Determine output file path (same folder as input).
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? ".", "MasterUsageReport.txt");

        try
        {
            // Write the report to a text file.
            File.WriteAllLines(outputPath, reportLines);
            Console.WriteLine($"Master usage report saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing report file: {ex.Message}");
        }
    }
}