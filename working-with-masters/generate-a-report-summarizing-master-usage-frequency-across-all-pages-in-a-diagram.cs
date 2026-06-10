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
                Diagram diagram = new Diagram(diagramPath);

                // Dictionary to hold master name and its usage count
                Dictionary<string, int> masterUsage = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has an associated master
                        if (shape.Master != null && !string.IsNullOrEmpty(shape.Master.Name))
                        {
                            string masterName = shape.Master.Name;

                            if (masterUsage.ContainsKey(masterName))
                            {
                                masterUsage[masterName]++;
                            }
                            else
                            {
                                masterUsage[masterName] = 1;
                            }
                        }
                    }
                }

                // Output the report
                Console.WriteLine("Master Usage Frequency Report:");
                Console.WriteLine("--------------------------------");
                foreach (var entry in masterUsage)
                {
                    Console.WriteLine($"Master \"{entry.Key}\": {entry.Value} occurrence(s)");
                }

                // Keep console window open if needed
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }