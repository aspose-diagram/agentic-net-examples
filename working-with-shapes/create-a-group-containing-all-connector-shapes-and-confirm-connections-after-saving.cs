using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Process each page: group all connector (1-D) shapes
                foreach (Page page in diagram.Pages)
                {
                    List<Shape> connectorShapes = new List<Shape>();

                    // Collect connector shapes (OneD == true)
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.OneD)
                        {
                            connectorShapes.Add(shape);
                        }
                    }

                    if (connectorShapes.Count > 0)
                    {
                        // Group the connector shapes
                        Shape[] connectorArray = connectorShapes.ToArray();
                        Shape groupShape = page.Shapes.Group(connectorArray);
                        Console.WriteLine($"Page '{page.Name}' - grouped {connectorShapes.Count} connectors into group ID {groupShape.ID}.");
                    }
                    else
                    {
                        Console.WriteLine($"Page '{page.Name}' - no connector shapes found.");
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

                // Reload the saved diagram to verify connections
                Diagram reloadedDiagram = new Diagram(outputPath);

                // Verify that each connector still has at least one connection
                foreach (Page page in reloadedDiagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.OneD)
                        {
                            long connectorId = shape.ID;
                            bool hasConnection = false;

                            // Iterate through the Connect collection to find a link involving this connector
                            foreach (Connect conn in page.Connects)
                            {
                                if (conn.FromSheet == connectorId || conn.ToSheet == connectorId)
                                {
                                    hasConnection = true;
                                    break;
                                }
                            }

                            if (!hasConnection)
                            {
                                throw new Exception($"Connector shape ID {connectorId} on page '{page.Name}' has no connections after saving.");
                            }
                        }
                    }
                }

                Console.WriteLine("All connector shapes have valid connections after saving.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }