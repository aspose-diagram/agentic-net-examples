using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Dictionary to hold master name and its usage count
                    Dictionary<string, int> masterUsage = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                    // Iterate through all pages and shapes to count master usage
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (shape.Master != null)
                            {
                                string masterName = shape.Master.Name;
                                if (masterUsage.ContainsKey(masterName))
                                    masterUsage[masterName]++;
                                else
                                    masterUsage[masterName] = 1;
                            }
                        }
                    }

                    // Ensure all masters are represented, even if not used
                    foreach (Master master in diagram.Masters)
                    {
                        if (!masterUsage.ContainsKey(master.Name))
                            masterUsage[master.Name] = 0;
                    }

                    // Output the report
                    Console.WriteLine("Master Usage Frequency Report:");
                    foreach (var entry in masterUsage)
                    {
                        Console.WriteLine($"{entry.Key}: {entry.Value}");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }