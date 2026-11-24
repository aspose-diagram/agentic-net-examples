using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page
            Page page = diagram.Pages[0];

            // Dictionary to map shape IDs to their custom connection point collections
            Dictionary<long, List<Connection>> shapeConnectionMap = new Dictionary<long, List<Connection>>();

            // Iterate over all shapes on the page
            for (int i = 0; i < page.Shapes.Count; i++)
            {
                Shape shape = page.Shapes[i];

                // Add a custom connection point to the shape (center-bottom)
                Connection customConn = new Connection();
                // Position the connection point at the middle of the bottom edge
                customConn.X.Ufe.F = "Width*0.5";
                customConn.Y.Ufe.F = "Height*0";
                shape.Connections.Add(customConn);

                // Capture the shape's ID
                long shapeId = shape.ID;

                // Store the shape's connections in the dictionary
                List<Connection> connections = new List<Connection>();
                foreach (Connection conn in shape.Connections)
                {
                    connections.Add(conn);
                }
                shapeConnectionMap[shapeId] = connections;
            }

            // Example batch update: shift all custom connection points 0.2 inches to the right
            foreach (KeyValuePair<long, List<Connection>> kvp in shapeConnectionMap)
            {
                // Retrieve the shape by ID
                Shape shape = page.Shapes.GetShape(kvp.Key);
                if (shape == null) continue;

                foreach (Connection conn in kvp.Value)
                {
                    // Update the X formula to add an offset
                    conn.X.Ufe.F = $"({conn.X.Ufe.F})+0.2";
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}