using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file containing the pages to compare
            string diagramPath = "input.vsdx";

            // Indices of the two pages to compare (0‑based)
            int pageIndex1 = 0;
            int pageIndex2 = 1;

            // Load the diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Validate that the requested pages exist
                if (diagram.Pages.Count <= Math.Max(pageIndex1, pageIndex2))
                {
                    Console.WriteLine("The diagram does not contain the specified pages.");
                    return;
                }

                Page page1 = diagram.Pages[pageIndex1];
                Page page2 = diagram.Pages[pageIndex2];

                // Build a lookup dictionary for shapes on the second page
                var page2Lookup = new Dictionary<string, Shape>();
                foreach (Shape shape2 in page2.Shapes)
                {
                    string key = GetShapeKey(shape2);
                    if (!page2Lookup.ContainsKey(key))
                        page2Lookup[key] = shape2;
                }

                var differences = new List<string>();

                // Compare each shape on the first page with the corresponding shape on the second page
                foreach (Shape shape1 in page1.Shapes)
                {
                    string key = GetShapeKey(shape1);
                    if (page2Lookup.TryGetValue(key, out Shape shape2))
                    {
                        CompareShapes(shape1, shape2, key, differences);
                        // Remove matched shape to later detect shapes that exist only on page2
                        page2Lookup.Remove(key);
                    }
                    else
                    {
                        differences.Add($"Shape '{key}' exists on Page {pageIndex1 + 1} but not on Page {pageIndex2 + 1}.");
                    }
                }

                // Remaining shapes in the lookup are new on page2
                foreach (var kvp in page2Lookup)
                {
                    differences.Add($"Shape '{kvp.Key}' exists on Page {pageIndex2 + 1} but not on Page {pageIndex1 + 1}.");
                }

                // Write the diff report to a text file
                string reportPath = "DiffReport.txt";
                File.WriteAllLines(reportPath, differences);
                Console.WriteLine($"Diff report generated at: {reportPath}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Creates a stable identifier for a shape using its universal name and master name
    private static string GetShapeKey(Shape shape)
    {
        string name = shape.NameU ?? string.Empty;
        string master = shape.Master != null ? shape.Master.Name ?? string.Empty : string.Empty;
        return $"{name}|{master}";
    }

    // Compares selected visual properties of two shapes and records any differences
    private static void CompareShapes(Shape s1, Shape s2, string key, List<string> diffs)
    {
        // Position
        if (!AreClose(s1.XForm.PinX.Value, s2.XForm.PinX.Value) ||
            !AreClose(s1.XForm.PinY.Value, s2.XForm.PinY.Value))
        {
            diffs.Add($"Shape '{key}' position changed from ({s1.XForm.PinX.Value}, {s1.XForm.PinY.Value}) to ({s2.XForm.PinX.Value}, {s2.XForm.PinY.Value}).");
        }

        // Size
        if (!AreClose(s1.XForm.Width.Value, s2.XForm.Width.Value) ||
            !AreClose(s1.XForm.Height.Value, s2.XForm.Height.Value))
        {
            diffs.Add($"Shape '{key}' size changed from ({s1.XForm.Width.Value} x {s1.XForm.Height.Value}) to ({s2.XForm.Width.Value} x {s2.XForm.Height.Value}).");
        }

        // Text content
        string text1 = s1.Text.Value.Text ?? string.Empty;
        string text2 = s2.Text.Value.Text ?? string.Empty;
        if (!string.Equals(text1, text2, StringComparison.Ordinal))
        {
            diffs.Add($"Shape '{key}' text changed from \"{text1}\" to \"{text2}\".");
        }

        // Line color
        string lineColor1 = s1.Line.LineColor.Value ?? string.Empty;
        string lineColor2 = s2.Line.LineColor.Value ?? string.Empty;
        if (!string.Equals(lineColor1, lineColor2, StringComparison.OrdinalIgnoreCase))
        {
            diffs.Add($"Shape '{key}' line color changed from {lineColor1} to {lineColor2}.");
        }

        // Fill color (foreground)
        string fillColor1 = s1.Fill.FillForegnd.Value ?? string.Empty;
        string fillColor2 = s2.Fill.FillForegnd.Value ?? string.Empty;
        if (!string.Equals(fillColor1, fillColor2, StringComparison.OrdinalIgnoreCase))
        {
            diffs.Add($"Shape '{key}' fill color changed from {fillColor1} to {fillColor2}.");
        }
    }

    // Helper to compare double values with a tolerance to avoid floating‑point noise
    private static bool AreClose(double a, double b, double tolerance = 0.0001)
    {
        return Math.Abs(a - b) <= tolerance;
    }
}
