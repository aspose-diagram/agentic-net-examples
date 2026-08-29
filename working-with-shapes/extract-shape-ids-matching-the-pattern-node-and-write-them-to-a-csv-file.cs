using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string visioPath = "input.vsdx";
            // Path to the CSV output file
            string csvPath = "node_ids.csv";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Collect IDs of shapes whose NameU matches "Node_*"
            List<long> nodeIds = new List<long>();

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // NameU holds the universal name of the shape
                    if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU.StartsWith("Node_"))
                    {
                        nodeIds.Add(shape.ID);
                    }
                }
            }

            // Write the collected IDs to a CSV file (one ID per line)
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                // Optional header
                writer.WriteLine("ShapeID");
                foreach (long id in nodeIds)
                {
                    writer.WriteLine(id);
                }
            }

            Console.WriteLine($"Extracted {nodeIds.Count} shape IDs to '{csvPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
