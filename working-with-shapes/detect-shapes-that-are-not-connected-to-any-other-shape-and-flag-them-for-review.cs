using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "diagram.vsdx";
        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Collect IDs of all shapes that participate in any connection.
                HashSet<long> connectedShapeIds = new HashSet<long>();
                // The Connects collection holds connector relationships between shapes.
                foreach (Connect conn in page.Connects)
                {
                    // Add both source and target shape IDs to the set.
                    connectedShapeIds.Add(conn.FromSheet);
                    connectedShapeIds.Add(conn.ToSheet);
                }

                // Examine each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // If the shape's ID is not present in the connection set, it is isolated.
                    if (!connectedShapeIds.Contains(shape.ID))
                    {
                        // Output details of the unconnected shape for review.
                        Console.WriteLine($"Unconnected shape detected - Page: '{page.NameU}', ID: {shape.ID}, NameU: {shape.NameU}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during diagram processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}