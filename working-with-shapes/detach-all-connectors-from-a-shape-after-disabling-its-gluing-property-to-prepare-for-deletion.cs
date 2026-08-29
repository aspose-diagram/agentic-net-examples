using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Find the target shape by its universal name (adjust as needed)
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU != null && shape.NameU.Equals("TargetShape", StringComparison.OrdinalIgnoreCase))
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("Target shape not found.");
                    return;
                }

                // Disable dynamic gluing for the target shape
                // GlueTypeValue.NoAllowDynamicGlue disables outgoing dynamic glue
                targetShape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;

                // Collect IDs of all connector shapes attached to the target shape
                List<long> connectorsToRemove = new List<long>();
                foreach (Connect connect in page.Connects)
                {
                    if (connect.FromSheet == targetShape.ID || connect.ToSheet == targetShape.ID)
                    {
                        // The connector shape ID is stored in the Connect element's FromSheet or ToSheet
                        // (the connector itself is the shape that owns the connection)
                        // Determine which side is the connector (1‑D shape)
                        long possibleConnectorId = (connect.FromSheet == targetShape.ID) ? connect.ToSheet : connect.FromSheet;
                        // Verify the shape is a connector (OneD == true)
                        Shape possibleConnector = page.Shapes.GetShape(possibleConnectorId);
                        if (possibleConnector != null && possibleConnector.OneD)
                        {
                            connectorsToRemove.Add(possibleConnectorId);
                        }
                    }
                }

                // Mark each connector for deletion
                foreach (long connectorId in connectorsToRemove)
                {
                    Shape connectorShape = page.Shapes.GetShape(connectorId);
                    if (connectorShape != null)
                    {
                        connectorShape.Del = BOOL.True;
                    }
                }

                // Optionally, also mark the target shape for deletion if desired
                // targetShape.Del = BOOL.True;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Connectors detached and diagram saved.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }