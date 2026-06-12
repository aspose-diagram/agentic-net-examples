using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (change as needed)
            string visioFilePath = "input.vsdx";
            // Output CSV file path
            string csvFilePath = "NodeShapeIds.csv";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioFilePath);

            // List to hold matching shape IDs
            List<long> nodeShapeIds = new List<long>();

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Shape.NameU contains the universal name of the shape
                    if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU.StartsWith("Node_"))
                    {
                        nodeShapeIds.Add(shape.ID);
                    }
                }
            }

            // Write the collected IDs to a CSV file (one ID per line)
            using (StreamWriter writer = new StreamWriter(csvFilePath))
            {
                // Optional header
                writer.WriteLine("ShapeID");
                foreach (long id in nodeShapeIds)
                {
                    writer.WriteLine(id);
                }
            }

            Console.WriteLine($"Extracted {nodeShapeIds.Count} shape IDs to '{csvFilePath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
