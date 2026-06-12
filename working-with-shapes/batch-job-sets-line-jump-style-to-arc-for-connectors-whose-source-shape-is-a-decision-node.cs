using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the modified Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Build a quick lookup for shapes by their ID
                    var shapeLookup = new System.Collections.Generic.Dictionary<long, Shape>();
                    foreach (Shape shp in page.Shapes)
                    {
                        shapeLookup[shp.ID] = shp;
                    }

                    // Keep track of connectors already processed to avoid duplicate settings
                    var processedConnectors = new System.Collections.Generic.HashSet<long>();

                    // Examine each connection on the page
                    foreach (Connect conn in page.Connects)
                    {
                        // The shape that the connector is attached to (source)
                        long sourceShapeId = conn.FromSheet;
                        // The connector shape ID
                        long connectorShapeId = conn.ToSheet;

                        // Ensure both IDs exist in the lookup
                        if (!shapeLookup.ContainsKey(sourceShapeId) || !shapeLookup.ContainsKey(connectorShapeId))
                            continue;

                        Shape sourceShape = shapeLookup[sourceShapeId];
                        Shape connectorShape = shapeLookup[connectorShapeId];

                        // Verify the source shape is a decision node (master name "Decision")
                        // and the target shape is a 1‑D connector
                        if (sourceShape.Master != null &&
                            sourceShape.Master.Name == "Decision" &&
                            connectorShape.OneD &&
                            !processedConnectors.Contains(connectorShapeId))
                        {
                            // Set the connector's line jump style to Arc
                            connectorShape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Arc;

                            // Mark this connector as processed
                            processedConnectors.Add(connectorShapeId);
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }