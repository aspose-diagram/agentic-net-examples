using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (first argument or default)
                string filePath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Dictionary to hold the aggregated user-defined cell counts per shape category
                Dictionary<string, int> categoryUserCellCounts = new Dictionary<string, int>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Determine the shape category (master name if available, otherwise shape type)
                        string category;
                        if (shape.Master != null && !string.IsNullOrEmpty(shape.Master.Name))
                        {
                            category = shape.Master.Name;
                        }
                        else
                        {
                            // Shape.Type is a TypeValue enum; use its string representation as fallback
                            category = shape.Type.ToString();
                        }

                        // Count user-defined cells for this shape
                        int userCellCount = 0;
                        foreach (User userCell in shape.Users)
                        {
                            userCellCount++;
                        }

                        // Aggregate the count into the dictionary
                        if (categoryUserCellCounts.ContainsKey(category))
                        {
                            categoryUserCellCounts[category] += userCellCount;
                        }
                        else
                        {
                            categoryUserCellCounts[category] = userCellCount;
                        }
                    }
                }

                // Output the summary report
                Console.WriteLine("User-defined cell count per shape category:");
                foreach (KeyValuePair<string, int> entry in categoryUserCellCounts)
                {
                    Console.WriteLine($"{entry.Key}: {entry.Value}");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }