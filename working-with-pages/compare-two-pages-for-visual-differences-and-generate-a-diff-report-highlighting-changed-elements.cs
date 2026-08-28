using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: source diagram path and target diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: DiffPages <sourceDiagram> <targetDiagram> [reportPath]");
            return;
        }

        // Assign input file paths.
        string sourcePath = args[0];
        // Guard: ensure source file exists.
        if (!File.Exists(sourcePath)) { Console.Error.WriteLine($"File not found: {sourcePath}"); return; }

        string targetPath = args[1];
        // Guard: ensure target file exists.
        if (!File.Exists(targetPath)) { Console.Error.WriteLine($"File not found: {targetPath}"); return; }

        // Optional report output path.
        string reportPath = args.Length >= 3 ? args[2] : null;
        if (reportPath != null && string.IsNullOrWhiteSpace(reportPath))
        {
            Console.Error.WriteLine("Report path is empty.");
            return;
        }

        try
        {
            // Load source diagram.
            Diagram sourceDiagram = new Diagram(sourcePath);
            // Load target diagram.
            Diagram targetDiagram = new Diagram(targetPath);

            // Retrieve the first page from each diagram (index 0).
            Page sourcePage = sourceDiagram.Pages[0];
            Page targetPage = targetDiagram.Pages[0];

            // Build dictionaries keyed by shape universal name (NameU) for quick lookup.
            var sourceShapes = new Dictionary<string, Shape>(StringComparer.OrdinalIgnoreCase);
            foreach (Shape s in sourcePage.Shapes)
            {
                // Use NameU as key; if empty, fallback to ID string.
                string key = !string.IsNullOrEmpty(s.NameU) ? s.NameU : s.ID.ToString();
                sourceShapes[key] = s;
            }

            var targetShapes = new Dictionary<string, Shape>(StringComparer.OrdinalIgnoreCase);
            foreach (Shape s in targetPage.Shapes)
            {
                string key = !string.IsNullOrEmpty(s.NameU) ? s.NameU : s.ID.ToString();
                targetShapes[key] = s;
            }

            // Prepare a StringBuilder for the diff report.
            StringBuilder report = new StringBuilder();

            // Detect removed and modified shapes.
            foreach (var kvp in sourceShapes)
            {
                string name = kvp.Key;
                Shape srcShape = kvp.Value;

                if (!targetShapes.TryGetValue(name, out Shape tgtShape))
                {
                    // Shape exists in source but not in target → removed.
                    report.AppendLine($"Removed shape: {name}");
                    continue;
                }

                // Compare key visual properties.
                List<string> changes = new List<string>();

                // Position comparison (PinX, PinY).
                double srcPinX = srcShape.XForm.PinX.Value;
                double tgtPinX = tgtShape.XForm.PinX.Value;
                if (Math.Abs(srcPinX - tgtPinX) > 0.001) changes.Add($"PinX: {srcPinX:F3} → {tgtPinX:F3}");

                double srcPinY = srcShape.XForm.PinY.Value;
                double tgtPinY = tgtShape.XForm.PinY.Value;
                if (Math.Abs(srcPinY - tgtPinY) > 0.001) changes.Add($"PinY: {srcPinY:F3} → {tgtPinY:F3}");

                // Size comparison (Width, Height).
                double srcWidth = srcShape.XForm.Width.Value;
                double tgtWidth = tgtShape.XForm.Width.Value;
                if (Math.Abs(srcWidth - tgtWidth) > 0.001) changes.Add($"Width: {srcWidth:F3} → {tgtWidth:F3}");

                double srcHeight = srcShape.XForm.Height.Value;
                double tgtHeight = tgtShape.XForm.Height.Value;
                if (Math.Abs(srcHeight - tgtHeight) > 0.001) changes.Add($"Height: {srcHeight:F3} → {tgtHeight:F3}");

                // Text comparison.
                string srcText = srcShape.Text?.Value?.Text ?? string.Empty;
                string tgtText = tgtShape.Text?.Value?.Text ?? string.Empty;
                if (!srcText.Equals(tgtText, StringComparison.Ordinal))
                    changes.Add($"Text: \"{srcText}\" → \"{tgtText}\"");

                // Line color comparison.
                string srcLineColor = srcShape.Line?.LineColor?.Value ?? string.Empty;
                string tgtLineColor = tgtShape.Line?.LineColor?.Value ?? string.Empty;
                if (!srcLineColor.Equals(tgtLineColor, StringComparison.OrdinalIgnoreCase))
                    changes.Add($"LineColor: {srcLineColor} → {tgtLineColor}");

                // Fill color comparison.
                string srcFillColor = srcShape.Fill?.FillForegnd?.Value ?? string.Empty;
                string tgtFillColor = tgtShape.Fill?.FillForegnd?.Value ?? string.Empty;
                if (!srcFillColor.Equals(tgtFillColor, StringComparison.OrdinalIgnoreCase))
                    changes.Add($"FillColor: {srcFillColor} → {tgtFillColor}");

                // If any differences were found, record them.
                if (changes.Count > 0)
                {
                    report.AppendLine($"Modified shape: {name}");
                    foreach (string change in changes)
                        report.AppendLine($"  - {change}");
                }
            }

            // Detect added shapes (present only in target).
            foreach (var kvp in targetShapes)
            {
                string name = kvp.Key;
                if (!sourceShapes.ContainsKey(name))
                {
                    report.AppendLine($"Added shape: {name}");
                }
            }

            // Output the diff report to console.
            Console.WriteLine("=== Visual Diff Report ===");
            Console.WriteLine(report.ToString());

            // If a report file path was provided, write the report to that file.
            if (!string.IsNullOrEmpty(reportPath))
            {
                try
                {
                    File.WriteAllText(reportPath, report.ToString());
                    Console.WriteLine($"Report written to: {reportPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to write report file: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}