using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting 5 arguments: sourceVisioPath, targetVisioPath, sourcePageIndex, targetPageIndex, outputReportPath
        if (args.Length != 5)
        {
            Console.Error.WriteLine("Usage: Program <sourceVisioPath> <targetVisioPath> <sourcePageIndex> <targetPageIndex> <outputReportPath>");
            return;
        }

        string sourcePath = args[0];
        string targetPath = args[1];
        string sourcePageIndexStr = args[2];
        string targetPageIndexStr = args[3];
        string reportPath = args[4];

        // Guard for source file existence
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Guard for target file existence
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        // Parse page indexes
        if (!int.TryParse(sourcePageIndexStr, out int sourcePageIndex) || sourcePageIndex < 0)
        {
            Console.Error.WriteLine("Invalid source page index.");
            return;
        }

        if (!int.TryParse(targetPageIndexStr, out int targetPageIndex) || targetPageIndex < 0)
        {
            Console.Error.WriteLine("Invalid target page index.");
            return;
        }

        try
        {
            // Load source diagram
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                // Load target diagram
                using (Diagram targetDiagram = new Diagram(targetPath))
                {
                    // Validate page indexes
                    if (sourcePageIndex >= sourceDiagram.Pages.Count)
                    {
                        Console.Error.WriteLine("Source page index out of range.");
                        return;
                    }

                    if (targetPageIndex >= targetDiagram.Pages.Count)
                    {
                        Console.Error.WriteLine("Target page index out of range.");
                        return;
                    }

                    // Retrieve pages
                    Page sourcePage = sourceDiagram.Pages[sourcePageIndex];
                    Page targetPage = targetDiagram.Pages[targetPageIndex];

                    // Build dictionaries of shapes keyed by shape ID for quick lookup
                    Dictionary<long, Shape> sourceShapes = new Dictionary<long, Shape>();
                    foreach (Shape shape in sourcePage.Shapes)
                    {
                        sourceShapes[shape.ID] = shape;
                    }

                    Dictionary<long, Shape> targetShapes = new Dictionary<long, Shape>();
                    foreach (Shape shape in targetPage.Shapes)
                    {
                        targetShapes[shape.ID] = shape;
                    }

                    List<string> reportLines = new List<string>();
                    reportLines.Add($"Comparison Report for Source Page {sourcePageIndex} and Target Page {targetPageIndex}");
                    reportLines.Add($"Generated on {DateTime.Now}");
                    reportLines.Add(string.Empty);

                    // Detect removed or modified shapes
                    foreach (var kvp in sourceShapes)
                    {
                        long shapeId = kvp.Key;
                        Shape srcShape = kvp.Value;

                        if (!targetShapes.TryGetValue(shapeId, out Shape tgtShape))
                        {
                            // Shape removed in target
                            string masterName = srcShape.Master?.Name ?? "N/A";
                            reportLines.Add($"Shape Removed: ID={shapeId}, Master={masterName}");
                            continue;
                        }

                        // Compare position
                        double srcPinX = srcShape.XForm.PinX.Value;
                        double srcPinY = srcShape.XForm.PinY.Value;
                        double tgtPinX = tgtShape.XForm.PinX.Value;
                        double tgtPinY = tgtShape.XForm.PinY.Value;

                        if (Math.Abs(srcPinX - tgtPinX) > 0.001 || Math.Abs(srcPinY - tgtPinY) > 0.001)
                        {
                            reportLines.Add($"Shape Moved: ID={shapeId}, From ({srcPinX:F3}, {srcPinY:F3}) To ({tgtPinX:F3}, {tgtPinY:F3})");
                        }

                        // Compare size
                        double srcWidth = srcShape.XForm.Width.Value;
                        double srcHeight = srcShape.XForm.Height.Value;
                        double tgtWidth = tgtShape.XForm.Width.Value;
                        double tgtHeight = tgtShape.XForm.Height.Value;

                        if (Math.Abs(srcWidth - tgtWidth) > 0.001 || Math.Abs(srcHeight - tgtHeight) > 0.001)
                        {
                            reportLines.Add($"Shape Resized: ID={shapeId}, From ({srcWidth:F3} x {srcHeight:F3}) To ({tgtWidth:F3} x {tgtHeight:F3})");
                        }

                        // Compare text
                        string srcText = srcShape.Text?.Value?.Text ?? string.Empty;
                        string tgtText = tgtShape.Text?.Value?.Text ?? string.Empty;

                        if (!string.Equals(srcText, tgtText, StringComparison.Ordinal))
                        {
                            reportLines.Add($"Shape Text Changed: ID={shapeId}, From \"{srcText}\" To \"{tgtText}\"");
                        }

                        // Compare master (shape type)
                        string srcMaster = srcShape.Master?.Name ?? "N/A";
                        string tgtMaster = tgtShape.Master?.Name ?? "N/A";

                        if (!string.Equals(srcMaster, tgtMaster, StringComparison.Ordinal))
                        {
                            reportLines.Add($"Shape Master Changed: ID={shapeId}, From \"{srcMaster}\" To \"{tgtMaster}\"");
                        }
                    }

                    // Detect added shapes in target
                    foreach (var kvp in targetShapes)
                    {
                        long shapeId = kvp.Key;
                        if (!sourceShapes.ContainsKey(shapeId))
                        {
                            Shape addedShape = kvp.Value;
                            string masterName = addedShape.Master?.Name ?? "N/A";
                            reportLines.Add($"Shape Added: ID={shapeId}, Master={masterName}");
                        }
                    }

                    // Ensure output directory exists
                    string reportDir = Path.GetDirectoryName(reportPath);
                    if (!string.IsNullOrEmpty(reportDir) && !Directory.Exists(reportDir))
                    {
                        Directory.CreateDirectory(reportDir);
                    }

                    // Write report to file
                    File.WriteAllLines(reportPath, reportLines);
                    Console.WriteLine($"Diff report generated at: {reportPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}