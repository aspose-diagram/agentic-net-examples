using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio diagram file (replace with actual path)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Dictionary to hold the total count of user-defined cells per shape category (master name)
                Dictionary<string, int> categoryUserCellCounts = new Dictionary<string, int>();

                // Iterate through all pages explicitly typing the iterator
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Determine the shape category based on its master name; fallback if master is null
                        string category = shape.Master != null ? shape.Master.Name : "NoMaster";

                        // Count user-defined cells for this shape
                        int userCellCount = 0;
                        foreach (User userCell in shape.Users)
                        {
                            // Each entry in shape.Users represents one user-defined cell
                            userCellCount++;
                        }

                        // Accumulate the count per category
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

                // Output the report
                Console.WriteLine("User-Defined Cell Count per Shape Category:");
                foreach (KeyValuePair<string, int> entry in categoryUserCellCounts)
                {
                    Console.WriteLine($"Category: {entry.Key}, Total User-Defined Cells: {entry.Value}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }