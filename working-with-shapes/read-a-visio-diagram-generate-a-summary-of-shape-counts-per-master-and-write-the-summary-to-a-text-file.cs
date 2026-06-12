using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Diagram;

class VisioShapeSummary
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputFile = "input.vsdx";

            // Path to the output text file
            string outputFile = "shape_summary.txt";

            // Load the Visio diagram (uses Diagram(string) constructor)
            using (Diagram diagram = new Diagram(inputFile))
            {
                // Dictionary to hold counts per master name
                var masterCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages and their shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Determine the master name; if no master, categorize as "NoMaster"
                        string masterName = shape.Master != null
                            ? (!string.IsNullOrEmpty(shape.Master.NameU) ? shape.Master.NameU : shape.Master.Name)
                            : "NoMaster";

                        if (string.IsNullOrEmpty(masterName))
                            masterName = "UnnamedMaster";

                        // Increment count
                        if (masterCounts.ContainsKey(masterName))
                            masterCounts[masterName]++;
                        else
                            masterCounts[masterName] = 1;
                    }
                }

                // Build the summary text
                var sb = new StringBuilder();
                sb.AppendLine("Shape Count per Master:");
                foreach (var kvp in masterCounts)
                {
                    sb.AppendLine($"{kvp.Key}: {kvp.Value}");
                }

                // Write the summary to a text file
                File.WriteAllText(outputFile, sb.ToString());
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
