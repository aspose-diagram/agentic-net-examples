using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (can be passed as a command‑line argument)
                string filePath = "input.vsdx";
                if (args.Length > 0)
                {
                    filePath = args[0];
                }

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.NameU}");

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape has dynamic gluing enabled
                        if (shape.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue)
                        {
                            Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}");

                            // Collect IDs of connectors attached to this shape
                            System.Collections.Generic.List<long> attachedConnectorIds = new System.Collections.Generic.List<long>();

                            foreach (Connect connect in page.Connects)
                            {
                                long fromId = connect.FromSheet;
                                long toId = connect.ToSheet;

                                // If the shape is the source and the target is a connector
                                if (fromId == shape.ID && IsConnector(page, toId))
                                {
                                    attachedConnectorIds.Add(toId);
                                }
                                // If the shape is the target and the source is a connector
                                else if (toId == shape.ID && IsConnector(page, fromId))
                                {
                                    attachedConnectorIds.Add(fromId);
                                }
                            }

                            if (attachedConnectorIds.Count > 0)
                            {
                                Console.WriteLine("  Attached Connectors:");
                                foreach (long connectorId in attachedConnectorIds)
                                {
                                    Shape connectorShape = page.Shapes.GetShape(connectorId);
                                    string masterName = connectorShape.Master != null ? connectorShape.Master.Name : "N/A";
                                    Console.WriteLine($"    Connector ID: {connectorShape.ID}, Master: {masterName}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("  No attached connectors.");
                            }
                        }
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }

        // Determines whether a shape is a connector (1‑D shape)
        private static bool IsConnector(Page page, long shapeId)
        {
            Shape shape = page.Shapes.GetShape(shapeId);
            // The OneD property returns a native bool
            return shape.OneD;
        }
    }