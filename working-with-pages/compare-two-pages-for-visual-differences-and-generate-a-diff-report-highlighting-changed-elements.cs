using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: first diagram path, second diagram path, page index (0‑based)
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <diagram1.vsdx> <diagram2.vsdx> <pageIndex>");
            return;
        }

        string diagramPath1 = args[0];
        string diagramPath2 = args[1];
        string pageIndexArg = args[2];

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

        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Invalid page index. It must be a non‑negative integer.");
            return;
        }

        try
        {
            // Load both diagrams inside using blocks as required by the API
            using (Diagram diagram1 = new Diagram(diagramPath1))
            using (Diagram diagram2 = new Diagram(diagramPath2))
            {
                // Validate page index range for both diagrams
                if (pageIndex >= diagram1.Pages.Count || pageIndex >= diagram2.Pages.Count)
                {
                    Console.Error.WriteLine("Page index out of range for one of the diagrams.");
                    return;
                }

                // Retrieve the pages to compare
                Page page1 = diagram1.Pages[pageIndex];
                Page page2 = diagram2.Pages[pageIndex];

                // Build dictionaries of shapes keyed by their unique IDs
                var shapes1 = new Dictionary<long, Shape>();
                foreach (Shape shape in page1.Shapes)
                {
                    shapes1[shape.ID] = shape;
                }

                var shapes2 = new Dictionary<long, Shape>();
                foreach (Shape shape in page2.Shapes)
                {
                    shapes2[shape.ID] = shape;
                }

                var reportLines = new List<string>();
                const double tolerance = 0.001; // tolerance for numeric comparisons

                // Detect removed or modified shapes
                foreach (var kvp in shapes1)
                {
                    long shapeId = kvp.Key;
                    Shape shape1 = kvp.Value;

                    if (!shapes2.TryGetValue(shapeId, out Shape shape2))
                    {
                        // Shape exists in diagram1 but not in diagram2 → removed
                        reportLines.Add($"Removed Shape ID {shapeId}");
                        continue;
                    }

                    // Compare relevant visual properties
                    var changes = new List<string>();

                    // Position X (PinX)
                    double pinX1 = shape1.XForm.PinX.Value;
                    double pinX2 = shape2.XForm.PinX.Value;
                    if (Math.Abs(pinX1 - pinX2) > tolerance)
                        changes.Add($"PinX: {pinX1} → {pinX2}");

                    // Position Y (PinY)
                    double pinY1 = shape1.XForm.PinY.Value;
                    double pinY2 = shape2.XForm.PinY.Value;
                    if (Math.Abs(pinY1 - pinY2) > tolerance)
                        changes.Add($"PinY: {pinY1} → {pinY2}");

                    // Width
                    double width1 = shape1.XForm.Width.Value;
                    double width2 = shape2.XForm.Width.Value;
                    if (Math.Abs(width1 - width2) > tolerance)
                        changes.Add($"Width: {width1} → {width2}");

                    // Height
                    double height1 = shape1.XForm.Height.Value;
                    double height2 = shape2.XForm.Height.Value;
                    if (Math.Abs(height1 - height2) > tolerance)
                        changes.Add($"Height: {height1} → {height2}");

                    // Text content
                    string text1 = shape1.Text?.Value?.Text ?? string.Empty;
                    string text2 = shape2.Text?.Value?.Text ?? string.Empty;
                    if (!string.Equals(text1, text2, StringComparison.Ordinal))
                        changes.Add($"Text: \"{text1}\" → \"{text2}\"");

                    // If any visual property changed, record it
                    if (changes.Count > 0)
                    {
                        reportLines.Add($"Modified Shape ID {shapeId}:");
                        foreach (string change in changes)
                        {
                            reportLines.Add($"  - {change}");
                        }
                    }
                }

                // Detect added shapes (present only in diagram2)
                foreach (var kvp in shapes2)
                {
                    long shapeId = kvp.Key;
                    if (!shapes1.ContainsKey(shapeId))
                    {
                        reportLines.Add($"Added Shape ID {shapeId}");
                    }
                }

                // Output the diff report to console
                Console.WriteLine("=== Visual Diff Report ===");
                if (reportLines.Count == 0)
                {
                    Console.WriteLine("No visual differences detected between the pages.");
                }
                else
                {
                    foreach (string line in reportLines)
                    {
                        Console.WriteLine(line);
                    }
                }

                // Optionally write the report to a text file
                string reportPath = "diff_report.txt";
                try
                {
                    File.WriteAllLines(reportPath, reportLines);
                    Console.WriteLine($"Diff report written to: {Path.GetFullPath(reportPath)}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to write diff report file: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram or I/O exceptions and report them
            Console.Error.WriteLine($"Error processing diagrams: {ex.Message}");
        }
    }
}