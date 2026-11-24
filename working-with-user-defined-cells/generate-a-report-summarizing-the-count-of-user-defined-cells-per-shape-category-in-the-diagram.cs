using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputReportPath]");
            return;
        }

        string diagramPath = args[0];
        string reportPath = args.Length >= 2 ? args[1] : null;

        // Guard against missing input file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Input file not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Dictionary to aggregate user‑defined cell counts per shape category.
                Dictionary<string, int> categoryCellCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Determine the shape's category.
                        string category = "Unknown";

                        // Prefer the master shape's universal name if available.
                        if (shape.Master != null && !string.IsNullOrWhiteSpace(shape.Master.NameU))
                        {
                            category = shape.Master.NameU;
                        }
                        // Fallback to the shape's own universal name.
                        else if (!string.IsNullOrWhiteSpace(shape.NameU))
                        {
                            category = shape.NameU;
                        }

                        // Count user‑defined cells for this shape.
                        int userCellCount = shape.Users != null ? shape.Users.Count : 0;

                        // Accumulate the count per category.
                        if (categoryCellCounts.ContainsKey(category))
                        {
                            categoryCellCounts[category] += userCellCount;
                        }
                        else
                        {
                            categoryCellCounts[category] = userCellCount;
                        }
                    }
                }

                // Build the report text.
                List<string> reportLines = new List<string>();
                reportLines.Add("User‑Defined Cells Count per Shape Category");
                reportLines.Add("==========================================");
                foreach (KeyValuePair<string, int> kvp in categoryCellCounts)
                {
                    reportLines.Add($"Category: {kvp.Key}, Total User‑Defined Cells: {kvp.Value}");
                }

                string reportContent = string.Join(Environment.NewLine, reportLines);

                // Output the report to console.
                Console.WriteLine(reportContent);

                // Optionally write the report to a file.
                if (!string.IsNullOrEmpty(reportPath))
                {
                    try
                    {
                        File.WriteAllText(reportPath, reportContent);
                        Console.WriteLine($"Report written to: {reportPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to write report file: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}