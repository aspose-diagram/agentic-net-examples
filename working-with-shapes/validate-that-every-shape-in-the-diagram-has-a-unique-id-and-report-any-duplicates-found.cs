using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Expect the Visio file path as the first argument
                if (args.Length == 0)
                {
                    Console.WriteLine("Please provide the path to the Visio file as a command‑line argument.");
                    return;
                }

                string filePath = args[0];

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Track seen IDs and collect duplicates
                HashSet<long> seenIds = new HashSet<long>();
                List<long> duplicateIds = new List<long>();

                // Iterate over all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        long id = shape.ID;
                        if (!seenIds.Add(id))
                        {
                            duplicateIds.Add(id);
                        }
                    }
                }

                // Report results
                if (duplicateIds.Count == 0)
                {
                    Console.WriteLine("No duplicate shape IDs found.");
                }
                else
                {
                    Console.WriteLine("Duplicate shape IDs detected:");
                    foreach (long dupId in duplicateIds)
                    {
                        Console.WriteLine($"Duplicate ID: {dupId}");
                    }

                    // Optionally raise an error
                    // throw new Exception("Duplicate shape IDs were found in the diagram.");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }