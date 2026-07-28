using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.Diagram;

namespace DiagramPageDiff
{
    // Holds simplified shape information for comparison
    class ShapeInfo
    {
        public long Id { get; set; }
        public string MasterName { get; set; }
        public double PinX { get; set; }
        public double PinY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Text { get; set; }
        public string LineColor { get; set; }
        public string FillColor { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: diagram file path, first page name, second page name
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: DiagramPageDiff <diagramPath> <pageName1> <pageName2>");
                return;
            }

            string diagramPath = args[0];
            // Guard: ensure the diagram file exists
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                return;
            }

            string pageName1 = args[1];
            string pageName2 = args[2];

            try
            {
                // Load the diagram inside a using block to ensure disposal
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Retrieve the two pages by name
                    Page page1 = diagram.Pages.GetPage(pageName1);
                    Page page2 = diagram.Pages.GetPage(pageName2);

                    if (page1 == null || page2 == null)
                    {
                        Console.WriteLine("One or both pages not found in the diagram.");
                        return;
                    }

                    // Extract shape information from each page
                    List<ShapeInfo> shapesPage1 = ExtractShapes(page1);
                    List<ShapeInfo> shapesPage2 = ExtractShapes(page2);

                    // Generate diff report
                    StringBuilder report = new StringBuilder();
                    report.AppendLine($"Diff Report for pages \"{pageName1}\" vs \"{pageName2}\"");
                    report.AppendLine($"Generated on {DateTime.Now}");
                    report.AppendLine();

                    // Compare shape counts
                    if (shapesPage1.Count != shapesPage2.Count)
                    {
                        report.AppendLine($"Shape count differs: {pageName1} has {shapesPage1.Count}, {pageName2} has {shapesPage2.Count}");
                    }

                    // Compare shapes by index (assuming similar ordering)
                    int maxCount = Math.Max(shapesPage1.Count, shapesPage2.Count);
                    for (int i = 0; i < maxCount; i++)
                    {
                        if (i >= shapesPage1.Count)
                        {
                            report.AppendLine($"Extra shape in {pageName2}: ID {shapesPage2[i].Id}");
                            continue;
                        }
                        if (i >= shapesPage2.Count)
                        {
                            report.AppendLine($"Extra shape in {pageName1}: ID {shapesPage1[i].Id}");
                            continue;
                        }

                        ShapeInfo s1 = shapesPage1[i];
                        ShapeInfo s2 = shapesPage2[i];

                        // Compare each property and log differences
                        if (s1.MasterName != s2.MasterName)
                            report.AppendLine($"Shape ID {s1.Id}: Master changed from \"{s1.MasterName}\" to \"{s2.MasterName}\"");

                        if (!AreClose(s1.PinX, s2.PinX) || !AreClose(s1.PinY, s2.PinY))
                            report.AppendLine($"Shape ID {s1.Id}: Position changed from ({s1.PinX:F3}, {s1.PinY:F3}) to ({s2.PinX:F3}, {s2.PinY:F3})");

                        if (!AreClose(s1.Width, s2.Width) || !AreClose(s1.Height, s2.Height))
                            report.AppendLine($"Shape ID {s1.Id}: Size changed from ({s1.Width:F3} x {s1.Height:F3}) to ({s2.Width:F3} x {s2.Height:F3})");

                        if (s1.Text != s2.Text)
                            report.AppendLine($"Shape ID {s1.Id}: Text changed from \"{s1.Text}\" to \"{s2.Text}\"");

                        if (s1.LineColor != s2.LineColor)
                            report.AppendLine($"Shape ID {s1.Id}: Line color changed from \"{s1.LineColor}\" to \"{s2.LineColor}\"");

                        if (s1.FillColor != s2.FillColor)
                            report.AppendLine($"Shape ID {s1.Id}: Fill color changed from \"{s1.FillColor}\" to \"{s2.FillColor}\"");
                    }

                    // Output report to console
                    Console.WriteLine(report.ToString());

                    // Also write report to a text file in the same directory as the diagram
                    string reportPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "diff_report.txt");
                    try
                    {
                        File.WriteAllText(reportPath, report.ToString());
                        Console.WriteLine($"Diff report saved to: {reportPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to write report file: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Capture any Aspose.Diagram related errors
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }

        // Extracts a list of ShapeInfo objects from a given page, ignoring deleted shapes
        private static List<ShapeInfo> ExtractShapes(Page page)
        {
            List<ShapeInfo> list = new List<ShapeInfo>();
            // Iterate over all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Skip shapes marked as deleted
                if (shape.Del == BOOL.True) continue;

                // Build ShapeInfo from the shape's properties
                ShapeInfo info = new ShapeInfo
                {
                    Id = shape.ID,
                    MasterName = shape.Master?.Name ?? "None",
                    PinX = shape.XForm.PinX.Value,
                    PinY = shape.XForm.PinY.Value,
                    Width = shape.XForm.Width.Value,
                    Height = shape.XForm.Height.Value,
                    Text = shape.Text.Value.Text, // plain concatenated text
                    LineColor = shape.Line.LineColor.Value,
                    FillColor = shape.Fill.FillForegnd.Value
                };
                list.Add(info);
            }
            return list;
        }

        // Helper to compare double values within a tolerance
        private static bool AreClose(double a, double b, double tolerance = 0.001)
        {
            return Math.Abs(a - b) <= tolerance;
        }
    }
}