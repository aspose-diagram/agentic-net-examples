using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Verify that a diagram file path is provided
                if (args.Length == 0)
                {
                    Console.WriteLine("Usage: UserDefinedCellReport <diagram-file-path>");
                    return;
                }

                string diagramPath = args[0];

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Dictionary to hold the total count of user‑defined cells per shape category
                Dictionary<string, int> categoryUserCellCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine the shape category (master name). Use "Unknown" if master is missing.
                        string category = shape.Master != null ? shape.Master.Name : "Unknown";

                        // Count user‑defined cells for this shape
                        int userCellCount = shape.Users != null ? shape.Users.Count : 0;

                        if (userCellCount > 0)
                        {
                            if (categoryUserCellCounts.ContainsKey(category))
                                categoryUserCellCounts[category] += userCellCount;
                            else
                                categoryUserCellCounts[category] = userCellCount;
                        }
                    }
                }

                // Output the report
                Console.WriteLine("User‑Defined Cell Count per Shape Category:");
                foreach (KeyValuePair<string, int> kvp in categoryUserCellCounts)
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