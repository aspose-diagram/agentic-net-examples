using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // List to hold IDs of shapes without any connections
            List<long> isolatedShapeIds = new List<long>();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve IDs of all shapes connected to the current shape (both incoming and outgoing)
                    long[] connected = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                    // If no connections are found, add the shape ID to the list
                    if (connected == null || connected.Length == 0)
                    {
                        isolatedShapeIds.Add(shape.ID);
                    }
                }
            }

            // Output the IDs to the console
            Console.WriteLine("Shapes with no connections:");
            foreach (long id in isolatedShapeIds)
            {
                Console.WriteLine(id);
            }

            // Optionally, write the IDs to a text file for further analysis
            System.IO.File.WriteAllLines("IsolatedShapeIds.txt", isolatedShapeIds.ConvertAll(id => id.ToString()));

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
