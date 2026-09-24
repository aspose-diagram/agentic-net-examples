using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file (adjust as needed)
            string filePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Collect IDs of shapes that are part of any connection
            HashSet<long> connectedShapeIds = new HashSet<long>();

            foreach (Page page in diagram.Pages)
            {
                // Iterate all connections on the page
                foreach (Connect connection in page.Connects)
                {
                    // FromSheet and ToSheet hold the shape IDs involved in the connection
                    connectedShapeIds.Add(connection.FromSheet);
                    connectedShapeIds.Add(connection.ToSheet);
                }
            }

            // List shapes with no connections
            Console.WriteLine("Shapes with no connections (IDs):");
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    if (!connectedShapeIds.Contains(shape.ID))
                    {
                        Console.WriteLine(shape.ID);
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
