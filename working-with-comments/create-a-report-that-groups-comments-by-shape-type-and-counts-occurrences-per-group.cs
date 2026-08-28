using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Path to the Visio diagram file
            const string diagramPath = "input.vsdx";

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Dictionary to hold comment counts grouped by shape type (master name or shape type)
            var commentCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all annotations (comments) on the page
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    string groupKey;

                    // If the comment is attached to a shape, retrieve the shape
                    if (annotation.ShapeID != 0)
                    {
                        Shape shape = page.Shapes.GetShape(annotation.ShapeID);
                        if (shape != null)
                        {
                            // Prefer the master name if available; otherwise use the shape's Type enum
                            if (shape.Master != null && !string.IsNullOrEmpty(shape.Master.Name))
                            {
                                groupKey = shape.Master.Name;
                            }
                            else
                            {
                                groupKey = shape.Type.ToString();
                            }
                        }
                        else
                        {
                            groupKey = "OrphanShapeComment";
                        }
                    }
                    else
                    {
                        // Comment not attached to any shape (page-level comment)
                        groupKey = "PageComment";
                    }

                    // Increment the count for the determined group
                    if (commentCounts.ContainsKey(groupKey))
                    {
                        commentCounts[groupKey]++;
                    }
                    else
                    {
                        commentCounts[groupKey] = 1;
                    }
                }
            }

            // Prepare report lines
            var reportLines = new List<string>
            {
                "Comment Count Report (Grouped by Shape Type)",
                "-------------------------------------------"
            };

            foreach (var kvp in commentCounts)
            {
                string line = $"{kvp.Key}: {kvp.Value}";
                Console.WriteLine(line);
                reportLines.Add(line);
            }

            // Save the report to a text file
            const string reportPath = "CommentReport.txt";
            try
            {
                File.WriteAllLines(reportPath, reportLines);
                Console.WriteLine($"Report saved to '{reportPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save report: {ex.Message}");
            }
        }
    }