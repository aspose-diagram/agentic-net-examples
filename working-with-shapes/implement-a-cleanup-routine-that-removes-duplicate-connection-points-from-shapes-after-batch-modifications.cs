using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: DiagramCleanup <inputFilePath> <outputFilePath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes without connection points
                    if (shape.Connections == null || shape.Connections.Count <= 1)
                        continue;

                    // Track unique connection point definitions using a hash set
                    HashSet<string> uniquePoints = new HashSet<string>();
                    List<int> indicesToRemove = new List<int>();

                    // Examine each connection point
                    for (int i = 0; i < shape.Connections.Count; i++)
                    {
                        var conn = shape.Connections[i];
                        // Build a key based on the X and Y formulas (Ufe.F)
                        string key = $"{conn.X.Ufe.F}|{conn.Y.Ufe.F}";

                        if (uniquePoints.Contains(key))
                        {
                            // Duplicate found – mark this index for removal
                            indicesToRemove.Add(i);
                        }
                        else
                        {
                            uniquePoints.Add(key);
                        }
                    }

                    // Remove duplicates in reverse order to keep indices valid
                    for (int i = indicesToRemove.Count - 1; i >= 0; i--)
                    {
                        int removeIndex = indicesToRemove[i];
                        shape.Connections.RemoveAt(removeIndex);
                    }
                }
            }

            // Save the cleaned diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved after cleanup to: {outputPath}");
        }
    }