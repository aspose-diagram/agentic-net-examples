using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the Visio file containing the pages to compare
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Retrieve the two pages by name (adjust names as needed)
                Page pageA = diagram.Pages.GetPage("Page-1");
                Page pageB = diagram.Pages.GetPage("Page-2");

                if (pageA == null || pageB == null)
                {
                    Console.WriteLine("One or both pages not found. Verify page names.");
                    return;
                }

                // Generate diff report
                List<string> diffLines = ComparePages(pageA, pageB);

                // Output report to console
                foreach (string line in diffLines)
                {
                    Console.WriteLine(line);
                }

                // Save report to a text file
                string reportPath = "diff_report.txt";
                File.WriteAllLines(reportPath, diffLines);
                Console.WriteLine($"Diff report saved to: {reportPath}");

                // Dispose diagram resources
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Compares two pages and returns a list of textual differences.
        /// </summary>
        static List<string> ComparePages(Page page1, Page page2)
        {
            var report = new List<string>();
            report.Add($"Comparing Page \"{page1.Name}\" (ID={page1.ID}) with Page \"{page2.Name}\" (ID={page2.ID})");
            report.Add("------------------------------------------------------------");

            // Build lookup dictionaries by universal shape name for quick matching
            var shapesPage1 = new Dictionary<string, Shape>();
            foreach (Shape shape in page1.Shapes)
            {
                if (!string.IsNullOrEmpty(shape.NameU))
                {
                    shapesPage1[shape.NameU] = shape;
                }
            }

            var shapesPage2 = new Dictionary<string, Shape>();
            foreach (Shape shape in page2.Shapes)
            {
                if (!string.IsNullOrEmpty(shape.NameU))
                {
                    shapesPage2[shape.NameU] = shape;
                }
            }

            // Detect removed or changed shapes (present in page1)
            foreach (var kvp in shapesPage1)
            {
                string shapeName = kvp.Key;
                Shape shape1 = kvp.Value;

                if (!shapesPage2.TryGetValue(shapeName, out Shape shape2))
                {
                    report.Add($"Removed Shape: \"{shapeName}\" (ID={shape1.ID})");
                    continue;
                }

                // Compare basic visual properties
                List<string> changes = new List<string>();

                // Position
                if (Math.Abs(shape1.XForm.PinX.Value - shape2.XForm.PinX.Value) > 0.001 ||
                    Math.Abs(shape1.XForm.PinY.Value - shape2.XForm.PinY.Value) > 0.001)
                {
                    changes.Add($"Position changed from ({shape1.XForm.PinX.Value:F3}, {shape1.XForm.PinY.Value:F3}) to ({shape2.XForm.PinX.Value:F3}, {shape2.XForm.PinY.Value:F3})");
                }

                // Size
                if (Math.Abs(shape1.XForm.Width.Value - shape2.XForm.Width.Value) > 0.001 ||
                    Math.Abs(shape1.XForm.Height.Value - shape2.XForm.Height.Value) > 0.001)
                {
                    changes.Add($"Size changed from ({shape1.XForm.Width.Value:F3} x {shape1.XForm.Height.Value:F3}) to ({shape2.XForm.Width.Value:F3} x {shape2.XForm.Height.Value:F3})");
                }

                // Fill color
                if (!string.Equals(shape1.Fill.FillForegnd.Value, shape2.Fill.FillForegnd.Value, StringComparison.OrdinalIgnoreCase))
                {
                    changes.Add($"Fill color changed from \"{shape1.Fill.FillForegnd.Value}\" to \"{shape2.Fill.FillForegnd.Value}\"");
                }

                // Line color
                if (!string.Equals(shape1.Line.LineColor.Value, shape2.Line.LineColor.Value, StringComparison.OrdinalIgnoreCase))
                {
                    changes.Add($"Line color changed from \"{shape1.Line.LineColor.Value}\" to \"{shape2.Line.LineColor.Value}\"");
                }

                // Text content
                string text1 = shape1.Text.Value.Text;
                string text2 = shape2.Text.Value.Text;
                if (!string.Equals(text1, text2, StringComparison.Ordinal))
                {
                    changes.Add($"Text changed from \"{text1}\" to \"{text2}\"");
                }

                if (changes.Count > 0)
                {
                    report.Add($"Modified Shape: \"{shapeName}\" (ID={shape1.ID})");
                    foreach (string change in changes)
                    {
                        report.Add($"  - {change}");
                    }
                }
            }

            // Detect added shapes (present only in page2)
            foreach (var kvp in shapesPage2)
            {
                string shapeName = kvp.Key;
                Shape shape2 = kvp.Value;

                if (!shapesPage1.ContainsKey(shapeName))
                {
                    report.Add($"Added Shape: \"{shapeName}\" (ID={shape2.ID})");
                }
            }

            if (report.Count == 2) // only header lines, no differences found
            {
                report.Add("No visual differences detected between the pages.");
            }

            return report;
        }
    }