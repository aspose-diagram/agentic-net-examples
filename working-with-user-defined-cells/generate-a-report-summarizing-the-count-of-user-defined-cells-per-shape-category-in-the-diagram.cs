using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Determine the diagram file path (first argument or default)
                string diagramPath = args.Length > 0 ? args[0] : "sample.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Dictionary to hold the total user-defined cell count per shape category
                var categoryUserCellCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Determine the shape category (master name or fallback)
                        string category = shape.Master != null ? shape.Master.Name : "NoMaster";

                        // Count user-defined cells for this shape
                        int userCellCount = shape.Users != null ? shape.Users.Count : 0;

                        // Accumulate counts only if there are user-defined cells
                        if (userCellCount > 0)
                        {
                            if (!categoryUserCellCounts.ContainsKey(category))
                            {
                                categoryUserCellCounts[category] = 0;
                            }
                            categoryUserCellCounts[category] += userCellCount;
                        }
                    }
                }

                // Output the report
                Console.WriteLine("User-defined cells count per shape category:");
                foreach (var kvp in categoryUserCellCounts)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }