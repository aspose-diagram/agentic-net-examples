using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect the Visio file path as the first argument.
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DiagramGlueInspector <input-visio-file>");
                return;
            }

            string inputPath = args[0];

            // Load the diagram from the specified file.
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"Page: {page.NameU} (ID: {page.ID})");

                // Build a lookup of shape ID to Shape for quick access.
                var shapeLookup = new Dictionary<long, Shape>();
                foreach (Shape shp in page.Shapes)
                {
                    shapeLookup[shp.ID] = shp;
                }

                // Iterate through each shape to find those with gluing enabled.
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape allows dynamic glue.
                    if (shape.Misc.GlueType != null &&
                        shape.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue)
                    {
                        Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}");

                        // Find connectors attached to this shape via the Connects collection.
                        var attachedConnectors = new List<long>();

                        foreach (Connect conn in page.Connects)
                        {
                            // If the shape is the source of the connection.
                            if (conn.FromSheet == shape.ID && IsConnector(shapeLookup, conn.ToSheet))
                            {
                                attachedConnectors.Add(conn.ToSheet);
                            }
                            // If the shape is the target of the connection.
                            else if (conn.ToSheet == shape.ID && IsConnector(shapeLookup, conn.FromSheet))
                            {
                                attachedConnectors.Add(conn.FromSheet);
                            }
                        }

                        // Output attached connector information.
                        if (attachedConnectors.Count == 0)
                        {
                            Console.WriteLine("\tNo attached connectors found.");
                        }
                        else
                        {
                            foreach (long connectorId in attachedConnectors)
                            {
                                if (shapeLookup.TryGetValue(connectorId, out Shape connectorShape))
                                {
                                    Console.WriteLine($"\tConnector ID: {connectorId}, NameU: {connectorShape.NameU}");
                                }
                                else
                                {
                                    Console.WriteLine($"\tConnector ID: {connectorId} (shape not found)");
                                }
                            }
                        }
                    }
                }
            }
        }

        // Helper method to determine if a shape ID corresponds to a connector (1‑D shape).
        private static bool IsConnector(Dictionary<long, Shape> lookup, long shapeId)
        {
            if (lookup.TryGetValue(shapeId, out Shape shp))
            {
                // The OneD property is a native bool indicating a connector.
                return shp.OneD;
            }
            return false;
        }
    }