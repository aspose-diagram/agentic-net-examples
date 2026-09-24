using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path – adjust as needed.
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path.
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file.
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (index 0).
            Page page = diagram.Pages[0];

            // Collect all connector shapes (1‑D shapes) on the page.
            List<Shape> connectorShapes = new List<Shape>();
            foreach (Shape shape in page.Shapes)
            {
                // Connector shapes are identified by the OneD boolean flag.
                if (shape.OneD)
                {
                    connectorShapes.Add(shape);
                }
            }

            // If there are connectors, group them into a single group shape.
            if (connectorShapes.Count > 0)
            {
                // The Group method returns the newly created group shape.
                Shape groupShape = page.Shapes.Group(connectorShapes.ToArray());

                // Optionally set a name for the group for easier identification.
                groupShape.NameU = "AllConnectorsGroup";
            }
            else
            {
                Console.WriteLine("No connector shapes found to group.");
            }

            // Save the modified diagram to the output file using the Vsdx format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Verify that connectors remain correctly connected after saving.
        try
        {
            // Reload the saved diagram.
            Diagram savedDiagram = new Diagram(outputPath);
            Page savedPage = savedDiagram.Pages[0];

            // Build a lookup of connector IDs for quick access.
            HashSet<long> connectorIds = new HashSet<long>();
            foreach (Shape shape in savedPage.Shapes)
            {
                if (shape.OneD)
                {
                    connectorIds.Add(shape.ID);
                }
            }

            // Track connectors that have at least one connection.
            HashSet<long> connectedConnectorIds = new HashSet<long>();
            foreach (Connect conn in savedPage.Connects)
            {
                // FromSheet or ToSheet may reference a connector shape.
                if (connectorIds.Contains(conn.FromSheet))
                {
                    connectedConnectorIds.Add(conn.FromSheet);
                }
                if (connectorIds.Contains(conn.ToSheet))
                {
                    connectedConnectorIds.Add(conn.ToSheet);
                }
            }

            // Determine if any connector lost its connections.
            bool allConnected = true;
            foreach (long id in connectorIds)
            {
                if (!connectedConnectorIds.Contains(id))
                {
                    Console.Error.WriteLine($"Connector shape ID {id} has no connections after save.");
                    allConnected = false;
                }
            }

            // Report verification result.
            if (allConnected)
            {
                Console.WriteLine("All connector shapes remain connected after saving.");
            }
            else
            {
                Console.Error.WriteLine("Some connector shapes lost their connections after saving.");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during verification.
            Console.Error.WriteLine($"Error verifying saved diagram: {ex.Message}");
        }
    }
}