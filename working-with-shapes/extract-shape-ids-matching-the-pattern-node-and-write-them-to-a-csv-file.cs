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

            // Load the Visio diagram
            var diagram = new Diagram("input.vsdx");

            var nodeIds = new List<long>();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape name matches the pattern "Node_*"
                    if (!string.IsNullOrEmpty(shape.Name) && shape.Name.StartsWith("Node_"))
                    {
                        nodeIds.Add(shape.ID);
                    }
                }
            }

            // Write the collected shape IDs to a CSV file
            using (var writer = new StreamWriter("node_ids.csv"))
            {
                // Header (optional)
                writer.WriteLine("ShapeID");
                foreach (long id in nodeIds)
                {
                    writer.WriteLine(id);
                }
            }

            // If you need to save any changes to the diagram, uncomment the line below
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
